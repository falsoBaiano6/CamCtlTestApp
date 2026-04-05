using System;
using System.IO.Ports;


namespace CamCtlTestApp
{


    public partial class MainForm : Form
    {
        public static string ZOOM_IN_STR = "<2802>";
        public static string ZOOM_OUT_STR = "<2812>";
        public static string START_MARKER_RCVD_CODE = "FE";
        public static int CMD_START_MARKER_IDX = 0;
        public static int RSP_START_MARKER_RCVD_CODE_IDX = 0;
        public static string DATA_CHAR_RCVD_CODE = "AA";
        public static int RSP_DATA_RCVD_CODE_IDX = 2;
        public static int CMD_DATA_IDX = 1;
        public static string END_MARKER_RCVD_CODE = "EF";
        public static int RSP_END_MARKER_RCVD_CODE_IDX = 10;
        public static int CMD_END_MARKER_IDX = 5;
        public static int MAX_CHAR_BUF_SIZE = 16;
        public static int NUM_CMD_CHARS = 6;
        public static int NUM_RSP_CHARS = 12;
        public static int NUM_CODE_CHARS = 2;
        // FEAAAAAAAAEF
        private char[] cmdBuf = new char[MAX_CHAR_BUF_SIZE];
        private char[] rspBuf = new char[MAX_CHAR_BUF_SIZE];

        public SerialPort camPort;

        public MainForm()
        {
            InitializeComponent();
            camPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One);

        }


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
                    if(!(SendCam1ZoomInCmd()))   
                    {
                        MessageBox.Show("Failed to send Zoom In Command or receive valid response.");

                    }
                }
            }
            else
            {
                checkBoxCam1ZoomIn.Checked = false;
            }
        }

        private void checkBoxCam1ZoomOut_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCam1ZoomOut.Checked)
            {
                if(checkBoxCam1ZoomIn.Checked)
                {
                    checkBoxCam1ZoomOut.Checked = false;
                }
                else
                {
                    checkBoxCam1ZoomOut.Checked = true;
                    if (!(SendCam1ZoomOutCmd()))
                    {
                        MessageBox.Show("Failed to send Zoom In Command or receive valid response.");

                    }
                }
            }
            else
            {
                checkBoxCam1ZoomOut.Checked = false;
            }
        }

        private bool SendCam1ZoomInCmd()
        {
            char[] rspArray = new char[NUM_RSP_CHARS];
            for (int i = 0; i < NUM_CMD_CHARS; i++)
            {
                bool success = false;
                char cmd = ZOOM_IN_STR[i];
                char rspChar;
                camPort.Open();
                camPort.Write(cmd.ToString());
                char[] currRspCode = new char[NUM_CODE_CHARS];
                
                char[] STXCode = new char[NUM_CODE_CHARS];
                char[] DataCode = new char[NUM_CODE_CHARS];
                char[] ETXCode = new char[NUM_CODE_CHARS];
                // wait for response char
                for (int j = 0; j < NUM_RSP_CHARS; j++)
                {
                    while (camPort.BytesToRead == 0) { };
                    rspChar = (char)camPort.ReadChar();
                    rspArray[j] = rspChar;

                    // start marker
                    if ((i == CMD_START_MARKER_IDX) && (j == RSP_DATA_RCVD_CODE_IDX - 1))
                    {
                        currRspCode = rspArray[0..2];
                        STXCode = START_MARKER_RCVD_CODE.ToCharArray();
                        bool areEqual = new string(currRspCode) == new string(STXCode);
                        success = (areEqual) ? true : false;
                        if (!success)
                        {
                            return false;
                        }
                    }

                    else
                    {
                        // data characters
                        if ((i > CMD_START_MARKER_IDX) && (i < CMD_END_MARKER_IDX) && (j >= RSP_DATA_RCVD_CODE_IDX) && j < RSP_END_MARKER_RCVD_CODE_IDX)
                        {
                            currRspCode = rspArray[2..10];
                            DataCode = DATA_CHAR_RCVD_CODE.ToCharArray();
                            success = (currRspCode == STXCode);
                            if (!success)
                            {
                                return false;
                            }
                        }
                        else
                        // end marker
                        {
                            if ((i == CMD_END_MARKER_IDX) && (j == RSP_END_MARKER_RCVD_CODE_IDX + 1))
                            {
                                currRspCode = rspArray[10..];
                                ETXCode = END_MARKER_RCVD_CODE.ToCharArray();
                                success = (currRspCode == ETXCode);
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
        private bool SendCam1ZoomOutCmd()
        {
            char[] rspArray = new char[NUM_RSP_CHARS];
            for (int i = 0; i < NUM_CMD_CHARS; i++)
            {
                char cmd = ZOOM_OUT_STR[i];
                char rspChar;
                camPort.Open();
                camPort.Write(cmd.ToString());
                // wait for response char
                for (int j = 0; j < NUM_RSP_CHARS; j++)
                {
                    while (camPort.BytesToRead == 0) { }
                    ;
                    rspChar = (char)camPort.ReadChar();
                    rspArray[i] = rspChar;

                    // start marker
                    if ((i == CMD_START_MARKER_IDX) && (j == RSP_DATA_RCVD_CODE_IDX - 1))
                    {
                        if (!(rspArray[0..2] == START_MARKER_RCVD_CODE.ToCharArray()))
                        {
                            return false;
                        }
                    }

                    else
                    {
                        // data characters
                        if ((i > CMD_START_MARKER_IDX) && (i < CMD_END_MARKER_IDX) && (j >= RSP_DATA_RCVD_CODE_IDX) && j < RSP_END_MARKER_RCVD_CODE_IDX)
                        {
                            if (!(rspArray[2..10] == DATA_CHAR_RCVD_CODE.ToCharArray()))
                            {
                                return false;
                            }
                        }
                        else
                        // end marker
                        {
                            if ((i == CMD_END_MARKER_IDX) && (j == RSP_END_MARKER_RCVD_CODE_IDX + 1))
                            {
                                if (!(rspArray[0..2] == START_MARKER_RCVD_CODE.ToCharArray()))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }

                    }

                }

            }

            // Ensure every path returns a bool
            return false;

        }
    }
}
