
namespace location
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
            this.rtxtServerOuput = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWhois = new System.Windows.Forms.RadioButton();
            this.btnH9 = new System.Windows.Forms.RadioButton();
            this.btnH0 = new System.Windows.Forms.RadioButton();
            this.btnH1 = new System.Windows.Forms.RadioButton();
            this.txtbxServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbxTimeout = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtArgs = new System.Windows.Forms.TextBox();
            this.lblArgs = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtxtServerOuput
            // 
            this.rtxtServerOuput.Location = new System.Drawing.Point(404, 30);
            this.rtxtServerOuput.Name = "rtxtServerOuput";
            this.rtxtServerOuput.ReadOnly = true;
            this.rtxtServerOuput.Size = new System.Drawing.Size(384, 190);
            this.rtxtServerOuput.TabIndex = 0;
            this.rtxtServerOuput.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(404, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Response";
            // 
            // btnWhois
            // 
            this.btnWhois.AutoSize = true;
            this.btnWhois.Checked = true;
            this.btnWhois.Location = new System.Drawing.Point(12, 12);
            this.btnWhois.Name = "btnWhois";
            this.btnWhois.Size = new System.Drawing.Size(58, 19);
            this.btnWhois.TabIndex = 2;
            this.btnWhois.TabStop = true;
            this.btnWhois.Text = "Whois";
            this.btnWhois.UseVisualStyleBackColor = true;
            // 
            // btnH9
            // 
            this.btnH9.AutoSize = true;
            this.btnH9.Location = new System.Drawing.Point(12, 37);
            this.btnH9.Name = "btnH9";
            this.btnH9.Size = new System.Drawing.Size(67, 19);
            this.btnH9.TabIndex = 3;
            this.btnH9.Text = "Http 0.9";
            this.btnH9.UseVisualStyleBackColor = true;
            // 
            // btnH0
            // 
            this.btnH0.AutoSize = true;
            this.btnH0.Location = new System.Drawing.Point(12, 62);
            this.btnH0.Name = "btnH0";
            this.btnH0.Size = new System.Drawing.Size(67, 19);
            this.btnH0.TabIndex = 4;
            this.btnH0.Text = "Http 1.0";
            this.btnH0.UseVisualStyleBackColor = true;
            // 
            // btnH1
            // 
            this.btnH1.AutoSize = true;
            this.btnH1.Location = new System.Drawing.Point(12, 87);
            this.btnH1.Name = "btnH1";
            this.btnH1.Size = new System.Drawing.Size(67, 19);
            this.btnH1.TabIndex = 5;
            this.btnH1.Text = "Http 1.1";
            this.btnH1.UseVisualStyleBackColor = true;
            // 
            // txtbxServer
            // 
            this.txtbxServer.Location = new System.Drawing.Point(176, 13);
            this.txtbxServer.Name = "txtbxServer";
            this.txtbxServer.Size = new System.Drawing.Size(144, 23);
            this.txtbxServer.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Timeout";
            // 
            // txtbxTimeout
            // 
            this.txtbxTimeout.Location = new System.Drawing.Point(176, 41);
            this.txtbxTimeout.Name = "txtbxTimeout";
            this.txtbxTimeout.Size = new System.Drawing.Size(144, 23);
            this.txtbxTimeout.TabIndex = 9;
            this.txtbxTimeout.TextChanged += new System.EventHandler(this.txtbxTimeout_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtArgs
            // 
            this.txtArgs.Location = new System.Drawing.Point(85, 146);
            this.txtArgs.Name = "txtArgs";
            this.txtArgs.Size = new System.Drawing.Size(138, 23);
            this.txtArgs.TabIndex = 12;
            this.txtArgs.TextChanged += new System.EventHandler(this.txtArgs_TextChanged);
            // 
            // lblArgs
            // 
            this.lblArgs.AutoSize = true;
            this.lblArgs.Location = new System.Drawing.Point(39, 149);
            this.lblArgs.Name = "lblArgs";
            this.lblArgs.Size = new System.Drawing.Size(31, 15);
            this.lblArgs.TabIndex = 11;
            this.lblArgs.Text = "Data";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(229, 143);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 26);
            this.button2.TabIndex = 13;
            this.button2.Text = "Send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblEmpty
            // 
            this.lblEmpty.AutoSize = true;
            this.lblEmpty.ForeColor = System.Drawing.Color.Red;
            this.lblEmpty.Location = new System.Drawing.Point(85, 128);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(127, 15);
            this.lblEmpty.TabIndex = 14;
            this.lblEmpty.Text = "Field Cannot be empty";
            this.lblEmpty.Visible = false;
            // 
            // GraphicalUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 243);
            this.Controls.Add(this.lblEmpty);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtArgs);
            this.Controls.Add(this.lblArgs);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtbxTimeout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbxServer);
            this.Controls.Add(this.btnH1);
            this.Controls.Add(this.btnH0);
            this.Controls.Add(this.btnH9);
            this.Controls.Add(this.btnWhois);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtxtServerOuput);
            this.Name = "GraphicalUI";
            this.Text = "Location Client";
            this.Load += new System.EventHandler(this.GraphicalUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtServerOuput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton btnWhois;
        private System.Windows.Forms.RadioButton btnH9;
        private System.Windows.Forms.RadioButton btnH0;
        private System.Windows.Forms.RadioButton btnH1;
        private System.Windows.Forms.TextBox txtbxServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbxTimeout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtArgs;
        private System.Windows.Forms.Label lblArgs;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblEmpty;
    }
}