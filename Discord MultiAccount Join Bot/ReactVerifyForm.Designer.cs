namespace Nicholas_s_Discord_Bot
{
    partial class ReactVerifyForm
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
            this.ChannelIdTextBox = new System.Windows.Forms.TextBox();
            this.MessageIdTextBox = new System.Windows.Forms.TextBox();
            this.IsCustomEmojiCheckBox = new System.Windows.Forms.CheckBox();
            this.EmojiNameTextBox = new System.Windows.Forms.TextBox();
            this.EmojiIdTextBox = new System.Windows.Forms.TextBox();
            this.TestLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChannelIdTextBox
            // 
            this.ChannelIdTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ChannelIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ChannelIdTextBox.Location = new System.Drawing.Point(12, 22);
            this.ChannelIdTextBox.Multiline = true;
            this.ChannelIdTextBox.Name = "ChannelIdTextBox";
            this.ChannelIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ChannelIdTextBox.Size = new System.Drawing.Size(207, 55);
            this.ChannelIdTextBox.TabIndex = 23;
            this.ChannelIdTextBox.Text = "Enter Channel Id... ";
            this.ChannelIdTextBox.TextChanged += new System.EventHandler(this.ChannelIdTextBox_TextChanged);
            this.ChannelIdTextBox.Enter += new System.EventHandler(this.ChannelIdTextBox_Enter);
            this.ChannelIdTextBox.Leave += new System.EventHandler(this.ChannelIdTextBox_Leave);
            // 
            // MessageIdTextBox
            // 
            this.MessageIdTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MessageIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.MessageIdTextBox.Location = new System.Drawing.Point(12, 101);
            this.MessageIdTextBox.Multiline = true;
            this.MessageIdTextBox.Name = "MessageIdTextBox";
            this.MessageIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.MessageIdTextBox.Size = new System.Drawing.Size(207, 55);
            this.MessageIdTextBox.TabIndex = 25;
            this.MessageIdTextBox.Text = "Enter Message Id... ";
            this.MessageIdTextBox.TextChanged += new System.EventHandler(this.MessageIdTextBox_TextChanged);
            this.MessageIdTextBox.Enter += new System.EventHandler(this.MessageIdTextBox_Enter);
            this.MessageIdTextBox.Leave += new System.EventHandler(this.MessageIdTextBox_Leave);
            // 
            // IsCustomEmojiCheckBox
            // 
            this.IsCustomEmojiCheckBox.AutoSize = true;
            this.IsCustomEmojiCheckBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.IsCustomEmojiCheckBox.Location = new System.Drawing.Point(254, 24);
            this.IsCustomEmojiCheckBox.Name = "IsCustomEmojiCheckBox";
            this.IsCustomEmojiCheckBox.Size = new System.Drawing.Size(173, 27);
            this.IsCustomEmojiCheckBox.TabIndex = 26;
            this.IsCustomEmojiCheckBox.Text = "Custom Emoji";
            this.IsCustomEmojiCheckBox.UseVisualStyleBackColor = true;
            this.IsCustomEmojiCheckBox.CheckedChanged += new System.EventHandler(this.IsCustomEmojiCheckBox_CheckedChanged);
            // 
            // EmojiNameTextBox
            // 
            this.EmojiNameTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.EmojiNameTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.EmojiNameTextBox.Location = new System.Drawing.Point(12, 175);
            this.EmojiNameTextBox.Multiline = true;
            this.EmojiNameTextBox.Name = "EmojiNameTextBox";
            this.EmojiNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.EmojiNameTextBox.Size = new System.Drawing.Size(207, 55);
            this.EmojiNameTextBox.TabIndex = 27;
            this.EmojiNameTextBox.Text = "Enter Emoji... ";
            this.EmojiNameTextBox.TextChanged += new System.EventHandler(this.EmojiNameTextBox_TextChanged);
            this.EmojiNameTextBox.Enter += new System.EventHandler(this.EmojiNameTextBox_Enter);
            this.EmojiNameTextBox.Leave += new System.EventHandler(this.EmojiNameTextBox_Leave);
            // 
            // EmojiIdTextBox
            // 
            this.EmojiIdTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.EmojiIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.EmojiIdTextBox.Location = new System.Drawing.Point(237, 175);
            this.EmojiIdTextBox.Multiline = true;
            this.EmojiIdTextBox.Name = "EmojiIdTextBox";
            this.EmojiIdTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.EmojiIdTextBox.Size = new System.Drawing.Size(207, 55);
            this.EmojiIdTextBox.TabIndex = 28;
            this.EmojiIdTextBox.Text = "Enter Emoji\'s Id... ";
            this.EmojiIdTextBox.Visible = false;
            this.EmojiIdTextBox.TextChanged += new System.EventHandler(this.EmojiIdTextBox_TextChanged);
            this.EmojiIdTextBox.Enter += new System.EventHandler(this.EmojiIdTextBox_Enter);
            this.EmojiIdTextBox.Leave += new System.EventHandler(this.EmojiIdTextBox_Leave);
            // 
            // TestLabel
            // 
            this.TestLabel.AutoSize = true;
            this.TestLabel.Location = new System.Drawing.Point(6, 9);
            this.TestLabel.Name = "TestLabel";
            this.TestLabel.Size = new System.Drawing.Size(0, 15);
            this.TestLabel.TabIndex = 29;
            // 
            // ReactVerifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 247);
            this.Controls.Add(this.EmojiIdTextBox);
            this.Controls.Add(this.EmojiNameTextBox);
            this.Controls.Add(this.IsCustomEmojiCheckBox);
            this.Controls.Add(this.MessageIdTextBox);
            this.Controls.Add(this.ChannelIdTextBox);
            this.Controls.Add(this.TestLabel);
            this.Name = "ReactVerifyForm";
            this.Text = "React Verify Page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReactVerifyForm_FormClosing);
            this.Load += new System.EventHandler(this.ReactVerifForm_Load);
            this.Click += new System.EventHandler(this.ReactVerifyForm_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion
        private TextBox ChannelIdTextBox;
        private TextBox MessageIdTextBox;
        private CheckBox IsCustomEmojiCheckBox;
        private TextBox EmojiNameTextBox;
        private TextBox EmojiIdTextBox;
        private Label TestLabel;
    }
}