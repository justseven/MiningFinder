namespace MiningFinder
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
            panel1 = new Panel();
            listBox1 = new ListBox();
            groupBox1 = new GroupBox();
            panel3 = new Panel();
            checkedListBox1 = new CheckedListBox();
            panel2 = new Panel();
            ckbDiff = new CheckBox();
            chbSelectAll = new CheckBox();
            btnCheck = new Button();
            btnImport = new Button();
            progressBar1 = new ProgressBar();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(listBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(217, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(583, 464);
            panel1.TabIndex = 0;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(583, 464);
            listBox1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.AccessibleRole = AccessibleRole.TitleBar;
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(panel2);
            groupBox1.Dock = DockStyle.Left;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(217, 464);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "服务器信息";
            // 
            // panel3
            // 
            panel3.Controls.Add(checkedListBox1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 88);
            panel3.Name = "panel3";
            panel3.Size = new Size(211, 373);
            panel3.TabIndex = 1;
            // 
            // checkedListBox1
            // 
            checkedListBox1.Dock = DockStyle.Fill;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(0, 0);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(211, 373);
            checkedListBox1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(ckbDiff);
            panel2.Controls.Add(chbSelectAll);
            panel2.Controls.Add(btnCheck);
            panel2.Controls.Add(btnImport);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(3, 19);
            panel2.Name = "panel2";
            panel2.Size = new Size(211, 69);
            panel2.TabIndex = 0;
            // 
            // ckbDiff
            // 
            ckbDiff.AutoSize = true;
            ckbDiff.Location = new Point(73, 41);
            ckbDiff.Name = "ckbDiff";
            ckbDiff.Size = new Size(51, 21);
            ckbDiff.TabIndex = 3;
            ckbDiff.Text = "反选";
            ckbDiff.UseVisualStyleBackColor = true;
            // 
            // chbSelectAll
            // 
            chbSelectAll.AutoSize = true;
            chbSelectAll.Location = new Point(16, 41);
            chbSelectAll.Name = "chbSelectAll";
            chbSelectAll.Size = new Size(51, 21);
            chbSelectAll.TabIndex = 2;
            chbSelectAll.Text = "全选";
            chbSelectAll.UseVisualStyleBackColor = true;
            chbSelectAll.CheckedChanged += chbSelectAll_CheckedChanged;
            // 
            // btnCheck
            // 
            btnCheck.Location = new Point(125, 3);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(75, 23);
            btnCheck.TabIndex = 1;
            btnCheck.Text = "检查";
            btnCheck.UseVisualStyleBackColor = true;
            btnCheck.Click += btnCheck_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(9, 3);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(75, 23);
            btnImport.TabIndex = 0;
            btnImport.Text = "导入";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // progressBar1
            // 
            progressBar1.Dock = DockStyle.Bottom;
            progressBar1.Location = new Point(0, 464);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(800, 23);
            progressBar1.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 487);
            Controls.Add(panel1);
            Controls.Add(groupBox1);
            Controls.Add(progressBar1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private Panel panel3;
        private CheckedListBox checkedListBox1;
        private Panel panel2;
        private CheckBox ckbDiff;
        private CheckBox chbSelectAll;
        private Button btnCheck;
        private Button btnImport;
        private ListBox listBox1;
        private ProgressBar progressBar1;
    }
}
