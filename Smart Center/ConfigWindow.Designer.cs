namespace Smart_Center
{
    partial class ConfigWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbSQL = new System.Windows.Forms.RadioButton();
            this.rbWindoes = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtPWD);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.rbSQL);
            this.groupBox1.Controls.Add(this.rbWindoes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 365);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "زانیاریێن سێرڤه‌ری";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(187, 294);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 50);
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "گرتن";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(54, 294);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(122, 50);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "هه‌لگرتن";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(54, 237);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.ReadOnly = true;
            this.txtPWD.Size = new System.Drawing.Size(219, 20);
            this.txtPWD.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(279, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "ژمارا نهێنی";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(54, 197);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(219, 20);
            this.txtID.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(279, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "ناڤێ بكارهێنه‌ری";
            // 
            // rbSQL
            // 
            this.rbSQL.AutoSize = true;
            this.rbSQL.Location = new System.Drawing.Point(57, 146);
            this.rbSQL.Name = "rbSQL";
            this.rbSQL.Size = new System.Drawing.Size(151, 17);
            this.rbSQL.TabIndex = 19;
            this.rbSQL.Text = "SQL Server Authentication";
            this.rbSQL.UseVisualStyleBackColor = true;
            // 
            // rbWindoes
            // 
            this.rbWindoes.AutoSize = true;
            this.rbWindoes.Checked = true;
            this.rbWindoes.Location = new System.Drawing.Point(57, 108);
            this.rbWindoes.Name = "rbWindoes";
            this.rbWindoes.Size = new System.Drawing.Size(140, 17);
            this.rbWindoes.TabIndex = 18;
            this.rbWindoes.TabStop = true;
            this.rbWindoes.Text = "Windows Authentication";
            this.rbWindoes.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "رێكا داخل بوونێ";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(54, 59);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(219, 20);
            this.txtDatabase.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "داتابێس:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(54, 19);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(219, 20);
            this.txtServer.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "ناڤێ سێرڤه‌ری:";
            // 
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 387);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfigWindow";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "رێكخستن";
            this.Load += new System.EventHandler(this.ConfigWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbSQL;
        private System.Windows.Forms.RadioButton rbWindoes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
    }
}