namespace ProcSym
{
    partial class Main
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
            button1 = new Button();
            listBox1 = new ListBox();
            runrbtn = new Button();
            openFolderBtn = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(82, 158);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(110, 44);
            button1.TabIndex = 10;
            button1.Text = "Remove";
            button1.UseVisualStyleBackColor = true;
            button1.Click += RemoveButtonClick;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(24, 30, 54);
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.ForeColor = SystemColors.HotTrack;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(237, 93);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(482, 262);
            listBox1.TabIndex = 9;
            // 
            // runrbtn
            // 
            runrbtn.Location = new Point(82, 314);
            runrbtn.Margin = new Padding(3, 4, 3, 4);
            runrbtn.Name = "runrbtn";
            runrbtn.Size = new Size(110, 43);
            runrbtn.TabIndex = 8;
            runrbtn.Text = "RUN";
            runrbtn.UseVisualStyleBackColor = true;
            runrbtn.Click += Runrbtn_Click;
            // 
            // openFolderBtn
            // 
            openFolderBtn.Location = new Point(82, 93);
            openFolderBtn.Margin = new Padding(3, 4, 3, 4);
            openFolderBtn.Name = "openFolderBtn";
            openFolderBtn.Size = new Size(110, 44);
            openFolderBtn.TabIndex = 7;
            openFolderBtn.Text = "Add Files";
            openFolderBtn.UseVisualStyleBackColor = true;
            openFolderBtn.Click += OpenFolderBtn_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 30, 54);
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Controls.Add(runrbtn);
            Controls.Add(openFolderBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Main";
            Text = "ProcSym";
            FormClosing += Main_Close;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private ListBox listBox1;
        private Button runrbtn;
        private Button openFolderBtn;
    }
}
