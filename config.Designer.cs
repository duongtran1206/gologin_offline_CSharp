namespace gologin_offline
{
    partial class config
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
            Token = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            TokenBox = new TextBox();
            LocalPathBox = new TextBox();
            ChromeDriverBox = new TextBox();
            Proxy6APIBOX = new TextBox();
            SaveButton = new Button();
            SuspendLayout();
            // 
            // Token
            // 
            Token.AutoSize = true;
            Token.ForeColor = SystemColors.ActiveCaptionText;
            Token.Location = new Point(22, 31);
            Token.Name = "Token";
            Token.Size = new Size(38, 15);
            Token.TabIndex = 0;
            Token.Text = "Token";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(22, 72);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 1;
            label2.Text = "Local_Path";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.Location = new Point(22, 105);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 2;
            label3.Text = "Chrome_Driver";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.Location = new Point(22, 153);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 3;
            label4.Text = "Proxy6_API";
            // 
            // TokenBox
            // 
            TokenBox.Location = new Point(125, 23);
            TokenBox.Name = "TokenBox";
            TokenBox.Size = new Size(215, 23);
            TokenBox.TabIndex = 4;
            // 
            // LocalPathBox
            // 
            LocalPathBox.Location = new Point(125, 64);
            LocalPathBox.Name = "LocalPathBox";
            LocalPathBox.Size = new Size(215, 23);
            LocalPathBox.TabIndex = 5;
            // 
            // ChromeDriverBox
            // 
            ChromeDriverBox.Location = new Point(125, 102);
            ChromeDriverBox.Name = "ChromeDriverBox";
            ChromeDriverBox.Size = new Size(215, 23);
            ChromeDriverBox.TabIndex = 6;
            // 
            // Proxy6APIBOX
            // 
            Proxy6APIBOX.Location = new Point(125, 145);
            Proxy6APIBOX.Name = "Proxy6APIBOX";
            Proxy6APIBOX.Size = new Size(215, 23);
            Proxy6APIBOX.TabIndex = 7;
            // 
            // SaveButton
            // 
            SaveButton.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            SaveButton.ForeColor = SystemColors.ActiveCaptionText;
            SaveButton.Location = new Point(158, 181);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(75, 34);
            SaveButton.TabIndex = 8;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // config
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 227);
            Controls.Add(SaveButton);
            Controls.Add(Proxy6APIBOX);
            Controls.Add(ChromeDriverBox);
            Controls.Add(LocalPathBox);
            Controls.Add(TokenBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(Token);
            ForeColor = SystemColors.ControlDarkDark;
            Name = "config";
            Text = "config";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Token;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox TokenBox;
        private TextBox LocalPathBox;
        private TextBox ChromeDriverBox;
        private TextBox Proxy6APIBOX;
        private Button SaveButton;
    }
}