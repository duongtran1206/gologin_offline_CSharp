namespace gologin_offline
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
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            config = new Button();
            button3 = new Button();
            button4 = new Button();
            EditProxy = new Button();
            CleanButton = new Button();
            button8 = new Button();
            button5 = new Button();
            uploadtoGdriver = new Button();
            LogsBox = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            creatProfile = new Button();
            button2 = new Button();
            Test = new Button();
            label4 = new Label();
            label5 = new Label();
            jHashID = new Label();
            jUpdatebutton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = SystemColors.ControlLightLight;
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle11.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle11;
            dataGridView1.Location = new Point(255, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = SystemColors.Control;
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ShowEditingIcon = false;
            dataGridView1.Size = new Size(533, 235);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.DoubleClick += dataGridView1_DoubleClick;
            // 
            // button1
            // 
            button1.Location = new Point(12, 30);
            button1.Name = "button1";
            button1.Size = new Size(96, 23);
            button1.TabIndex = 1;
            button1.Text = "Update Profile";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_click;
            // 
            // config
            // 
            config.BackColor = Color.FromArgb(192, 255, 192);
            config.Location = new Point(132, 154);
            config.Name = "config";
            config.Size = new Size(96, 23);
            config.TabIndex = 2;
            config.Text = "Config";
            config.UseVisualStyleBackColor = false;
            config.Click += config_Click;
            // 
            // button3
            // 
            button3.Location = new Point(132, 30);
            button3.Name = "button3";
            button3.Size = new Size(96, 23);
            button3.TabIndex = 3;
            button3.Text = "Load Local";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(128, 255, 128);
            button4.Location = new Point(12, 73);
            button4.Name = "button4";
            button4.Size = new Size(96, 23);
            button4.TabIndex = 4;
            button4.Text = "Start Profile";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // EditProxy
            // 
            EditProxy.Location = new Point(12, 116);
            EditProxy.Name = "EditProxy";
            EditProxy.Size = new Size(96, 23);
            EditProxy.TabIndex = 6;
            EditProxy.Text = "Edit Proxy";
            EditProxy.UseVisualStyleBackColor = true;
            EditProxy.Click += EditProxy_Click;
            // 
            // CleanButton
            // 
            CleanButton.Location = new Point(132, 116);
            CleanButton.Name = "CleanButton";
            CleanButton.Size = new Size(96, 23);
            CleanButton.TabIndex = 7;
            CleanButton.Text = "Clean Profile";
            CleanButton.UseVisualStyleBackColor = true;
            CleanButton.Click += CleanButton_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(255, 128, 128);
            button8.Location = new Point(12, 154);
            button8.Name = "button8";
            button8.Size = new Size(96, 23);
            button8.TabIndex = 8;
            button8.Text = "Delete Selected";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(255, 128, 128);
            button5.Location = new Point(132, 73);
            button5.Name = "button5";
            button5.Size = new Size(96, 23);
            button5.TabIndex = 9;
            button5.Text = "Stop All";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // uploadtoGdriver
            // 
            uploadtoGdriver.Location = new Point(12, 190);
            uploadtoGdriver.Name = "uploadtoGdriver";
            uploadtoGdriver.Size = new Size(216, 23);
            uploadtoGdriver.TabIndex = 10;
            uploadtoGdriver.Text = "Upload to Gdriver(developing)";
            uploadtoGdriver.UseVisualStyleBackColor = true;
            // 
            // LogsBox
            // 
            LogsBox.Location = new Point(255, 296);
            LogsBox.Name = "LogsBox";
            LogsBox.ReadOnly = true;
            LogsBox.Size = new Size(533, 142);
            LogsBox.TabIndex = 11;
            LogsBox.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(490, 7);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 12;
            label1.Text = "Data Table";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(502, 273);
            label2.Name = "label2";
            label2.Size = new Size(42, 20);
            label2.TabIndex = 13;
            label2.Text = "Logs";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(12, 7);
            label3.Name = "label3";
            label3.Size = new Size(299, 17);
            label3.TabIndex = 14;
            label3.Text = "Start Gologin and Close Gologin before using tool !!!";
            // 
            // creatProfile
            // 
            creatProfile.BackColor = SystemColors.ActiveCaption;
            creatProfile.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            creatProfile.Location = new Point(13, 372);
            creatProfile.Name = "creatProfile";
            creatProfile.Size = new Size(215, 31);
            creatProfile.TabIndex = 15;
            creatProfile.Text = "Creat Profile";
            creatProfile.UseVisualStyleBackColor = false;
            creatProfile.Visible = false;
            creatProfile.Click += creatProfile_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(13, 343);
            button2.Name = "button2";
            button2.Size = new Size(216, 23);
            button2.TabIndex = 16;
            button2.Text = "Import list profile name";
            button2.UseVisualStyleBackColor = false;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // Test
            // 
            Test.Location = new Point(13, 231);
            Test.Name = "Test";
            Test.Size = new Size(95, 23);
            Test.TabIndex = 17;
            Test.Text = "Test";
            Test.UseVisualStyleBackColor = true;
            Test.Visible = false;
            Test.Click += Test_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 423);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 18;
            label4.Text = "Hash ID :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(70, 426);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 19;
            // 
            // jHashID
            // 
            jHashID.AutoSize = true;
            jHashID.Location = new Point(70, 423);
            jHashID.Name = "jHashID";
            jHashID.Size = new Size(38, 15);
            jHashID.TabIndex = 20;
            jHashID.Text = "label6";
            // 
            // jUpdatebutton
            // 
            jUpdatebutton.Location = new Point(14, 280);
            jUpdatebutton.Name = "jUpdatebutton";
            jUpdatebutton.Size = new Size(120, 23);
            jUpdatebutton.TabIndex = 21;
            jUpdatebutton.Text = "Check for Update";
            jUpdatebutton.UseVisualStyleBackColor = true;
            jUpdatebutton.Click += jUpdatebutton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(jUpdatebutton);
            Controls.Add(jHashID);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Test);
            Controls.Add(button2);
            Controls.Add(creatProfile);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LogsBox);
            Controls.Add(uploadtoGdriver);
            Controls.Add(button5);
            Controls.Add(button8);
            Controls.Add(CleanButton);
            Controls.Add(EditProxy);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(config);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Gologin Offline";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button config;
        private Button button3;
        private Button button4;
        private Button EditProxy;
        private Button CleanButton;
        private Button button8;
        private Button button5;
        private Button uploadtoGdriver;
        private RichTextBox LogsBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button creatProfile;
        private Button button2;
        private Button Test;
        private Label label4;
        private Label label5;
        private Label jHashID;
        private Button jUpdatebutton;
    }
}
