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
            buttonCam1ZoomOut = new Button();
            buttonCam1ZoomIn = new Button();
            groupBoxPrerequisites = new GroupBox();
            textBoxPrereqResponse = new TextBox();
            checkBoxUcPwrCycleComplete = new CheckBox();
            groupBoxFunctions.SuspendLayout();
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
            textBoxResponseString.Location = new Point(19, 143);
            textBoxResponseString.Name = "textBoxResponseString";
            textBoxResponseString.ReadOnly = true;
            textBoxResponseString.Size = new Size(739, 23);
            textBoxResponseString.TabIndex = 3;
            // 
            // textBoxCmdString
            // 
            textBoxCmdString.BorderStyle = BorderStyle.FixedSingle;
            textBoxCmdString.Location = new Point(19, 211);
            textBoxCmdString.Name = "textBoxCmdString";
            textBoxCmdString.ReadOnly = true;
            textBoxCmdString.Size = new Size(739, 23);
            textBoxCmdString.TabIndex = 4;
            // 
            // labelResponseString
            // 
            labelResponseString.AutoSize = true;
            labelResponseString.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelResponseString.Location = new Point(19, 108);
            labelResponseString.Name = "labelResponseString";
            labelResponseString.Size = new Size(189, 32);
            labelResponseString.TabIndex = 5;
            labelResponseString.Text = "Response String:";
            // 
            // labelCommandString
            // 
            labelCommandString.AutoSize = true;
            labelCommandString.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCommandString.Location = new Point(19, 169);
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
            groupBoxFunctions.Controls.Add(buttonCam1ZoomOut);
            groupBoxFunctions.Controls.Add(buttonCam1ZoomIn);
            groupBoxFunctions.Controls.Add(labelCommandString);
            groupBoxFunctions.Controls.Add(textBoxResponseString);
            groupBoxFunctions.Controls.Add(textBoxCmdString);
            groupBoxFunctions.Controls.Add(labelResponseString);
            groupBoxFunctions.Location = new Point(12, 201);
            groupBoxFunctions.Name = "groupBoxFunctions";
            groupBoxFunctions.Size = new Size(776, 240);
            groupBoxFunctions.TabIndex = 8;
            groupBoxFunctions.TabStop = false;
            groupBoxFunctions.Text = "Functions";
            // 
            // buttonCam1ZoomOut
            // 
            buttonCam1ZoomOut.Font = new Font("Segoe UI", 18F);
            buttonCam1ZoomOut.Location = new Point(513, 31);
            buttonCam1ZoomOut.Name = "buttonCam1ZoomOut";
            buttonCam1ZoomOut.Size = new Size(220, 40);
            buttonCam1ZoomOut.TabIndex = 8;
            buttonCam1ZoomOut.Text = "Cam 1 Zoom Out";
            buttonCam1ZoomOut.UseVisualStyleBackColor = true;
            buttonCam1ZoomOut.Click += buttonCam1ZoomOut_Click;
            // 
            // buttonCam1ZoomIn
            // 
            buttonCam1ZoomIn.Font = new Font("Segoe UI", 18F);
            buttonCam1ZoomIn.Location = new Point(242, 31);
            buttonCam1ZoomIn.Name = "buttonCam1ZoomIn";
            buttonCam1ZoomIn.Size = new Size(193, 40);
            buttonCam1ZoomIn.TabIndex = 7;
            buttonCam1ZoomIn.Text = "Cam 1 Zoom In";
            buttonCam1ZoomIn.UseVisualStyleBackColor = true;
            buttonCam1ZoomIn.Click += buttonCam1ZoomIn_Click;
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
            ClientSize = new Size(800, 450);
            Controls.Add(groupBoxPrerequisites);
            Controls.Add(groupBoxFunctions);
            Controls.Add(label1);
            Name = "MainForm";
            groupBoxFunctions.ResumeLayout(false);
            groupBoxFunctions.PerformLayout();
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
        private Button buttonCam1ZoomIn;
        private Button buttonCam1ZoomOut;

    }
}
