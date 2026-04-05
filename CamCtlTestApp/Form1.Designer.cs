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
            checkBoxCam1ZoomIn = new CheckBox();
            checkBoxCam1ZoomOut = new CheckBox();
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
            // checkBoxCam1ZoomIn
            // 
            checkBoxCam1ZoomIn.AutoSize = true;
            checkBoxCam1ZoomIn.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxCam1ZoomIn.Location = new Point(295, 164);
            checkBoxCam1ZoomIn.Name = "checkBoxCam1ZoomIn";
            checkBoxCam1ZoomIn.Size = new Size(159, 29);
            checkBoxCam1ZoomIn.TabIndex = 1;
            checkBoxCam1ZoomIn.Text = "Cam 1 Zoom In";
            checkBoxCam1ZoomIn.UseVisualStyleBackColor = true;
            checkBoxCam1ZoomIn.CheckedChanged += checkBoxCam1ZoomIn_CheckedChanged;
            // 
            // checkBoxCam1ZoomOut
            // 
            checkBoxCam1ZoomOut.AutoSize = true;
            checkBoxCam1ZoomOut.Font = new Font("Segoe UI", 14F);
            checkBoxCam1ZoomOut.Location = new Point(295, 205);
            checkBoxCam1ZoomOut.Name = "checkBoxCam1ZoomOut";
            checkBoxCam1ZoomOut.Size = new Size(174, 29);
            checkBoxCam1ZoomOut.TabIndex = 2;
            checkBoxCam1ZoomOut.Text = "Cam 1 Zoom Out";
            checkBoxCam1ZoomOut.UseVisualStyleBackColor = true;
            checkBoxCam1ZoomOut.CheckedChanged += checkBoxCam1ZoomOut_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBoxCam1ZoomOut);
            Controls.Add(checkBoxCam1ZoomIn);
            Controls.Add(label1);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox checkBoxCam1ZoomIn;
        private CheckBox checkBoxCam1ZoomOut;
    }
}
