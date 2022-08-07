using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nicholas_s_Discord_Bot
{
    public partial class ReactVerifyForm : Form
    {
        public string channelId = "";
        public string messageId = "";
        public string emoji = "";
        public string emojiId = "";
        public bool isCustomEmoji = false;
        public Form1 form;
        public ReactVerifyForm()
        {
            InitializeComponent();
        }

        private void ReactVerifForm_Load(object sender, EventArgs e)
        {
            TestLabel.Focus();
            var jsonData = JObject.Parse(File.ReadAllText(Environment.CurrentDirectory + @"\" + "sessionData"));

            if(jsonData["reactVerify"] != null)
            {
                if (jsonData["reactVerify"]["channelId"] != null)
                {
                    channelId = jsonData["reactVerify"]["channelId"].ToString();

                    if (channelId.Trim() != "")
                    {
                        ChannelIdTextBox.Text = channelId;
                        ChannelIdTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                    }
                }

                if (jsonData["reactVerify"]["messageId"] != null)
                {
                    messageId = jsonData["reactVerify"]["messageId"].ToString();
                    if(messageId.Trim() != "")
                    {
                        MessageIdTextBox.Text = messageId;
                        MessageIdTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                    }
                }

                if (jsonData["reactVerify"]["emoji"] != null)
                {
                    emoji = jsonData["reactVerify"]["emoji"].ToString();

                    if (emoji.Trim() != "")
                    {
                        EmojiNameTextBox.Text = emoji;
                        EmojiNameTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                    }
                }

                if (jsonData["reactVerify"]["isCustomEmoji"] != null)
                {
                    isCustomEmoji = (bool)jsonData["reactVerify"]["isCustomEmoji"];

                    if (isCustomEmoji)
                    {
                        IsCustomEmojiCheckBox.Checked = true;
                        IsCustomEmojiCheckBox_CheckedChanged(this, EventArgs.Empty);
                    }
                }

                if (jsonData["reactVerify"]["emojiId"] != null)
                {
                    emojiId = jsonData["reactVerify"]["emojiId"].ToString();

                    if (emojiId.Trim() != "")
                    {
                        EmojiIdTextBox.Text = emojiId;
                        EmojiIdTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                    }
                }

              
            }

            form.reactSetting.emojiName = emoji;
            form.reactSetting.emojiId = emojiId;
            form.reactSetting.channelId = channelId;
            form.reactSetting.messageId = messageId;
            form.reactSetting.isCustomEmoji = isCustomEmoji;
          
        }

        #region Channel ID Text Box
        private void ChannelIdTextBox_Enter(object sender, EventArgs e)
        {
            if (ChannelIdTextBox.Text == "Enter Channel Id... ")
            {
                ChannelIdTextBox.Text = "";
                ChannelIdTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }

        private void ChannelIdTextBox_Leave(object sender, EventArgs e)
        {
            if (ChannelIdTextBox.Text.Trim().Length == 0)
            {
                ChannelIdTextBox.Text = "Enter Channel Id... ";
                channelId = "";
                ChannelIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (ChannelIdTextBox.Text == "Enter Channel Id... ")
                {
                    channelId = "";
                    ChannelIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }

        private void ChannelIdTextBox_TextChanged(object sender, EventArgs e)
        {
            channelId = ChannelIdTextBox.Text.Trim();

            setReactSetting();
        }
        #endregion

        #region Message ID Text Box
        private void MessageIdTextBox_TextChanged(object sender, EventArgs e)
        {
            messageId = MessageIdTextBox.Text.Trim();

            setReactSetting();
        }

        private void MessageIdTextBox_Enter(object sender, EventArgs e)
        {
            if (MessageIdTextBox.Text == "Enter Message Id... ")
            {
                MessageIdTextBox.Text = "";
                MessageIdTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }

        private void MessageIdTextBox_Leave(object sender, EventArgs e)
        {
            if (MessageIdTextBox.Text.Trim().Length == 0)
            {
                MessageIdTextBox.Text = "Enter Message Id... ";
                messageId = "";
                MessageIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (MessageIdTextBox.Text == "Enter Message Id... ")
                {
                    messageId = "";
                    MessageIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }
        #endregion

        #region Emoji Name Text Box
        private void EmojiNameTextBox_Leave(object sender, EventArgs e)
        {
            if (EmojiNameTextBox.Text.Trim().Length == 0)
            {
                EmojiNameTextBox.Text = "Enter Emoji... ";
                emoji = "";
                EmojiNameTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (EmojiNameTextBox.Text == "Enter Emoji... ")
                {
                    emoji = "";
                    EmojiNameTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }

        private void EmojiNameTextBox_TextChanged(object sender, EventArgs e)
        {
            emoji = EmojiNameTextBox.Text.Trim();

            setReactSetting();
        }

        private void EmojiNameTextBox_Enter(object sender, EventArgs e)
        {
            if (EmojiNameTextBox.Text == "Enter Emoji... ")
            {
                EmojiNameTextBox.Text = "";
                EmojiNameTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }
        #endregion

        #region Emoji ID Text Box
        private void EmojiIdTextBox_TextChanged(object sender, EventArgs e)
        {
            emojiId = EmojiIdTextBox.Text.Trim();

            setReactSetting();
        }

        private void EmojiIdTextBox_Enter(object sender, EventArgs e)
        {
            if (EmojiIdTextBox.Text == "Enter Emoji's Id... ")
            {
                EmojiIdTextBox.Text = "";
                EmojiIdTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }

        private void EmojiIdTextBox_Leave(object sender, EventArgs e)
        {
            if (EmojiIdTextBox.Text.Trim().Length == 0)
            {
                EmojiIdTextBox.Text = "Enter Emoji's Id... ";
                emojiId = "";
                EmojiIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (EmojiIdTextBox.Text == "Enter Emoji's Id... ")
                {
                    emojiId = "";
                    EmojiIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }

        #endregion

        public void setReactSetting()
        {
            form.reactSetting.emojiName = emoji;
            form.reactSetting.emojiId = emojiId;
            form.reactSetting.channelId = channelId;
            form.reactSetting.messageId = messageId;
            form.reactSetting.isCustomEmoji = isCustomEmoji;
        }
        private void IsCustomEmojiCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isCustomEmoji = IsCustomEmojiCheckBox.Checked;
            if(isCustomEmoji)
            {
                EmojiIdTextBox.Visible = true;
                emojiId = "";
                EmojiIdTextBox.Text = "Enter Emoji's Id... ";
                EmojiIdTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else if(!isCustomEmoji)
            {
                EmojiIdTextBox.Visible = false;
            }

            setReactSetting();
        }

        private void ReactVerifyForm_Click(object sender, EventArgs e)
        {
            TestLabel.Focus();
        }

        private void ReactVerifyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.reactSetting.emojiName = emoji;
            form.reactSetting.emojiId = emojiId;
            form.reactSetting.channelId = channelId;
            form.reactSetting.messageId = messageId;
            form.reactSetting.isCustomEmoji = isCustomEmoji;
          
        }
    }
}
