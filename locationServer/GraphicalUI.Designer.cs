
namespace locationserver
{
    partial class GraphicalUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chbxLogging = new System.Windows.Forms.CheckBox();
            this.txtLogging = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.chbxDatabase = new System.Windows.Forms.CheckBox();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.lblThreads = new System.Windows.Forms.Label();
            this.txtThreads = new System.Windows.Forms.TextBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.chbxDebug = new System.Windows.Forms.CheckBox();
            this.rtxtErrors = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // chbxLogging
            // 
            this.chbxLogging.AutoSize = true;
            this.chbxLogging.Location = new System.Drawing.Point(24, 33);
            this.chbxLogging.Name = "chbxLogging";
            this.chbxLogging.Size = new System.Drawing.Size(108, 19);
            this.chbxLogging.TabIndex = 0;
            this.chbxLogging.Text = "Enable Logging";
            this.chbxLogging.UseVisualStyleBackColor = true;
            this.chbxLogging.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtLogging
            // 
            this.txtLogging.Location = new System.Drawing.Point(138, 31);
            this.txtLogging.Name = "txtLogging";
            this.txtLogging.Size = new System.Drawing.Size(144, 23);
            this.txtLogging.TabIndex = 1;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(138, 60);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(144, 23);
            this.txtDatabase.TabIndex = 3;
            // 
            // chbxDatabase
            // 
            this.chbxDatabase.AutoSize = true;
            this.chbxDatabase.Location = new System.Drawing.Point(24, 62);
            this.chbxDatabase.Name = "chbxDatabase";
            this.chbxDatabase.Size = new System.Drawing.Size(112, 19);
            this.chbxDatabase.TabIndex = 2;
            this.chbxDatabase.Text = "Enable Database";
            this.chbxDatabase.UseVisualStyleBackColor = true;
            this.chbxDatabase.CheckedChanged += new System.EventHandler(this.chbxDatabase_CheckedChanged);
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.Location = new System.Drawing.Point(301, 31);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.ReadOnly = true;
            this.rtxtOutput.Size = new System.Drawing.Size(577, 329);
            this.rtxtOutput.TabIndex = 4;
            this.rtxtOutput.Text = "";
            // 
            // lblThreads
            // 
            this.lblThreads.AutoSize = true;
            this.lblThreads.Location = new System.Drawing.Point(24, 94);
            this.lblThreads.Name = "lblThreads";
            this.lblThreads.Size = new System.Drawing.Size(109, 15);
            this.lblThreads.TabIndex = 5;
            this.lblThreads.Text = "Number Of threads";
            // 
            // txtThreads
            // 
            this.txtThreads.Location = new System.Drawing.Point(139, 91);
            this.txtThreads.Name = "txtThreads";
            this.txtThreads.Size = new System.Drawing.Size(69, 23);
            this.txtThreads.TabIndex = 6;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(24, 220);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(271, 140);
            this.btnStartStop.TabIndex = 7;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // chbxDebug
            // 
            this.chbxDebug.AutoSize = true;
            this.chbxDebug.Location = new System.Drawing.Point(24, 122);
            this.chbxDebug.Name = "chbxDebug";
            this.chbxDebug.Size = new System.Drawing.Size(140, 19);
            this.chbxDebug.TabIndex = 8;
            this.chbxDebug.Text = "Enable Debug Output";
            this.chbxDebug.UseVisualStyleBackColor = true;
            // 
            // rtxtErrors
            // 
            this.rtxtErrors.Enabled = false;
            this.rtxtErrors.Location = new System.Drawing.Point(24, 147);
            this.rtxtErrors.Name = "rtxtErrors";
            this.rtxtErrors.Size = new System.Drawing.Size(271, 67);
            this.rtxtErrors.TabIndex = 9;
            this.rtxtErrors.Text = "";
            this.rtxtErrors.Visible = false;
            this.rtxtErrors.TextChanged += new System.EventHandler(this.rtxtErrors_TextChanged);
            // 
            // GraphicalUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 372);
            this.Controls.Add(this.rtxtErrors);
            this.Controls.Add(this.chbxDebug);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.txtThreads);
            this.Controls.Add(this.lblThreads);
            this.Controls.Add(this.rtxtOutput);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.chbxDatabase);
            this.Controls.Add(this.txtLogging);
            this.Controls.Add(this.chbxLogging);
            this.Name = "GraphicalUI";
            this.Text = "GraphicalUI";
            this.Load += new System.EventHandler(this.GraphicalUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbxLogging;
        private System.Windows.Forms.TextBox txtLogging;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.CheckBox chbxDatabase;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.TextBox txtThreads;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.CheckBox chbxDebug;
        private System.Windows.Forms.RichTextBox rtxtErrors;
    }
}