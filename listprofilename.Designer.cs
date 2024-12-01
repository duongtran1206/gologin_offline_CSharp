namespace gologin_offline
{
    partial class listprofilename
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
            listnameprofileBox = new RichTextBox();
            Submit_buton = new Button();
            SuspendLayout();
            // 
            // listnameprofileBox
            // 
            listnameprofileBox.Location = new Point(12, 12);
            listnameprofileBox.Name = "listnameprofileBox";
            listnameprofileBox.Size = new Size(212, 242);
            listnameprofileBox.TabIndex = 0;
            listnameprofileBox.Text = "";
            // 
            // Submit_buton
            // 
            Submit_buton.BackColor = SystemColors.ActiveCaption;
            Submit_buton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Submit_buton.Location = new Point(81, 260);
            Submit_buton.Name = "Submit_buton";
            Submit_buton.Size = new Size(75, 23);
            Submit_buton.TabIndex = 1;
            Submit_buton.Text = "Submit";
            Submit_buton.UseVisualStyleBackColor = false;
            Submit_buton.Click += Submit_buton_Click;
            // 
            // listprofilename
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(236, 289);
            Controls.Add(Submit_buton);
            Controls.Add(listnameprofileBox);
            Name = "listprofilename";
            Text = "listprofilename";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox listnameprofileBox;
        private Button Submit_buton;
    }
}