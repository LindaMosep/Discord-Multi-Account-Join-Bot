namespace Nicholas_s_Discord_Bot
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
            this.SelectTokenButton = new System.Windows.Forms.Button();
            this.SelectProxyButton = new System.Windows.Forms.Button();
            this.LoadText = new System.Windows.Forms.Label();
            this.CaptchaKeyTextBox = new System.Windows.Forms.TextBox();
            this.TokenCountText = new System.Windows.Forms.Label();
            this.ProxyCountText = new System.Windows.Forms.Label();
            this.JoinButton = new System.Windows.Forms.Button();
            this.InviteLinkTextBox = new System.Windows.Forms.TextBox();
            this.SetUserButton = new System.Windows.Forms.Button();
            this.ClearConsoleButton = new System.Windows.Forms.Button();
            this.MainTokenTextBox = new System.Windows.Forms.TextBox();
            this.MainTokenSubmitButton = new System.Windows.Forms.Button();
            this.MainUserPictureBox = new System.Windows.Forms.PictureBox();
            this.MainUserNameLabel = new System.Windows.Forms.Label();
            this.GuildImageBox = new System.Windows.Forms.PictureBox();
            this.SubmitInviteButton = new System.Windows.Forms.Button();
            this.GuildTextBox = new System.Windows.Forms.Label();
            this.SaveStateButton = new System.Windows.Forms.Button();
            this.ClearStateButton = new System.Windows.Forms.Button();
            this.ClearComboBox = new System.Windows.Forms.ComboBox();
            this.SaveComboBox = new System.Windows.Forms.ComboBox();
            this.UserCountText = new System.Windows.Forms.Label();
            this.ClearTokenButton = new System.Windows.Forms.Button();
            this.ClearProxyButton = new System.Windows.Forms.Button();
            this.ClearUserButton = new System.Windows.Forms.Button();
            this.CaptchaBalanceButton = new System.Windows.Forms.Button();
            this.UseProxyCheckBox = new System.Windows.Forms.CheckBox();
            this.ReactVerifyButton = new System.Windows.Forms.Button();
            this.VerifyWithReactCheckBox = new System.Windows.Forms.CheckBox();
            this.JoinDelayTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainUserPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuildImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectTokenButton
            // 
            this.SelectTokenButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SelectTokenButton.Location = new System.Drawing.Point(817, 12);
            this.SelectTokenButton.Name = "SelectTokenButton";
            this.SelectTokenButton.Size = new System.Drawing.Size(103, 55);
            this.SelectTokenButton.TabIndex = 0;
            this.SelectTokenButton.Text = "Select Tokens";
            this.SelectTokenButton.UseVisualStyleBackColor = true;
            this.SelectTokenButton.Click += new System.EventHandler(this.SelectTokenButton_Click);
            // 
            // SelectProxyButton
            // 
            this.SelectProxyButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SelectProxyButton.Location = new System.Drawing.Point(676, 12);
            this.SelectProxyButton.Name = "SelectProxyButton";
            this.SelectProxyButton.Size = new System.Drawing.Size(103, 55);
            this.SelectProxyButton.TabIndex = 1;
            this.SelectProxyButton.Text = "Select Proxies";
            this.SelectProxyButton.UseVisualStyleBackColor = true;
            this.SelectProxyButton.Click += new System.EventHandler(this.SelectProxyButton_Click);
            // 
            // LoadText
            // 
            this.LoadText.AutoSize = true;
            this.LoadText.Font = new System.Drawing.Font("Courier New", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LoadText.ForeColor = System.Drawing.Color.Red;
            this.LoadText.Location = new System.Drawing.Point(906, 577);
            this.LoadText.Name = "LoadText";
            this.LoadText.Size = new System.Drawing.Size(177, 37);
            this.LoadText.TabIndex = 2;
            this.LoadText.Text = "LoadText";
            // 
            // CaptchaKeyTextBox
            // 
            this.CaptchaKeyTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CaptchaKeyTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.CaptchaKeyTextBox.Location = new System.Drawing.Point(747, 294);
            this.CaptchaKeyTextBox.Multiline = true;
            this.CaptchaKeyTextBox.Name = "CaptchaKeyTextBox";
            this.CaptchaKeyTextBox.Size = new System.Drawing.Size(207, 55);
            this.CaptchaKeyTextBox.TabIndex = 3;
            this.CaptchaKeyTextBox.Text = "Enter Captcha Key... ";
            this.CaptchaKeyTextBox.TextChanged += new System.EventHandler(this.CaptchaKeyTextBox_TextChanged);
            this.CaptchaKeyTextBox.Enter += new System.EventHandler(this.CaptchaKeyTextBox_GotFocus);
            this.CaptchaKeyTextBox.Leave += new System.EventHandler(this.CaptchaKeyTextBox_LostFocus);
            // 
            // TokenCountText
            // 
            this.TokenCountText.AutoSize = true;
            this.TokenCountText.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TokenCountText.ForeColor = System.Drawing.Color.LimeGreen;
            this.TokenCountText.Location = new System.Drawing.Point(12, 551);
            this.TokenCountText.Name = "TokenCountText";
            this.TokenCountText.Size = new System.Drawing.Size(194, 25);
            this.TokenCountText.TabIndex = 4;
            this.TokenCountText.Text = "Token Count: 0";
            // 
            // ProxyCountText
            // 
            this.ProxyCountText.AutoSize = true;
            this.ProxyCountText.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ProxyCountText.ForeColor = System.Drawing.Color.LimeGreen;
            this.ProxyCountText.Location = new System.Drawing.Point(12, 589);
            this.ProxyCountText.Name = "ProxyCountText";
            this.ProxyCountText.Size = new System.Drawing.Size(194, 25);
            this.ProxyCountText.TabIndex = 5;
            this.ProxyCountText.Text = "Proxy Count: 0";
            // 
            // JoinButton
            // 
            this.JoinButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.JoinButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.JoinButton.Location = new System.Drawing.Point(3, 7);
            this.JoinButton.Name = "JoinButton";
            this.JoinButton.Size = new System.Drawing.Size(261, 55);
            this.JoinButton.TabIndex = 6;
            this.JoinButton.Text = "Start join!";
            this.JoinButton.UseVisualStyleBackColor = true;
            this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
            // 
            // InviteLinkTextBox
            // 
            this.InviteLinkTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InviteLinkTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.InviteLinkTextBox.Location = new System.Drawing.Point(747, 377);
            this.InviteLinkTextBox.Multiline = true;
            this.InviteLinkTextBox.Name = "InviteLinkTextBox";
            this.InviteLinkTextBox.Size = new System.Drawing.Size(207, 55);
            this.InviteLinkTextBox.TabIndex = 7;
            this.InviteLinkTextBox.Text = "Enter Invite Url... ";
            this.InviteLinkTextBox.TextChanged += new System.EventHandler(this.InviteLinkTextBox_TextChanged);
            this.InviteLinkTextBox.Enter += new System.EventHandler(this.InviteLinkTextBox_GotFocus);
            this.InviteLinkTextBox.Leave += new System.EventHandler(this.InviteLinkTextBox_LostFocus);
            // 
            // SetUserButton
            // 
            this.SetUserButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SetUserButton.Location = new System.Drawing.Point(536, 13);
            this.SetUserButton.Name = "SetUserButton";
            this.SetUserButton.Size = new System.Drawing.Size(103, 54);
            this.SetUserButton.TabIndex = 0;
            this.SetUserButton.Text = "Set users";
            this.SetUserButton.Click += new System.EventHandler(this.SetUserButton_Click);
            // 
            // ClearConsoleButton
            // 
            this.ClearConsoleButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ClearConsoleButton.Location = new System.Drawing.Point(12, 434);
            this.ClearConsoleButton.Name = "ClearConsoleButton";
            this.ClearConsoleButton.Size = new System.Drawing.Size(104, 54);
            this.ClearConsoleButton.TabIndex = 8;
            this.ClearConsoleButton.Text = "Clear Console";
            this.ClearConsoleButton.Click += new System.EventHandler(this.ClearConsoleButton_Click);
            // 
            // MainTokenTextBox
            // 
            this.MainTokenTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainTokenTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.MainTokenTextBox.Location = new System.Drawing.Point(747, 467);
            this.MainTokenTextBox.Multiline = true;
            this.MainTokenTextBox.Name = "MainTokenTextBox";
            this.MainTokenTextBox.Size = new System.Drawing.Size(207, 80);
            this.MainTokenTextBox.TabIndex = 9;
            this.MainTokenTextBox.Text = "Enter \r\nMain \r\nToken... ";
            this.MainTokenTextBox.TextChanged += new System.EventHandler(this.MainTokenTextBox_TextChanged);
            this.MainTokenTextBox.Enter += new System.EventHandler(this.MainTokenTextBox_GotFocus);
            this.MainTokenTextBox.Leave += new System.EventHandler(this.MainTokenTextBox_LostFocus);
            // 
            // MainTokenSubmitButton
            // 
            this.MainTokenSubmitButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainTokenSubmitButton.Location = new System.Drawing.Point(980, 467);
            this.MainTokenSubmitButton.Name = "MainTokenSubmitButton";
            this.MainTokenSubmitButton.Size = new System.Drawing.Size(103, 80);
            this.MainTokenSubmitButton.TabIndex = 10;
            this.MainTokenSubmitButton.Text = "Submit Main Token";
            this.MainTokenSubmitButton.UseVisualStyleBackColor = true;
            this.MainTokenSubmitButton.Click += new System.EventHandler(this.MainTokenSubmitButton_Click);
            // 
            // MainUserPictureBox
            // 
            this.MainUserPictureBox.Location = new System.Drawing.Point(12, 281);
            this.MainUserPictureBox.Name = "MainUserPictureBox";
            this.MainUserPictureBox.Size = new System.Drawing.Size(128, 128);
            this.MainUserPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MainUserPictureBox.TabIndex = 11;
            this.MainUserPictureBox.TabStop = false;
            // 
            // MainUserNameLabel
            // 
            this.MainUserNameLabel.AutoSize = true;
            this.MainUserNameLabel.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainUserNameLabel.ForeColor = System.Drawing.Color.Blue;
            this.MainUserNameLabel.Location = new System.Drawing.Point(12, 242);
            this.MainUserNameLabel.Name = "MainUserNameLabel";
            this.MainUserNameLabel.Size = new System.Drawing.Size(0, 25);
            this.MainUserNameLabel.TabIndex = 12;
            // 
            // GuildImageBox
            // 
            this.GuildImageBox.ErrorImage = null;
            this.GuildImageBox.Location = new System.Drawing.Point(955, 12);
            this.GuildImageBox.Name = "GuildImageBox";
            this.GuildImageBox.Size = new System.Drawing.Size(128, 128);
            this.GuildImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GuildImageBox.TabIndex = 13;
            this.GuildImageBox.TabStop = false;
            // 
            // SubmitInviteButton
            // 
            this.SubmitInviteButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SubmitInviteButton.Location = new System.Drawing.Point(980, 377);
            this.SubmitInviteButton.Name = "SubmitInviteButton";
            this.SubmitInviteButton.Size = new System.Drawing.Size(103, 55);
            this.SubmitInviteButton.TabIndex = 14;
            this.SubmitInviteButton.Text = "Submit Invite";
            this.SubmitInviteButton.UseVisualStyleBackColor = true;
            this.SubmitInviteButton.Click += new System.EventHandler(this.SubmitInviteButton_click);
            // 
            // GuildTextBox
            // 
            this.GuildTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GuildTextBox.Font = new System.Drawing.Font("Courier New", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.GuildTextBox.ForeColor = System.Drawing.Color.Red;
            this.GuildTextBox.Location = new System.Drawing.Point(922, 152);
            this.GuildTextBox.Name = "GuildTextBox";
            this.GuildTextBox.Size = new System.Drawing.Size(161, 139);
            this.GuildTextBox.TabIndex = 15;
            this.GuildTextBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SaveStateButton
            // 
            this.SaveStateButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveStateButton.Location = new System.Drawing.Point(149, 128);
            this.SaveStateButton.Name = "SaveStateButton";
            this.SaveStateButton.Size = new System.Drawing.Size(115, 54);
            this.SaveStateButton.TabIndex = 16;
            this.SaveStateButton.Text = "Save Data";
            this.SaveStateButton.Click += new System.EventHandler(this.SaveStateButton_Click);
            // 
            // ClearStateButton
            // 
            this.ClearStateButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ClearStateButton.Location = new System.Drawing.Point(12, 128);
            this.ClearStateButton.Name = "ClearStateButton";
            this.ClearStateButton.Size = new System.Drawing.Size(115, 54);
            this.ClearStateButton.TabIndex = 17;
            this.ClearStateButton.Text = "Clear Data";
            this.ClearStateButton.Click += new System.EventHandler(this.ClearStateButton_Click);
            // 
            // ClearComboBox
            // 
            this.ClearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClearComboBox.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ClearComboBox.FormattingEnabled = true;
            this.ClearComboBox.Items.AddRange(new object[] {
            "All",
            "Tokens",
            "Proxies",
            "Captcha",
            "Invite",
            "Main T.",
            "R.Verify"});
            this.ClearComboBox.Location = new System.Drawing.Point(12, 85);
            this.ClearComboBox.Name = "ClearComboBox";
            this.ClearComboBox.Size = new System.Drawing.Size(115, 30);
            this.ClearComboBox.TabIndex = 18;
            // 
            // SaveComboBox
            // 
            this.SaveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SaveComboBox.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveComboBox.FormattingEnabled = true;
            this.SaveComboBox.Items.AddRange(new object[] {
            "All",
            "Tokens",
            "Proxies",
            "Captcha",
            "Invite",
            "Main T.",
            "R.Verify"});
            this.SaveComboBox.Location = new System.Drawing.Point(149, 85);
            this.SaveComboBox.Name = "SaveComboBox";
            this.SaveComboBox.Size = new System.Drawing.Size(115, 30);
            this.SaveComboBox.TabIndex = 19;
            // 
            // UserCountText
            // 
            this.UserCountText.AutoSize = true;
            this.UserCountText.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UserCountText.ForeColor = System.Drawing.Color.LimeGreen;
            this.UserCountText.Location = new System.Drawing.Point(12, 512);
            this.UserCountText.Name = "UserCountText";
            this.UserCountText.Size = new System.Drawing.Size(194, 25);
            this.UserCountText.TabIndex = 20;
            this.UserCountText.Text = "Users Count: 0";
            // 
            // ClearTokenButton
            // 
            this.ClearTokenButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ClearTokenButton.Location = new System.Drawing.Point(817, 85);
            this.ClearTokenButton.Name = "ClearTokenButton";
            this.ClearTokenButton.Size = new System.Drawing.Size(103, 55);
            this.ClearTokenButton.TabIndex = 21;
            this.ClearTokenButton.Text = "Clear Tokens";
            this.ClearTokenButton.UseVisualStyleBackColor = true;
            this.ClearTokenButton.Click += new System.EventHandler(this.ClearTokenButton_Click);
            // 
            // ClearProxyButton
            // 
            this.ClearProxyButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ClearProxyButton.Location = new System.Drawing.Point(676, 85);
            this.ClearProxyButton.Name = "ClearProxyButton";
            this.ClearProxyButton.Size = new System.Drawing.Size(103, 55);
            this.ClearProxyButton.TabIndex = 22;
            this.ClearProxyButton.Text = "Clear Proxies";
            this.ClearProxyButton.UseVisualStyleBackColor = true;
            this.ClearProxyButton.Click += new System.EventHandler(this.ClearProxyButton_Click);
            // 
            // ClearUserButton
            // 
            this.ClearUserButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ClearUserButton.Location = new System.Drawing.Point(536, 85);
            this.ClearUserButton.Name = "ClearUserButton";
            this.ClearUserButton.Size = new System.Drawing.Size(103, 55);
            this.ClearUserButton.TabIndex = 23;
            this.ClearUserButton.Text = "Clear Users";
            this.ClearUserButton.UseVisualStyleBackColor = true;
            this.ClearUserButton.Click += new System.EventHandler(this.ClearUserButton_Click);
            // 
            // CaptchaBalanceButton
            // 
            this.CaptchaBalanceButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.CaptchaBalanceButton.Location = new System.Drawing.Point(980, 294);
            this.CaptchaBalanceButton.Name = "CaptchaBalanceButton";
            this.CaptchaBalanceButton.Size = new System.Drawing.Size(103, 55);
            this.CaptchaBalanceButton.TabIndex = 24;
            this.CaptchaBalanceButton.Text = "Captcha Balance";
            this.CaptchaBalanceButton.UseVisualStyleBackColor = true;
            this.CaptchaBalanceButton.Click += new System.EventHandler(this.CaptchaBalanceButton_Click);
            // 
            // UseProxyCheckBox
            // 
            this.UseProxyCheckBox.AutoSize = true;
            this.UseProxyCheckBox.Checked = true;
            this.UseProxyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseProxyCheckBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UseProxyCheckBox.Location = new System.Drawing.Point(138, 461);
            this.UseProxyCheckBox.Name = "UseProxyCheckBox";
            this.UseProxyCheckBox.Size = new System.Drawing.Size(137, 27);
            this.UseProxyCheckBox.TabIndex = 25;
            this.UseProxyCheckBox.Text = "Use Proxy";
            this.UseProxyCheckBox.UseVisualStyleBackColor = true;
            this.UseProxyCheckBox.CheckedChanged += new System.EventHandler(this.UseProxyCheckBox_CheckedChanged);
            // 
            // ReactVerifyButton
            // 
            this.ReactVerifyButton.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ReactVerifyButton.Location = new System.Drawing.Point(282, 45);
            this.ReactVerifyButton.Name = "ReactVerifyButton";
            this.ReactVerifyButton.Size = new System.Drawing.Size(147, 54);
            this.ReactVerifyButton.TabIndex = 26;
            this.ReactVerifyButton.Text = "React Settings";
            this.ReactVerifyButton.UseVisualStyleBackColor = true;
            this.ReactVerifyButton.Click += new System.EventHandler(this.ReactVerifyButton_Click);
            // 
            // VerifyWithReactCheckBox
            // 
            this.VerifyWithReactCheckBox.AutoSize = true;
            this.VerifyWithReactCheckBox.Checked = true;
            this.VerifyWithReactCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VerifyWithReactCheckBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.VerifyWithReactCheckBox.Location = new System.Drawing.Point(282, 7);
            this.VerifyWithReactCheckBox.Name = "VerifyWithReactCheckBox";
            this.VerifyWithReactCheckBox.Size = new System.Drawing.Size(233, 27);
            this.VerifyWithReactCheckBox.TabIndex = 27;
            this.VerifyWithReactCheckBox.Text = "Verify with react";
            this.VerifyWithReactCheckBox.UseVisualStyleBackColor = true;
            this.VerifyWithReactCheckBox.CheckedChanged += new System.EventHandler(this.VerifyWithReactCheckBox_CheckedChanged);
            // 
            // JoinDelayTextBox
            // 
            this.JoinDelayTextBox.Font = new System.Drawing.Font("Courier New", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.JoinDelayTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.JoinDelayTextBox.Location = new System.Drawing.Point(282, 127);
            this.JoinDelayTextBox.Multiline = true;
            this.JoinDelayTextBox.Name = "JoinDelayTextBox";
            this.JoinDelayTextBox.Size = new System.Drawing.Size(207, 55);
            this.JoinDelayTextBox.TabIndex = 28;
            this.JoinDelayTextBox.Text = "Enter join delay... ";
            this.JoinDelayTextBox.TextChanged += new System.EventHandler(this.JoinDelayTextBox_TextChanged);
            this.JoinDelayTextBox.Enter += new System.EventHandler(this.JoinDelayTextBox_GotFocus);
            this.JoinDelayTextBox.Leave += new System.EventHandler(this.JoinDelayTextBox_LostFocus);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 632);
            this.Controls.Add(this.JoinDelayTextBox);
            this.Controls.Add(this.VerifyWithReactCheckBox);
            this.Controls.Add(this.ReactVerifyButton);
            this.Controls.Add(this.UseProxyCheckBox);
            this.Controls.Add(this.CaptchaBalanceButton);
            this.Controls.Add(this.ClearUserButton);
            this.Controls.Add(this.ClearProxyButton);
            this.Controls.Add(this.ClearTokenButton);
            this.Controls.Add(this.UserCountText);
            this.Controls.Add(this.SaveComboBox);
            this.Controls.Add(this.ClearComboBox);
            this.Controls.Add(this.ClearStateButton);
            this.Controls.Add(this.SaveStateButton);
            this.Controls.Add(this.GuildTextBox);
            this.Controls.Add(this.SubmitInviteButton);
            this.Controls.Add(this.GuildImageBox);
            this.Controls.Add(this.MainUserNameLabel);
            this.Controls.Add(this.MainUserPictureBox);
            this.Controls.Add(this.MainTokenSubmitButton);
            this.Controls.Add(this.MainTokenTextBox);
            this.Controls.Add(this.ClearConsoleButton);
            this.Controls.Add(this.SetUserButton);
            this.Controls.Add(this.InviteLinkTextBox);
            this.Controls.Add(this.JoinButton);
            this.Controls.Add(this.ProxyCountText);
            this.Controls.Add(this.TokenCountText);
            this.Controls.Add(this.CaptchaKeyTextBox);
            this.Controls.Add(this.LoadText);
            this.Controls.Add(this.SelectProxyButton);
            this.Controls.Add(this.SelectTokenButton);
            this.Name = "Form1";
            this.Text = "Nicholas\'s Discord Join Bot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            ((System.ComponentModel.ISupportInitialize)(this.MainUserPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GuildImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

  







        #endregion

        private TextBox CaptchaKeyTextBox;
        private Button SelectTokenButton;
        private Label LoadText;
        private Label TokenCountText;
        private TextBox InviteLinkTextBox;
        private Label ProxyCountText;
        private Button SelectProxyButton;
        private Button JoinButton;
        private Button SetUserButton;
        private Button ClearConsoleButton;
        private TextBox MainTokenTextBox;
        private Button MainTokenSubmitButton;
        private PictureBox MainUserPictureBox;
        private Label MainUserNameLabel;
        private PictureBox GuildImageBox;
        private Button SubmitInviteButton;
        private Label GuildTextBox;
        private Button SaveStateButton;
        private Button ClearStateButton;
        private ComboBox ClearComboBox;
        private ComboBox SaveComboBox;
        private Label UserCountText;
        private Button ClearTokenButton;
        private Button ClearProxyButton;
        private Button ClearUserButton;
        private Button CaptchaBalanceButton;
        private CheckBox UseProxyCheckBox;
        private Button ReactVerifyButton;
        private CheckBox VerifyWithReactCheckBox;
        private TextBox JoinDelayTextBox;
    }
}