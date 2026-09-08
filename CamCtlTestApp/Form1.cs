using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.IO.Ports;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;


namespace CamCtlTestApp
{


    public partial class MainForm : Form
    {
        public enum ComState { COM_IDLE, STX, CID, CMD, DATA, ETX, CC };

        public enum ZoomState { ZOOM_IDLE, ZOOM_IN, ZOOM_OUT };

        // codes
        public static string START_MARKER_STR = "<";
        public static string END_MARKER_STR = ">";
        public static string CAM1_STR = "CAM1";
        public static string CAM2_STR = "CAM2";
        public static string CAM3_STR = "CAM3";
        public static string START_MARKER_RCVD_CODE = "FE";
        public static string CAM_ID_RCVD_CODE = "@";
        public static string CMD_RCVD_CODE = "$";
        public static string DATA_CHAR_RCVD_CODE = "AA";
        public static string END_MARKER_RCVD_CODE = "EF";
        public static string HOST_LISTENING_CODE = "!";
        public static string COMMAND_COMPLETE_CODE = "%";

        public static string CAM1_ID = "1";
        public static string CAM2_ID = "2";
        public static string CAM3_ID = "3";

        // ─── Command codes ─────────────
        public static string CMD_PAN_LEFT_STR = "L";
        public static string CMD_PAN_RIGHT_STR = "R";
        public static string CMD_TILT_UP_STR = "U";
        public static string CMD_TILT_DOWN_STR = "D";
        public static string CMD_PAN_STOP_STR = "S";
        public static string CMD_LANC_STR = "Z";
        public static string CMD_LANC_STOP_STR = "Y";
        public static string ZOOM_DIR_IN_STR = "2800";
        public static string ZOOM_DIR_OUT_STR = "2810";
        // Indices
        public static int CMD_START_MARKER_IDX = 0;
        public static int CAM_ID_IDX = 1;
        public static int CMD_IDX = 2;
        public static int RSP_START_MARKER_RCVD_CODE_IDX = 0;
        public static int RSP_CAM_ID_RCVD_CODE_IDX = 2;
        public static int RSP_CMD_RCVD_CODE_IDX = 3;
        public static int RSP_DATA_RCVD_CODE_IDX = 4;
        public static int CMD_DATA_IDX = 3;
        public static int RSP_END_MARKER_RCVD_CODE_IDX = 11;
        public static int CMD_END_MARKER_IDX = 7;
        // Sizes
        public static int MAX_CHAR_BUF_SIZE = 16;
        public static int NUM_CMD_CHARS = 8;
        public static int NUM_RSP_CHARS = 14;
        public static int NUM_CODE_CHARS = 2;
        // Expected return string: FE@$AAAAAAAAEF

        // Public members
        public string textBoxResponseStringText;
        public string textBoxCmdStringText;
        public string textBoxCmdStringCompleteText;

        public ComState currentComState = ComState.COM_IDLE;
        public ZoomState currentZoomState = ZoomState.ZOOM_IDLE;

        public int lancDataByteIndex = 0;
        public string camIdStr = "";
        public string cmdCodeStr = "";
        public string dataStr = "";


        // Private members
        public SerialPort camPort;
        private bool isUcPowerCycled = false;
        private bool cam1ZoomInButtonPressed = false;
        private bool cam1ZoomInButtonReleased = false;
        private bool cam1ZoomOutButtonPressed = false;
        private bool cam1ZoomOutButtonReleased = false;
        private CancellationTokenSource _cts;
        private Task _workerTask;
        private bool isComInitialized = false;


        public MainForm()
        {
            InitializeComponent();
            textBoxPrereqResponse.Text = "Select 'Initialize Mirocontroller and Communication' before operating functions...";
            //InitializeBackgroundWorkers();
            StartWorker();

        }

        ~MainForm()
        {
            // Cleanup code for unmanaged resources
            camPort.Close();
        }

        public void FlushRxBuffer()
        {
            camPort.DiscardInBuffer();
            camPort.BaseStream.Flush();

        }
        private bool charArraysAreEqual(char[] currRspCode, char[] referenceCode)
        {
            return (new string(currRspCode) == new string(referenceCode));
        }

        private void StartWorker()
        {
            if (_workerTask != null && !_workerTask.IsCompleted) return;

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            // Progress for UI updates; runs on UI thread
            var rspProgress = new Progress<string>(s =>
            {
                // update the actual TextBox control, not the string field
                textBoxResponseString.Text = s;
            });
            var cmdProgress = new Progress<string>(s =>
            {
                // update the actual TextBox control, not the string field
                textBoxCmdString.Text = s;
            });

            _workerTask = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (cam1ZoomInButtonPressed)
                    {
                        if(currentComState == ComState.COM_IDLE)
                        {
                            currentZoomState = ZoomState.ZOOM_IN;
                            camIdStr = CAM1_ID;
                            cmdCodeStr = CMD_LANC_STR;
                            dataStr = ZOOM_DIR_IN_STR;
                            UpdateComState(camIdStr, cmdCodeStr, dataStr);
                        }

                        ((IProgress<string>)cmdProgress).Report(textBoxCmdStringText);
                        ((IProgress<string>)rspProgress).Report(textBoxResponseStringText);

                    }

                    if (cam1ZoomInButtonReleased)
                    {
                        if(currentComState == ComState.COM_IDLE)  
                        {
                            // Wait until the currentComState is IDLE before resetting the zoom state to ZOOM_IDLE
                            currentZoomState = ZoomState.ZOOM_IDLE;
                            cam1ZoomInButtonReleased = false;
                            camIdStr = CAM1_ID;
                            cmdCodeStr = CMD_LANC_STOP_STR;
                            dataStr = "";
                            UpdateComState(camIdStr, cmdCodeStr, dataStr);
                        }
                        ((IProgress<string>)cmdProgress).Report(textBoxCmdStringText);
                        ((IProgress<string>)rspProgress).Report(textBoxResponseStringText);
                    }

                    if (cam1ZoomOutButtonPressed)
                    {
                        if (currentComState == ComState.COM_IDLE)
                        {
                            currentZoomState = ZoomState.ZOOM_OUT;
                            camIdStr = CAM1_ID;
                            cmdCodeStr = CMD_LANC_STR;
                            dataStr = ZOOM_DIR_OUT_STR;
                            UpdateComState(camIdStr, cmdCodeStr, dataStr);
                        }

                        ((IProgress<string>)cmdProgress).Report(textBoxCmdStringText);
                        ((IProgress<string>)rspProgress).Report(textBoxResponseStringText);
                    }

                    if (cam1ZoomOutButtonReleased)
                    {
                        if (currentComState == ComState.COM_IDLE)
                        {
                            // Wait until the currentComState is IDLE before resetting the zoom state to ZOOM_IDLE
                            currentZoomState = ZoomState.ZOOM_IDLE;
                            cam1ZoomOutButtonReleased = false;
                            camIdStr = CAM1_ID;
                            cmdCodeStr = CMD_LANC_STOP_STR;
                            dataStr = "";
                            UpdateComState(camIdStr, cmdCodeStr, dataStr);
                        }
                        ((IProgress<string>)cmdProgress).Report(textBoxCmdStringText);
                        ((IProgress<string>)rspProgress).Report(textBoxResponseStringText);
                    }

                    if (currentComState != ComState.COM_IDLE)
                    {
                        // If communication is underway, finish processing it.
                        UpdateComState(camIdStr, cmdCodeStr, dataStr);
                        ((IProgress<string>)cmdProgress).Report(textBoxCmdStringText);
                        ((IProgress<string>)rspProgress).Report(textBoxResponseStringText);
                    }

                        try
                    {
                        await Task.Delay(100, token);
                    }
                    catch (TaskCanceledException) { break; }
                }
            }, token);
        }

        // stop the worker (e.g., on form closing)
        private async Task StopWorkerAsync()
        {
            if (_cts == null) return;
            _cts.Cancel();
            try { await _workerTask; } catch { }
            _cts.Dispose();
            _cts = null;
            _workerTask = null;
        }

        private void UpdateComState(string camID, string commandCode, string data)
        {
            string rspStr = "";

            switch (currentComState)
            {
                case ComState.COM_IDLE:
                    textBoxResponseStringText = "";
                    textBoxCmdStringText = "";
                    // Flush Rx Buffer before sending command to ensure only response chars from current command are processed
                    FlushRxBuffer();
                    lancDataByteIndex = 0;

                    // Send current command char...
                    camPort.Write(START_MARKER_STR);
                    textBoxCmdStringText += START_MARKER_STR;
                    currentComState = ComState.STX;
                    break;

                case ComState.STX:
                    if (camPort.BytesToRead > 0)
                    {
                        rspStr = camPort.ReadLine().Replace("\r", "").Replace("\n", "");
                        textBoxResponseStringText += rspStr;

                        if (rspStr == START_MARKER_RCVD_CODE)
                        {
                            camPort.Write(camID);
                            textBoxCmdStringText += camID;
                            currentComState = ComState.CID;
                        }
                        //else
                        //{
                        //    currentComState = ComState.IDLE;

                        //}
                    }
                    break;

                case ComState.CID:
                    if (camPort.BytesToRead > 0)
                    {
                        rspStr = camPort.ReadLine().Replace("\r", "").Replace("\n", "");
                        textBoxResponseStringText += rspStr;
                        if (rspStr == CAM_ID_RCVD_CODE)
                        {
                            // Send current command code...
                            camPort.Write(commandCode);
                            textBoxCmdStringText += commandCode;
                            currentComState = ComState.CMD;
                        }
                        //else
                        //{
                        //    currentComState = ComState.IDLE;
                        //}
                    }
                    break;

                case ComState.CMD:
                    if (camPort.BytesToRead > 0)
                    {
                        rspStr = camPort.ReadLine().Replace("\r", "").Replace("\n", "");
                        textBoxResponseStringText += rspStr;
                        if (rspStr == CMD_RCVD_CODE)
                        {
                            if (commandCode == CMD_LANC_STR)
                            {

                                // Send first data character...
                                string firstDataChar = data.Substring(lancDataByteIndex++, 1);
                                camPort.Write(firstDataChar);
                                textBoxCmdStringText += firstDataChar;
                                currentComState = ComState.DATA;
                            }
                            else
                            {
                                // command is Pan/Tilt,  command is ZOOM_STOP, or packet is malformed
                                // Send end marker...
                                camPort.Write(END_MARKER_STR);
                                textBoxCmdStringText += END_MARKER_STR;
                                currentComState = ComState.ETX;
                            }
                        }
                        //else
                        //{
                        //    currentComState = ComState.IDLE;
                        //}
                    }
                    break;

                case ComState.DATA:
                    if (camPort.BytesToRead > 0)
                    {
                        rspStr = camPort.ReadLine().Replace("\r", "").Replace("\n", "");
                        textBoxResponseStringText += rspStr;
                        if (rspStr == DATA_CHAR_RCVD_CODE)
                        {
                            if (lancDataByteIndex < data.Length)
                            {
                                string nextDataChar = data.Substring(lancDataByteIndex++, 1);
                                camPort.Write(nextDataChar);
                                textBoxCmdStringText += nextDataChar;
                            }
                            else
                            {
                                // Send end marker...
                                camPort.Write(END_MARKER_STR);
                                textBoxCmdStringText += END_MARKER_STR;
                                currentComState = ComState.ETX;
                            }
                        }
                        //else
                        //{
                        //    currentComState = ComState.IDLE;
                        //}
                    }
                    break;

                case ComState.ETX:
                    if (camPort.BytesToRead > 0)
                    {
                        rspStr = camPort.ReadLine().Replace("\r", "").Replace("\n", "");
                        textBoxResponseStringText += rspStr;
                        if (rspStr == END_MARKER_RCVD_CODE)
                        {
                            currentComState = ComState.CC;
                        }
                        //else
                        //{
                        //    currentComState = ComState.IDLE;
                        //}
                    }
                    break;

                case ComState.CC: // (Command Completed)
                    if (camPort.BytesToRead > 0)
                    {
                        rspStr = camPort.ReadLine().Replace("\r", "").Replace("\n", "");
                        textBoxResponseStringText += rspStr;
                        if (rspStr == COMMAND_COMPLETE_CODE)
                        {
                            currentComState = ComState.COM_IDLE;
                        }
                    }
                    break;


                default:

                    throw new ArgumentOutOfRangeException($"State {currentComState} not implemented.");
                    break;
            }

        }

        public bool InitializeMicroAndComms()
        {
            //camPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
            try
            {
                camPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
                camPort.DtrEnable = true;
                camPort.RtsEnable = true;

                camPort.Open();
                // If execution reaches here, the port is open.
            }
            catch (Exception ex)
            {
                // Handle specific exceptions like UnauthorizedAccessException here.
                MessageBox.Show("COM Port Initialization Failed." + ex.Message);
                //Console.WriteLine("Error opening port: " + ex.Message);
                return false;
            }
            camPort.Write(HOST_LISTENING_CODE);
            // wait for initialization string from Arduino
            Thread.Sleep(500);
            if (camPort.BytesToRead == 0)
            {
                MessageBox.Show("No response from microcontroller. Please check connection and ensure microcontroller is properly initialized.");
                //Console.WriteLine("Error communicating with micro: ");

                return false;
            }
            while (camPort.BytesToRead == 0) { }
            ;
            textBoxResponseString.Text = camPort.ReadLine().ToString();
            if (textBoxResponseString.Text.Length == 0)
            {
                return false;
            }
            isComInitialized = true;
            return true;
        }

        // UI Event Handlers


        private void checkBoxInitializeMicro_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInitializeMicro.Checked)
            {
                textBoxPrereqResponse.Text = "Cycle Power to Microcontroller... Check uC power cycle complete? box when completed";
            }
        }

        private void checkBoxUcPwrCycleComplete_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUcPwrCycleComplete.Checked)
            {
                isUcPowerCycled = true;
                if (InitializeMicroAndComms())
                {
                    textBoxPrereqResponse.Text = "Microcontroller Power Cycle complete... COM Port initialized";
                }
                else
                {
                    textBoxPrereqResponse.Text = "Microcontroller communication failed";
                }

                checkBoxInitializeMicro.Checked = false;
                checkBoxUcPwrCycleComplete.Checked = false;
            }
        }

        private void buttonCam1ZoomIn_Click(object sender, EventArgs e)
        {
            if ((cam1ZoomInButtonPressed == false) && (cam1ZoomOutButtonPressed == false))
            {
                cam1ZoomInButtonPressed = true;
                buttonCam1ZoomIn.BackColor = Color.LightGreen;
                if (!(isUcPowerCycled))
                {
                    MessageBox.Show("Please initialize microcontroller and communication before sending commands.");
                    cam1ZoomInButtonPressed = false;
                    buttonCam1ZoomIn.BackColor = SystemColors.Control;
                    return;
                }
            }
            else
            {
                if (cam1ZoomInButtonReleased == false)
                {
                    cam1ZoomInButtonReleased = true;
                }
                cam1ZoomInButtonPressed = false;
                buttonCam1ZoomIn.BackColor = SystemColors.Control;
                textBoxCmdString.Text = textBoxCmdStringCompleteText;
            }
        }

        private void buttonCam1ZoomOut_Click(object sender, EventArgs e)
        {
            if ((cam1ZoomOutButtonPressed == false) && (cam1ZoomInButtonPressed == false))
            {
                cam1ZoomOutButtonPressed = true;
                buttonCam1ZoomOut.BackColor = Color.LightGreen;
                if (!(isUcPowerCycled))
                {
                    MessageBox.Show("Please initialize microcontroller and communication before sending commands.");
                    cam1ZoomOutButtonPressed = false;
                    buttonCam1ZoomOut.BackColor = SystemColors.Control;
                    return;
                }
            }
            else
            {
                if (cam1ZoomOutButtonReleased == false)
                {
                    cam1ZoomOutButtonReleased = true;
                }
                cam1ZoomOutButtonPressed = false;
                buttonCam1ZoomIn.BackColor = SystemColors.Control;
                textBoxCmdString.Text = textBoxCmdStringCompleteText;
            }

        }
    }
}
