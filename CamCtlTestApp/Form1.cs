using System;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO.Ports;
using System.Reflection;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace CamCtlTestApp
{


    public partial class MainForm : Form
    {
        // codes
        public static string CAM1_STR = "CAM1";
        public static string CAM2_STR = "CAM2";
        public static string CAM3_STR = "CAM3";
        public static string ZOOM_IN_STR = "<12800>";
        public static string ZOOM_OUT_STR = "<12810>";
        public static string START_MARKER_RCVD_CODE = "FE";
        public static string CAM_ID_RCVD_CODE = "@";
        public static string DATA_CHAR_RCVD_CODE = "AA";
        public static string END_MARKER_RCVD_CODE = "EF";
        public static string HOST_LISTENING_CODE = "!";
        // Indices
        public static int CMD_START_MARKER_IDX = 0;
        public static int CAM_ID_IDX = 1;
        public static int RSP_START_MARKER_RCVD_CODE_IDX = 0;
        public static int RSP_CAM_ID_RCVD_CODE_IDX = 2;
        public static int RSP_DATA_RCVD_CODE_IDX = 3;
        public static int CMD_DATA_IDX = 2;
        public static int RSP_END_MARKER_RCVD_CODE_IDX = 11;
        public static int CMD_END_MARKER_IDX = 6;
        // Sizes
        public static int MAX_CHAR_BUF_SIZE = 16;
        public static int NUM_CMD_CHARS = 7;
        public static int NUM_RSP_CHARS = 13;
        public static int NUM_CODE_CHARS = 2;
        // Expected return string: FEAAAAAAAAEF

        // Private members
        public SerialPort camPort;
        private bool isUcPowerCycled = false;
        private bool cam1ZoomInButtonPressed = false;
        private bool cam1ZoomOutButtonPressed = false;
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private BackgroundWorker backgroundWorker2 = new BackgroundWorker();
        string textBoxResponseStringText;
        string textBoxCmdStringText;
        string textBoxCmdStringCompleteText;

        public MainForm()
        {
            InitializeComponent();
            textBoxPrereqResponse.Text = "Select 'Initialize Mirocontroller and Communication' before operating functions...";
            InitializeBackgroundWorkers();
        }

        ~MainForm()
        {
            // Cleanup code for unmanaged resources
            backgroundWorker1.CancelAsync();
            backgroundWorker1.Dispose();
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

        void InitializeBackgroundWorkers()
        {
 

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;

            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            while(true)
            {
                if (cam1ZoomInButtonPressed)
                {
                    if (!(SendZoomCmd("CAM1", ZOOM_IN_STR)))
                    {
                        MessageBox.Show("Send Zoom In Command Failed.");
                        cam1ZoomInButtonPressed = false;
                        buttonCam1ZoomIn.BackColor = SystemColors.Control;
                        return;
                    }
                    else
                    {
                        // Succeeded -- Update textBoxCmdString in UI
                        backgroundWorker1.ReportProgress(0, "");
                        backgroundWorker1.ReportProgress(0, textBoxResponseStringText);
                    }
                }

                if (cam1ZoomOutButtonPressed)
                {
                    if (!(SendZoomCmd("CAM1", ZOOM_OUT_STR)))
                    {
                        MessageBox.Show("Send Zoom Out Command Failed.");
                        cam1ZoomOutButtonPressed = false;
                        buttonCam1ZoomOut.BackColor = SystemColors.Control;
                        return;
                    }
                    else
                    {
                        // Succeeded -- Update textBoxResponseString in UI
                        backgroundWorker1.ReportProgress(0, "");
                        backgroundWorker1.ReportProgress(0, textBoxResponseStringText);
                    }
                }
            }
        }
        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                // First, handle the case where an exception was thrown.
                if (e.Error != null)
                {
                    _ = MessageBox.Show(e.Error.Message);
                }
                else if (e.Cancelled)
                {
                }
                else
                {
                }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBoxResponseString.Text = (string)e.UserState;
        } 

        private bool SendZoomCmd(string cameraStr, string zoomDirectionStr)
        {
            char[] rspArray = new char[NUM_RSP_CHARS];
            bool success = false;
            char cmdChar;
            char rspChar;
            string rspStr = "";
            char[] currRspCode = new char[NUM_CODE_CHARS];
            char[] STXCode = new char[NUM_CODE_CHARS];
            char[] camIdCode = new char[1];
            char[] DataCode = new char[NUM_CODE_CHARS];
            char[] ETXCode = new char[NUM_CODE_CHARS];

            textBoxResponseStringText = "";
            textBoxCmdStringText = "";

            // Flush Rx Buffer before sending command to ensure only response chars from current command are processed
            FlushRxBuffer();

    
            for (int i = 0; i < NUM_CMD_CHARS; i++)
            {
                success = false;
                cmdChar = zoomDirectionStr[i];

                // Send current command char...
                camPort.Write(cmdChar.ToString());

                textBoxCmdStringText += cmdChar;

                while (camPort.BytesToRead == 0) { } ;
                rspStr =camPort.ReadLine().Replace("\r", "").Replace("\n", "");


                // start marker
                if (i == CMD_START_MARKER_IDX)
                {
                    Array.Copy(rspStr.ToCharArray(), 0, rspArray, RSP_START_MARKER_RCVD_CODE_IDX, NUM_CODE_CHARS);
                    STXCode = START_MARKER_RCVD_CODE.ToCharArray();
                    textBoxResponseStringText += new string(rspStr);
                    success = (charArraysAreEqual(rspStr.ToCharArray(), STXCode)) ? true : false;
                    if (!success)
                    {
                        return false;                                                                                                                                                                     return false;
                    }
                }
                // Indices                                                 
                else
                {
                    // camera ID character
                    if (i == CAM_ID_IDX)
                    {
                        Array.Copy(rspStr.ToCharArray(), 0, rspArray, RSP_CAM_ID_RCVD_CODE_IDX, 1);
                        camIdCode = CAM_ID_RCVD_CODE.ToCharArray();
                        textBoxResponseStringText += new string(rspStr);
                        success = (charArraysAreEqual(rspStr.ToCharArray(), camIdCode)) ? true : false;
                        if (!success)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // data characters
                        if ((i > CAM_ID_IDX) && (i < CMD_END_MARKER_IDX))
                        {
                            Array.Copy(rspStr.ToCharArray(), 0, rspArray, RSP_DATA_RCVD_CODE_IDX + ((i - CMD_DATA_IDX) * NUM_CODE_CHARS), NUM_CODE_CHARS);
                            DataCode = DATA_CHAR_RCVD_CODE.ToCharArray();
                            textBoxResponseStringText += new string(rspStr);
                            success = (charArraysAreEqual(rspStr.ToCharArray(), DataCode)) ? true : false;
                            if (!success)
                            {
                                return false;
                            }
                        }
                        else
                        // end marker
                        {
                            if (i == CMD_END_MARKER_IDX)
                            {
                                Array.Copy(rspStr.ToCharArray(), 0, rspArray, RSP_END_MARKER_RCVD_CODE_IDX, NUM_CODE_CHARS);
                                ETXCode = END_MARKER_RCVD_CODE.ToCharArray();
                                textBoxResponseStringText += new string(rspStr);
                                textBoxCmdStringCompleteText = textBoxCmdStringText;
                                success = (charArraysAreEqual(rspStr.ToCharArray(), ETXCode)) ? true : false;
                                if (!success)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                        }

                    }


                }

            }

            // Ensure every path returns a bool
            return false;
        }


        public bool InitializeMicroAndComms()
        {
            //camPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
            try
            {
                camPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);

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
            if(camPort.BytesToRead == 0)
            {
                MessageBox.Show("No response from microcontroller. Please check connection and ensure microcontroller is properly initialized.");
                //Console.WriteLine("Error communicating with micro: ");

                return false;
            }
            while (camPort.BytesToRead == 0) { };
            textBoxResponseString.Text = camPort.ReadLine().ToString();
            if (textBoxResponseString.Text.Length == 0)
            {

                return false;
            }
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
                if(InitializeMicroAndComms())
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
                cam1ZoomOutButtonPressed = false;
                buttonCam1ZoomOut.BackColor = SystemColors.Control;
                textBoxCmdString.Text = textBoxCmdStringCompleteText;
            }

        }
    }
}
