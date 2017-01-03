namespace RevelatorFileSwapper
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.CharacterEnableCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.GetScriptGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.enableAll = new System.Windows.Forms.CheckBox();
            this.modsEnableBox = new System.Windows.Forms.CheckBox();
            this.memp1checktimer = new System.Windows.Forms.Timer(this.components);
            this.memp2checktimer = new System.Windows.Forms.Timer(this.components);
            this.pathNotExistTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.debugtimer = new System.Windows.Forms.Timer(this.components);
            this.GetScriptGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Script";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Choose a Player",
            "Player 1",
            "Player 2",
            "Player 1 (Etc)",
            "Player 2 (Etc)"});
            this.comboBox1.Location = new System.Drawing.Point(6, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(133, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // CharacterEnableCheckedListBox
            // 
            this.CharacterEnableCheckedListBox.FormattingEnabled = true;
            this.CharacterEnableCheckedListBox.Location = new System.Drawing.Point(6, 71);
            this.CharacterEnableCheckedListBox.Name = "CharacterEnableCheckedListBox";
            this.CharacterEnableCheckedListBox.Size = new System.Drawing.Size(188, 364);
            this.CharacterEnableCheckedListBox.Sorted = true;
            this.CharacterEnableCheckedListBox.TabIndex = 2;
            this.CharacterEnableCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CharacterEnableCheckedListBox_ItemCheck);
            // 
            // GetScriptGroupBox
            // 
            this.GetScriptGroupBox.Controls.Add(this.button1);
            this.GetScriptGroupBox.Controls.Add(this.comboBox1);
            this.GetScriptGroupBox.Location = new System.Drawing.Point(12, 5);
            this.GetScriptGroupBox.Name = "GetScriptGroupBox";
            this.GetScriptGroupBox.Size = new System.Drawing.Size(147, 91);
            this.GetScriptGroupBox.TabIndex = 3;
            this.GetScriptGroupBox.TabStop = false;
            this.GetScriptGroupBox.Text = "Script Extractor";
            this.GetScriptGroupBox.Enter += new System.EventHandler(this.GetScriptGroupBox_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enableAll);
            this.groupBox1.Controls.Add(this.modsEnableBox);
            this.groupBox1.Controls.Add(this.CharacterEnableCheckedListBox);
            this.groupBox1.Location = new System.Drawing.Point(165, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 435);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mod Manager";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // enableAll
            // 
            this.enableAll.AutoSize = true;
            this.enableAll.Location = new System.Drawing.Point(7, 41);
            this.enableAll.Name = "enableAll";
            this.enableAll.Size = new System.Drawing.Size(151, 17);
            this.enableAll.TabIndex = 4;
            this.enableAll.Text = "Enable All Character Mods";
            this.enableAll.UseVisualStyleBackColor = true;
            this.enableAll.CheckedChanged += new System.EventHandler(this.enableAll_CheckedChanged);
            // 
            // modsEnableBox
            // 
            this.modsEnableBox.AutoSize = true;
            this.modsEnableBox.Location = new System.Drawing.Point(7, 20);
            this.modsEnableBox.Name = "modsEnableBox";
            this.modsEnableBox.Size = new System.Drawing.Size(88, 17);
            this.modsEnableBox.TabIndex = 3;
            this.modsEnableBox.Text = "Enable Mods";
            this.modsEnableBox.UseVisualStyleBackColor = true;
            this.modsEnableBox.CheckedChanged += new System.EventHandler(this.modsEnableBox_CheckedChanged);
            // 
            // memp1checktimer
            // 
            this.memp1checktimer.Interval = 15;
            this.memp1checktimer.Tick += new System.EventHandler(this.memchecktimer_Tick);
            // 
            // memp2checktimer
            // 
            this.memp2checktimer.Interval = 15;
            this.memp2checktimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pathNotExistTimer
            // 
            this.pathNotExistTimer.Tick += new System.EventHandler(this.pathNotExistTimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Debug Info:";
            // 
            // debugtimer
            // 
            this.debugtimer.Interval = 250;
            this.debugtimer.Tick += new System.EventHandler(this.debugtimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 441);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GetScriptGroupBox);
            this.Name = "MainForm";
            this.Text = "Revelator File Swapper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.GetScriptGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckedListBox CharacterEnableCheckedListBox;
        private System.Windows.Forms.GroupBox GetScriptGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox enableAll;
        private System.Windows.Forms.CheckBox modsEnableBox;
        private System.Windows.Forms.Timer memp1checktimer;
        private System.Windows.Forms.Timer memp2checktimer;
        private System.Windows.Forms.Timer pathNotExistTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer debugtimer;
    }
}

