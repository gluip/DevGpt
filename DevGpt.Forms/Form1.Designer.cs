namespace DevGpt.Forms
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnStart = new Button();
            txtLog = new TextBox();
            txtPrompt = new TextBox();
            btnScreensho = new Button();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(701, 387);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // txtLog
            // 
            txtLog.AcceptsReturn = true;
            txtLog.Location = new Point(12, 12);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(956, 332);
            txtLog.TabIndex = 1;
            // 
            // txtPrompt
            // 
            txtPrompt.Location = new Point(2, 387);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(693, 87);
            txtPrompt.TabIndex = 2;
            txtPrompt.Text = resources.GetString("txtPrompt.Text");
            // 
            // btnScreensho
            // 
            btnScreensho.Location = new Point(701, 422);
            btnScreensho.Name = "btnScreensho";
            btnScreensho.Size = new Size(143, 29);
            btnScreensho.TabIndex = 3;
            btnScreensho.Text = "Get Screenshot";
            btnScreensho.UseVisualStyleBackColor = true;
            btnScreensho.Click += btnScreensho_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(965, 486);
            Controls.Add(btnScreensho);
            Controls.Add(txtPrompt);
            Controls.Add(txtLog);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private TextBox txtLog;
        private TextBox txtPrompt;
        private Button btnScreensho;
    }
}
