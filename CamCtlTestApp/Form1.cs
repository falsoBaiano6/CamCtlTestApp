using System;
using System.IO.Ports;
using System.Security.Cryptography;


namespace CamCtlTestApp
{


    public partial class MainForm : Form
    {
        // codes
        public static string CAM1_STR = "CAM1";
        public static string CAM2_STR = "CAM2";
        public static string CAM3_STR = "CAM3";
        public static string ZOOM_IN_STR = "<2802>";
        public static string ZOOM_OUT_STR = "<2812>";
        public static string START_MARKER_RCVD_CODE = "FE";
        public static string DATA_CHAR_RCVD_CODE = "AA";
        public static string END_MARKER_RCVD_CODE = "EF";
        public static string HOST_LISTENING_CODE = "!";
        // Indices
        public static int CMD_START_MARKER_IDX = 0;
        public static int RSP_START_MARKER_RCVD_CODE_IDX = 0;
        public static int RSP_DATA_RCVD_CODE_IDX = 2;
        public static int CMD_DATA_IDX = 1;
        public static int RSP_END_MARKER_RCVD_CODE_IDX = 10;
        public static int CMD_END_MARKER_IDX = 5;
        // Sizes
        public static int MAX_CHAR_BUF_SIZE = 16;
        public static int NUM_CMD_CHARS = 6;
        public static int NUM_RSP_CHARS = 12;
        public static int NUM_CODE_CHARS = 2;
        // Expected return string: FEAAAAAAAAEF

        public SerialPort camPort;
        private bool isUcPowerCycled = false;

        public MainForm()
        {
            InitializeComponent();
            textBoxPrereqResponse.Text = "Select 'Initialize Mirocontroller and Communication' before operating functions...";
        }

        ~MainForm()
        {
            // Cleanup code for unmanaged resources
            camPort.Close();
        }

        public void FlushRxBuffer()
        {
            camPort.DiscardInBuffer();
        }
        private bool charArraysAreEqual(char[] currRspCode, char[] referenceCode)
        {
            return (new string(currRspCode) == new string(referenceCode));
        }

        private bool SendZoomCmd(string cameraStr, string zoomDirectionStr)
        {
            char[] rspArray = new char[NUM_RSP_CHARS];
            bool success = false;
            char cmdChar;
            char rspChar;
            char[] currRspCode = new char[NUM_CODE_CHARS];
            char[] STXCode = new char[NUM_CODE_CHARS];
            char[] DataCode = new char[NUM_CODE_CHARS];
            char[] ETXCode = new char[NUM_CODE_CHARS];


            textBoxResponseString.Text = ""; // Clear response string text box before sending command
            textBoxCmdString.Text = ""; // Clear command string text box before sending command

            // Flush Rx Buffer before sending command to ensure only response chars from current command are processed
            FlushRxBuffer();



            for (int i = 0; i < NUM_CMD_CHARS; i++)
            {
                success = false;
                cmdChar = zoomDirectionStr[i];

                // Send current command char...
                camPort.Write(cmdChar.ToString());
                textBoxCmdString.Text += cmdChar;

                // wait for response char
                for (int j = 0; j < NUM_CODE_CHARS; j++)
                {
                    while (camPort.BytesToRead == 0) { };
                    rspChar = (char)camPort.ReadChar();
                    currRspCode[j] = rspChar;


                    // start marker
                    if ((i == CMD_START_MARKER_IDX) && (j == NUM_CODE_CHARS - 1))
                    {
                        Array.Copy(currRspCode, 0, rspArray, RSP_START_MARKER_RCVD_CODE_IDX, NUM_CODE_CHARS);
                        STXCode = START_MARKER_RCVD_CODE.ToCharArray();
                        textBoxResponseString.Text += new string(currRspCode);
                        success = (charArraysAreEqual(currRspCode, STXCode)) ? true : false;
                        if (!success)
                        {
                            return false;
                        }
                    }
                    // Indices
                    else
                    {
                        // data characters
                        if ((i > CMD_START_MARKER_IDX) && (i < CMD_END_MARKER_IDX) && (j == NUM_CODE_CHARS - 1))
                        {
                            Array.Copy(currRspCode, 0, rspArray, RSP_DATA_RCVD_CODE_IDX + ((i - CMD_DATA_IDX) * NUM_CODE_CHARS), NUM_CODE_CHARS);
                            DataCode = DATA_CHAR_RCVD_CODE.ToCharArray();
                            textBoxResponseString.Text += new string(currRspCode);
                            success = (charArraysAreEqual(currRspCode, DataCode)) ? true : false;
                            if (!success)
                            {
                                return false;
                            }
                        }
                        else
                        // end marker
                        {
                            if ((i == CMD_END_MARKER_IDX) && (j == NUM_CODE_CHARS - 1))
                            {
                                Array.Copy(currRspCode, 0, rspArray, RSP_END_MARKER_RCVD_CODE_IDX, NUM_CODE_CHARS);
                                ETXCode = END_MARKER_RCVD_CODE.ToCharArray();
                                textBoxResponseString.Text += new string(currRspCode);
                                success = (charArraysAreEqual(currRspCode, ETXCode)) ? true : false;
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
        public void InitializeMicroAndComms()
        {
            camPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);
            camPort.Open();
            camPort.Write(HOST_LISTENING_CODE);
            // wait for initialization string from Arduino
            while (camPort.BytesToRead == 0) { };
            textBoxResponseString.Text = camPort.ReadLine().ToString();
        }

        // UI Event Handlers
        private void checkBoxCam1ZoomIn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCam1ZoomIn.Checked)
            {
                if (checkBoxCam1ZoomOut.Checked)
                {
                    checkBoxCam1ZoomIn.Checked = false;
                }
                else
                {
                    checkBoxCam1ZoomIn.Checked = true;
                    if (!(isUcPowerCycled))
                    {
                        MessageBox.Show("Please initialize microcontroller and communication before sending commands.");
                        checkBoxCam1ZoomIn.Checked = false;
                        return;
                    }
                    if (!(SendZoomCmd("CAM1", ZOOM_IN_STR)))
                    {
                        MessageBox.Show("Send Zoom In Command Failed.");
                        checkBoxCam1ZoomIn.Checked = false;
                        return;

                    }
                    else 
                    {
                        // Command succeeded
                        checkBoxCam1ZoomIn.Checked = false;
                        return;
                    }
                }
            }
            else
            {
                // Unchecking the box will not send a command, it simply allows the user to select the function again if desired
            }
        }

        private void checkBoxCam1ZoomOut_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCam1ZoomOut.Checked)
            {
                if (checkBoxCam1ZoomIn.Checked)
                {
                    checkBoxCam1ZoomOut.Checked = false;
                }
                else
                {
                    checkBoxCam1ZoomOut.Checked = true;
                    if(!(isUcPowerCycled))
                    {
                        MessageBox.Show("Please initialize microcontroller and communication before sending commands.");
                        checkBoxCam1ZoomOut.Checked = false;
                        return;

                    }
                    if (!(SendZoomCmd("CAM1", ZOOM_OUT_STR)))
                    {
                        MessageBox.Show("Send Zoom Out Command Failed.");
                        checkBoxCam1ZoomOut.Checked = false;
                        return;
                    }
                    else
                    {
                        // Command succeeded
                        checkBoxCam1ZoomOut.Checked = false;
                        return;

                    }
                }
            }
            else
            {
                // Unchecking the box will not send a command, it simply allows the user to select the function again if desired
            }
        }

        private void checkBoxInitializeMicro_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInitializeMicro.Checked)
            {
               textBoxPrereqResponse.Text = "Cycle Power to Microcontroller... Check uC power cycle complete? box when completed";
             }
        }

        private void checkBoxUcPwrCycleComplete_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxUcPwrCycleComplete.Checked)
            {
                isUcPowerCycled = true; 
                InitializeMicroAndComms();
                textBoxPrereqResponse.Text = "Microcontroller Power Cycle complete... COM Port initialized";
                checkBoxInitializeMicro.Checked = false;
                checkBoxUcPwrCycleComplete.Checked = false;
            }
        }
    }
}
