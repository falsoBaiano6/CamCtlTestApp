using System.ComponentModel;

namespace CamCtlTestApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBoxResponseString = new TextBox();
            textBoxCmdString = new TextBox();
            labelResponseString = new Label();
            labelCommandString = new Label();
            checkBoxInitializeMicro = new CheckBox();
            groupBoxFunctions = new GroupBox();
            buttonTiltUp = new Button();
            buttonTiltDown = new Button();
            buttonPanRight = new Button();
            buttonPanLeft = new Button();
            groupBoxActiveCamera = new GroupBox();
            radioButtonCam3 = new RadioButton();
            radioButtonCam2 = new RadioButton();
            radioButtonCam1 = new RadioButton();
            buttonZoomOut = new Button();
            buttonCamZoomIn = new Button();
            groupBoxPrerequisites = new GroupBox();
            textBoxPrereqResponse = new TextBox();
            checkBoxUcPwrCycleComplete = new CheckBox();
            groupBoxFunctions.SuspendLayout();
            groupBoxActiveCamera.SuspendLayout();
            groupBoxPrerequisites.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 26F);
            label1.Location = new Point(109, 25);
            label1.Name = "label1";
            label1.Size = new Size(550, 47);
            label1.TabIndex = 0;
            label1.Text = "Cam Ctl Test App -- Main Window";
            // 
            // textBoxResponseString
            // 
            textBoxResponseString.BorderStyle = BorderStyle.FixedSingle;
            textBoxResponseString.Location = new Point(23, 524);
            textBoxResponseString.Name = "textBoxResponseString";
            textBoxResponseString.ReadOnly = true;
            textBoxResponseString.Size = new Size(739, 23);
            textBoxResponseString.TabIndex = 3;
            // 
            // textBoxCmdString
            // 
            textBoxCmdString.BorderStyle = BorderStyle.FixedSingle;
            textBoxCmdString.Location = new Point(23, 592);
            textBoxCmdString.Name = "textBoxCmdString";
            textBoxCmdString.ReadOnly = true;
            textBoxCmdString.Size = new Size(739, 23);
            textBoxCmdString.TabIndex = 4;
            // 
            // labelResponseString
            // 
            labelResponseString.AutoSize = true;
            labelResponseString.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelResponseString.Location = new Point(23, 489);
            labelResponseString.Name = "labelResponseString";
            labelResponseString.Size = new Size(189, 32);
            labelResponseString.TabIndex = 5;
            labelResponseString.Text = "Response String:";
            // 
            // labelCommandString
            // 
            labelCommandString.AutoSize = true;
            labelCommandString.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCommandString.Location = new Point(23, 550);
            labelCommandString.Name = "labelCommandString";
            labelCommandString.Size = new Size(199, 32);
            labelCommandString.TabIndex = 6;
            labelCommandString.Text = "Command String:";
            // 
            // checkBoxInitializeMicro
            // 
            checkBoxInitializeMicro.AutoSize = true;
            checkBoxInitializeMicro.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxInitializeMicro.Location = new Point(6, 13);
            checkBoxInitializeMicro.Name = "checkBoxInitializeMicro";
            checkBoxInitializeMicro.Size = new Size(414, 29);
            checkBoxInitializeMicro.TabIndex = 7;
            checkBoxInitializeMicro.Text = "Initialize Microcontroller and Communication";
            checkBoxInitializeMicro.UseVisualStyleBackColor = true;
            checkBoxInitializeMicro.CheckedChanged += checkBoxInitializeMicro_CheckedChanged;
            // 
            // groupBoxFunctions
            // 
            groupBoxFunctions.Controls.Add(buttonTiltUp);
            groupBoxFunctions.Controls.Add(buttonTiltDown);
            groupBoxFunctions.Controls.Add(buttonPanRight);
            groupBoxFunctions.Controls.Add(buttonPanLeft);
            groupBoxFunctions.Controls.Add(groupBoxActiveCamera);
            groupBoxFunctions.Controls.Add(buttonZoomOut);
            groupBoxFunctions.Controls.Add(buttonCamZoomIn);
            groupBoxFunctions.Location = new Point(12, 201);
            groupBoxFunctions.Name = "groupBoxFunctions";
            groupBoxFunctions.Size = new Size(776, 292);
            groupBoxFunctions.TabIndex = 8;
            groupBoxFunctions.TabStop = false;
            groupBoxFunctions.Text = "Functions";
            // 
            // buttonTiltUp
            // 
            buttonTiltUp.Font = new Font("Segoe UI", 18F);
            buttonTiltUp.Location = new Point(513, 219);
            buttonTiltUp.Name = "buttonTiltUp";
            buttonTiltUp.Size = new Size(220, 40);
            buttonTiltUp.TabIndex = 13;
            buttonTiltUp.Text = "Tilt Up";
            buttonTiltUp.UseVisualStyleBackColor = true;
            // 
            // buttonTiltDown
            // 
            buttonTiltDown.Font = new Font("Segoe UI", 18F);
            buttonTiltDown.Location = new Point(242, 219);
            buttonTiltDown.Name = "buttonTiltDown";
            buttonTiltDown.Size = new Size(193, 40);
            buttonTiltDown.TabIndex = 12;
            buttonTiltDown.Text = "Tilt Down";
            buttonTiltDown.UseVisualStyleBackColor = true;
            // 
            // buttonPanRight
            // 
            buttonPanRight.Font = new Font("Segoe UI", 18F);
            buttonPanRight.Location = new Point(513, 129);
            buttonPanRight.Name = "buttonPanRight";
            buttonPanRight.Size = new Size(220, 40);
            buttonPanRight.TabIndex = 11;
            buttonPanRight.Text = "Pan Right";
            buttonPanRight.UseVisualStyleBackColor = true;
            // 
            // buttonPanLeft
            // 
            buttonPanLeft.Font = new Font("Segoe UI", 18F);
            buttonPanLeft.Location = new Point(242, 129);
            buttonPanLeft.Name = "buttonPanLeft";
            buttonPanLeft.Size = new Size(193, 40);
            buttonPanLeft.TabIndex = 10;
            buttonPanLeft.Text = "Pan Left";
            buttonPanLeft.UseVisualStyleBackColor = true;
            // 
            // groupBoxActiveCamera
            // 
            groupBoxActiveCamera.Controls.Add(radioButtonCam3);
            groupBoxActiveCamera.Controls.Add(radioButtonCam2);
            groupBoxActiveCamera.Controls.Add(radioButtonCam1);
            groupBoxActiveCamera.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBoxActiveCamera.Location = new Point(20, 60);
            groupBoxActiveCamera.Name = "groupBoxActiveCamera";
            groupBoxActiveCamera.Size = new Size(200, 145);
            groupBoxActiveCamera.TabIndex = 9;
            groupBoxActiveCamera.TabStop = false;
            groupBoxActiveCamera.Text = "Active Camera";
            // 
            // radioButtonCam3
            // 
            radioButtonCam3.AutoSize = true;
            radioButtonCam3.Location = new Point(23, 111);
            radioButtonCam3.Name = "radioButtonCam3";
            radioButtonCam3.Size = new Size(90, 34);
            radioButtonCam3.TabIndex = 2;
            radioButtonCam3.TabStop = true;
            radioButtonCam3.Text = "Cam 3";
            radioButtonCam3.UseVisualStyleBackColor = true;
            radioButtonCam3.CheckedChanged += radioButtonActiveCamera_CheckedChanged;
            // 
            // radioButtonCam2
            // 
            radioButtonCam2.AutoSize = true;
            radioButtonCam2.Location = new Point(23, 71);
            radioButtonCam2.Name = "radioButtonCam2";
            radioButtonCam2.Size = new Size(90, 34);
            radioButtonCam2.TabIndex = 1;
            radioButtonCam2.TabStop = true;
            radioButtonCam2.Text = "Cam 2";
            radioButtonCam2.UseVisualStyleBackColor = true;
            radioButtonCam2.CheckedChanged += radioButtonActiveCamera_CheckedChanged;
            // 
            // radioButtonCam1
            // 
            radioButtonCam1.AutoSize = true;
            radioButtonCam1.Checked = true;
            radioButtonCam1.Location = new Point(23, 34);
            radioButtonCam1.Name = "radioButtonCam1";
            radioButtonCam1.Size = new Size(90, 34);
            radioButtonCam1.TabIndex = 0;
            radioButtonCam1.TabStop = true;
            radioButtonCam1.Text = "Cam 1";
            radioButtonCam1.UseVisualStyleBackColor = true;
            radioButtonCam1.CheckedChanged += radioButtonActiveCamera_CheckedChanged;
            // 
            // buttonZoomOut
            // 
            buttonZoomOut.Font = new Font("Segoe UI", 18F);
            buttonZoomOut.Location = new Point(513, 31);
            buttonZoomOut.Name = "buttonZoomOut";
            buttonZoomOut.Size = new Size(220, 40);
            buttonZoomOut.TabIndex = 8;
            buttonZoomOut.Text = "Zoom Out";
            buttonZoomOut.UseVisualStyleBackColor = true;
            buttonZoomOut.Click += buttonCam1ZoomOut_Click;
            // 
            // buttonCamZoomIn
            // 
            buttonCamZoomIn.Font = new Font("Segoe UI", 18F);
            buttonCamZoomIn.Location = new Point(242, 31);
            buttonCamZoomIn.Name = "buttonCamZoomIn";
            buttonCamZoomIn.Size = new Size(193, 40);
            buttonCamZoomIn.TabIndex = 7;
            buttonCamZoomIn.Text = "Zoom In";
            buttonCamZoomIn.UseVisualStyleBackColor = true;
            buttonCamZoomIn.Click += buttonCam1ZoomIn_Click;
            // 
            // groupBoxPrerequisites
            // 
            groupBoxPrerequisites.Controls.Add(textBoxPrereqResponse);
            groupBoxPrerequisites.Controls.Add(checkBoxInitializeMicro);
            groupBoxPrerequisites.Controls.Add(checkBoxUcPwrCycleComplete);
            groupBoxPrerequisites.Location = new Point(109, 90);
            groupBoxPrerequisites.Name = "groupBoxPrerequisites";
            groupBoxPrerequisites.Size = new Size(653, 105);
            groupBoxPrerequisites.TabIndex = 9;
            groupBoxPrerequisites.TabStop = false;
            groupBoxPrerequisites.Text = "Prerequisites";
            // 
            // textBoxPrereqResponse
            // 
            textBoxPrereqResponse.BorderStyle = BorderStyle.FixedSingle;
            textBoxPrereqResponse.Location = new Point(6, 76);
            textBoxPrereqResponse.Name = "textBoxPrereqResponse";
            textBoxPrereqResponse.ReadOnly = true;
            textBoxPrereqResponse.Size = new Size(630, 23);
            textBoxPrereqResponse.TabIndex = 13;
            // 
            // checkBoxUcPwrCycleComplete
            // 
            checkBoxUcPwrCycleComplete.AutoSize = true;
            checkBoxUcPwrCycleComplete.Font = new Font("Segoe UI", 14.25F);
            checkBoxUcPwrCycleComplete.Location = new Point(6, 41);
            checkBoxUcPwrCycleComplete.Name = "checkBoxUcPwrCycleComplete";
            checkBoxUcPwrCycleComplete.Size = new Size(251, 29);
            checkBoxUcPwrCycleComplete.TabIndex = 12;
            checkBoxUcPwrCycleComplete.Text = "uC power cycle complete?";
            checkBoxUcPwrCycleComplete.UseVisualStyleBackColor = true;
            checkBoxUcPwrCycleComplete.CheckedChanged += checkBoxUcPwrCycleComplete_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 638);
            Controls.Add(groupBoxPrerequisites);
            Controls.Add(groupBoxFunctions);
            Controls.Add(label1);
            Controls.Add(labelCommandString);
            Controls.Add(textBoxCmdString);
            Controls.Add(textBoxResponseString);
            Controls.Add(labelResponseString);
            Name = "MainForm";
            groupBoxFunctions.ResumeLayout(false);
            groupBoxActiveCamera.ResumeLayout(false);
            groupBoxActiveCamera.PerformLayout();
            groupBoxPrerequisites.ResumeLayout(false);
            groupBoxPrerequisites.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxResponseString;
        private TextBox textBoxCmdString;
        private Label labelResponseString;
        private Label labelCommandString;
        private CheckBox checkBoxInitializeMicro;
        private GroupBox groupBoxFunctions;
        private GroupBox groupBoxPrerequisites;
        private CheckBox checkBoxUcPwrCycleComplete;
        private TextBox textBoxPrereqResponse;
        private Button buttonCamZoomIn;
        private Button buttonZoomOut;
        private GroupBox groupBoxActiveCamera;
        private RadioButton radioButtonCam2;
        private RadioButton radioButtonCam1;
        private RadioButton radioButtonCam3;
        private Button buttonPanRight;
        private Button buttonPanLeft;
        private Button buttonTiltUp;
        private Button buttonTiltDown;
    }
}
