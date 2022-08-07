using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;
using System.Net;
using System.Text;
/*
* Developer: N*1_ AKA LindaMosep
* 
* I spend more than 40 hours lol :>
* 
* I will continue to develop more >:*
* 
*/
namespace Nicholas_s_Discord_Bot
{
    public partial class Form1 : Form
    {
        #region Classes
        public class User
        {
            public ulong? id { get; set; }
            public string? UserName { get; set; }
            public string? AvatarUrl { get; set; }
            public string? Discriminator { get; set; }
            public string? BannerUrl { get; set; }
            public string? Bio { get; set; }
            public string? Pronouns { get; set; }
            public string? Locale { get; set; }
            public bool NSFW_Allowed { get; set; }
            public bool MFA_Allowed { get; set; }
            public string? Email { get; set; }
            public bool Verified { get; set; }
            public string? Phone { get; set; }
            public string? Token { get; set; }
            public string? Password { get; set; }

            public bool Avatar_Active { get; set; }
            public bool needsAction { get; set; }

            public User(ulong id, string userName, string AvatarUrl, string Discriminator, string BannerUrl, string Bio, string Pronouns, string Locale, bool NSFW, bool MFA, string Email, bool Verified, string Phone, string Token, string Password = "null")
            {
                this.id = id;
                this.UserName = userName;
                this.AvatarUrl = AvatarUrl;

                if (AvatarUrl != null)
                {
                    if (AvatarUrl != "null" && AvatarUrl != "" && AvatarUrl.Trim() != "" && AvatarUrl.Length > 3)
                    {

                        this.AvatarUrl = "https://cdn.discordapp.com/avatars/" + id +"/" + AvatarUrl + ".png";
                        Avatar_Active = true;
                    }
                    else
                    {
                        if (Discriminator != "null")
                        {
                            this.AvatarUrl = "https://cdn.discordapp.com/embed/avatars/" + int.Parse(Discriminator) % 5 + ".png";
                        }




                        Avatar_Active = false;
                    }
                }
                else
                {

                    if (Discriminator != "null")
                    {
                        this.AvatarUrl = "https://cdn.discordapp.com/embed/avatars/" + int.Parse(Discriminator) % 5 + ".png";

                        Avatar_Active = false;
                    }





                }


                this.Discriminator = Discriminator;
                this.BannerUrl = BannerUrl;
                this.Bio = Bio;
                this.Pronouns = Pronouns;
                this.Locale = Locale;
                this.NSFW_Allowed = NSFW;
                this.MFA_Allowed = MFA;
                this.Phone = Phone;
                this.Password = Password;
                this.Email = Email;
                this.Verified = Verified;
                this.Token = Token;
                this.Password = Password;
            }

        }

        public class Invite
        {
            public string? code { get; set; }
            public string? expireDate { get; set; }
            public string? guildId { get; set; }
            public string? guildName { get; set; }
            public string? inviterId { get; set; }
            public string? inviterUsername { get; set; }
            public string? inviterDiscriminator { get; set; }

        }

        public class Guild
        {
            public string? id;
            public string? name;
            public string? icon;
            public string? description;
            public string? splash;
            public string? banner;
            public string? owner_id;
            public string? region;
            public string? vanityUrl;
            public int memberCount;
            public bool thereIsRule;
            public string ruleJson;
            public string? code;

        }

        public class ReactSettings
        {
            public string channelId { get; set; }
            public string emojiName { get; set; }
            public string messageId { get; set; }
            public string emojiId { get; set; }

            public bool isCustomEmoji = false;
        }

        #endregion

        #region Datas
        public List<string> tokens = new List<string>();
        public List<string> proxies = new List<string>();
        public List<User> users = new List<User>();
        public ReactVerifyForm ReactVerify;
        public string mainToken = "";
        public User? MainUser;
        public Guild? MainGuild;
        public Invite? MainInvite;
        public string captchaKey = "";
        public StackFrame sf = new StackFrame();
        public bool isReactVerifyOpen = false;
        public List<string> proxyErrors = new List<string> { "task was cancel", "operation was cancel", "was cancel", "The proxy tunnel request to proxy", "failed with status code '502'.", "The SSL connection could not" };
        public string inviteUrl = "";
        public ReactSettings reactSetting = new ReactSettings();
        public string delayString = "";
        public bool useProxy = true;
        public bool verifyWithReact = false;
        #endregion

        #region Main
        private void Form1_Click(object sender, EventArgs e)
        {
            TokenCountText.Focus();
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            LoadText.Text = "";
            MainAsync();
            ClearComboBox.SelectedIndex = 0;
            SaveComboBox.SelectedIndex = 0;

            ServicePointManager.DefaultConnectionLimit = 15999999;
            if (File.Exists(Environment.CurrentDirectory + @"\" + "sessionData"))
            {
                var jsonData = JObject.Parse(File.ReadAllText(Environment.CurrentDirectory + @"\" + "sessionData"));

                var captcha = jsonData["captchaKey"];
                var main = jsonData["mainToken"];
                var invite = jsonData["inviteLink"];
                var proxy = jsonData["proxies"];
                var token = jsonData["tokens"];
                var verifyWithReact = jsonData["reactVerify"]["isVerifyingWithReact"];
                var joinDelay = jsonData["joinDelay"];

                if (captcha != null)
                {
                    if (captcha.ToString() != "")
                    {
                        CaptchaKeyTextBox.Text = captcha.ToString();
                        CaptchaKeyTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                        captchaKey = captcha.ToString();

                    }
                }

                if (main != null)
                {
                    if (main.ToString() != "")
                    {
                        MainTokenTextBox.Text = main.ToString();
                        MainTokenTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                        mainToken = main.ToString();

                    }
                }

                if (invite != null)
                {
                    if (invite.ToString() != "")
                    {
                        InviteLinkTextBox.Text = invite.ToString();
                        InviteLinkTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                        inviteUrl = invite.ToString();

                    }
                }

                if (token != null)
                {
                    if (token.ToList() != null)
                    {
                        if (token.ToList().Count > 0)
                        {
                            token.ToList().ForEach(m => tokens.Add(m.ToString()));
                        }
                    }

                    var baseEx = tokens.Count.ToString().ToList().Count;
                    LoadText.Text = tokens.Count + " tokens loaded from data";
                    LoadText.Location = new Point((781 - baseEx * 20) - 200, 800);
                }

                if (proxy != null)
                {
                    if (proxy.ToList() != null)
                    {
                        if (proxy.ToList().Count > 0)
                        {
                            proxy.ToList().ForEach(m => proxies.Add(m.ToString()));
                        }
                    }


                }

                if (verifyWithReact != null)
                {
                    VerifyWithReactCheckBox.Checked = (bool)verifyWithReact;
                    VerifyWithReactCheckBox_CheckedChanged(this, EventArgs.Empty);
                }

                if(joinDelay != null)
                {
                    if(joinDelay.ToString() != "")
                    {
                        JoinDelayTextBox.Text = joinDelay.ToString();
                        JoinDelayTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
                        delayString = joinDelay.ToString().Trim();
                    }
                }

                var emoji = "";
                var emojiId = "";
                var channelId = "";
                var messageId = "";
                var isCustomEmoji = false;
                if (jsonData["reactVerify"] != null)
                {
                    if (jsonData["reactVerify"]["channelId"] != null)
                    {
                        channelId = jsonData["reactVerify"]["channelId"].ToString().Trim();

                    }

                    if (jsonData["reactVerify"]["messageId"] != null)
                    {
                        messageId = jsonData["reactVerify"]["messageId"].ToString().Trim();
                      
                    }

                    if (jsonData["reactVerify"]["emoji"] != null)
                    {
                        emoji = jsonData["reactVerify"]["emoji"].ToString().Trim();

                      
                    }

                    if (jsonData["reactVerify"]["isCustomEmoji"] != null)
                    {
                        isCustomEmoji = (bool)jsonData["reactVerify"]["isCustomEmoji"];

                       
                    }

                    if (jsonData["reactVerify"]["emojiId"] != null)
                    {
                        emojiId = jsonData["reactVerify"]["emojiId"].ToString().Trim();

                    
                    }


                }

                reactSetting.emojiName = emoji;
                reactSetting.emojiId = emojiId;
                reactSetting.channelId = channelId;
                reactSetting.messageId = messageId;
                reactSetting.isCustomEmoji = isCustomEmoji;
            }
        }
        public async Task MainAsync()
        {
            for (int i = 0; true; i++)
            {
                await Task.Delay(200);


                TokenCountText.Text = "Token count: " + tokens.Count.ToString();
                ProxyCountText.Text = "Proxy count: " + proxies.Count.ToString();
                UserCountText.Text = "Users count: " + users.Count.ToString();
            }

        }
        #endregion

        #region Captcha Key
        private void CaptchaKeyTextBox_TextChanged(object sender, EventArgs e)
        {
            captchaKey = CaptchaKeyTextBox.Text.Trim();
        }
        private void CaptchaKeyTextBox_LostFocus(object sender, EventArgs e)
        {
            if (CaptchaKeyTextBox.Text.Length == 0)
            {
                CaptchaKeyTextBox.Text = "Enter Captcha Key... ";
                CaptchaKeyTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (CaptchaKeyTextBox.Text == "Enter Captcha Key... ")
                {
                    CaptchaKeyTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }
        private void CaptchaKeyTextBox_GotFocus(object sender, EventArgs e)
        {
            if (CaptchaKeyTextBox.Text == "Enter Captcha Key... ")
            {
                CaptchaKeyTextBox.Text = "";
                CaptchaKeyTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }
        private void CaptchaBalanceButton_Click(object sender, EventArgs e)
        {
            RestClient client = new RestClient("https://api.capmonster.cloud/getBalance");
            RestRequest request = new RestRequest();
            request.Method = Method.Post;
            request.Timeout = 5000;

            var cf = new
            {
                clientKey = captchaKey

            };

            request.AddBody(JObject.FromObject(cf).ToString(), "application/json");
            var response = client.ExecuteAsync(request).Result;

            if (response.Content != null)
            {
                var jsonObj = JObject.Parse(response.Content);

                if (jsonObj["errorId"] != null)
                {
                    if ((int)jsonObj["errorId"] == 0)
                    {
                        if (jsonObj["balance"] != null)
                        {
                            if ((decimal)jsonObj["balance"] != null)
                            {
                                Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Key's balance is " + ((decimal)jsonObj["balance"]).ToString("F4") + "$\n");
                                var baseEx = ("Captcha balance: " + ((decimal)jsonObj["balance"]).ToString("F4") + "$").ToList().Count;
                                LoadText.Text = "Captcha balance: " + ((decimal)jsonObj["balance"]).ToString("F4") + "$";
                                LoadText.Location = new Point(1081 - baseEx * 20, 800);
                            }
                        }
                        else
                        {
                            if (response.ErrorMessage != null)
                            {
                                Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Error " + response.ErrorMessage + "\n");
                            }
                            else
                            {
                                Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Can't get the balance and don't know the reason.\n");
                            }
                        }
                    }
                    else
                    {
                        if (jsonObj["errorCode"] != null)
                        {
                            Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Error " + jsonObj["errorCode"] + "\n");
                        }
                        else
                        {
                            if (response.ErrorMessage != null)
                            {
                                Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Error " + response.ErrorMessage + "\n");
                            }
                            else
                            {
                                Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Can't get the balance and don't know the reason.\n");
                            }
                        }



                    }
                }
                else
                {
                    if (response.ErrorMessage != null)
                    {
                        Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Error " + response.ErrorMessage + "\n");
                    }
                    else
                    {
                        Console.WriteLine($"[{DateTime.Now.ToString("T")}] - {captchaKey}:" + " Can't get the balance and don't know the reason." + "\n");
                    }
                }

            }



        }
        #endregion

        #region Invite Link
        private void InviteLinkTextBox_LostFocus(object sender, EventArgs e)
        {
            if (InviteLinkTextBox.Text.Length == 0)
            {
                InviteLinkTextBox.Text = "Enter Invite Url... ";
                InviteLinkTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (InviteLinkTextBox.Text == "Enter Invite Url... ")
                {
                    InviteLinkTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }

        private void InviteLinkTextBox_GotFocus(object sender, EventArgs e)
        {
            if (InviteLinkTextBox.Text == "Enter Invite Url... ")
            {
                InviteLinkTextBox.Text = "";
                InviteLinkTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }

        private void InviteLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (InviteLinkTextBox.Text.Trim().ToLower().StartsWith("https://discord.gg/") || InviteLinkTextBox.Text.Trim().ToLower().StartsWith("http://discord.gg/") ||InviteLinkTextBox.Text.Trim().ToLower().StartsWith("discord.gg/"))
            {
                if (InviteLinkTextBox.Text.Trim().ToLower().StartsWith("https://discord.gg/"))
                {
                    inviteUrl = InviteLinkTextBox.Text.Trim().Substring("https://discord.gg/".Length).Trim();

                }
                else if (InviteLinkTextBox.Text.Trim().ToLower().StartsWith("http://discord.gg/"))
                {
                    inviteUrl = InviteLinkTextBox.Text.Trim().Substring("http://discord.gg/".Length).Trim();

                }
                else if (InviteLinkTextBox.Text.Trim().ToLower().StartsWith("discord.gg/"))
                {
                    inviteUrl = InviteLinkTextBox.Text.Trim().Substring("discord.gg/".Length).Trim();

                }
            }
            else
            {
                inviteUrl = InviteLinkTextBox.Text.Trim();
            }


        }

        private void SubmitInviteButton_click(object sender, EventArgs e)
        {
            if (MainUser != null)
            {
                GetGuildRequest(inviteUrl);
            }
            else
            {
                Console.WriteLine("========================================");
                Console.WriteLine("You can't submit because main user not set!");
                Console.WriteLine("========================================");
                Console.WriteLine("");
            }
        }
        #endregion

        #region Main Token

        public bool CheckIfProxyError(string msg)
        {
            return proxyErrors.Exists(m => msg.ToLower().Contains(m));
        }
        private void MainTokenTextBox_TextChanged(object sender, EventArgs e)
        {
            mainToken = MainTokenTextBox.Text.Trim();
        }
        private void MainTokenTextBox_LostFocus(object sender, EventArgs e)
        {
            if (MainTokenTextBox.Text.Length == 0)
            {
                MainTokenTextBox.Text = "Enter \r\nMain \r\nToken... ";
                MainTokenTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (MainTokenTextBox.Text == "Enter \r\nMain \r\nToken... ")
                {
                    MainTokenTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }

        private void MainTokenTextBox_GotFocus(object sender, EventArgs e)
        {
            if (MainTokenTextBox.Text == "Enter \r\nMain \r\nToken... ")
            {
                MainTokenTextBox.Text = "";
                MainTokenTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }

        private void MainTokenSubmitButton_Click(object sender, EventArgs e)
        {
            MainUserRequest();


        }

        private async Task MainUserRequest()
        {
            bool test = false;
            RestClientOptions options = new RestClientOptions();

            options.BaseUrl = new Uri("https://discord.com/api/v9/users/@me");
            #region Proxy

            var proxySt = "";
            if (proxies.Count > 0)
            {
                proxySt = proxies[0];

            }
            if (proxySt != "" && proxySt != "null" && proxySt != null &&proxySt.Contains(":") && useProxy)
            {
                var sp = proxySt.Split(':');

                string proxyUser = sp[2].Replace(":", "").Trim();
                string proxyPass = sp[3].Replace(":", "").Trim();


                WebProxy myproxy = new WebProxy(sp[0].Replace(":", "").Trim() + ":" + sp[1].Replace(":", "").Trim());

                myproxy.Credentials= new NetworkCredential(proxyUser, proxyPass);

                options.Proxy = myproxy;
            }

            #endregion

            var responseSt = "";


            RestClient restClient = new RestClient(options);

            var meRequest = new RestRequest();


            meRequest.AddHeader("authority", "discord.com");
            meRequest.AddHeader("accept", "*/*");
            meRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
            meRequest.AddHeader("authorization", mainToken);
            meRequest.AddHeader("content-type", "application/json");
            meRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
            meRequest.AddHeader("origin", "https://discord.com");
            meRequest.AddHeader("referer", "https://discord.com/channels/@me");
            meRequest.AddHeader("sec-fetch-dest", "empty");
            meRequest.AddHeader("sec-fetch-mode", "cors");
            meRequest.AddHeader("sec-fetch-site", "same-origin");
            meRequest.AddHeader("sec-gpc", "1");
            meRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
            meRequest.AddHeader("x-debug-options", "bugReporterEnabled");
            meRequest.AddHeader("x-discord-locale", "en-US");
            meRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");
            meRequest.Timeout = 5000;

            for (int i = 0; i < 10; i++)
            {

                await Task.Delay(1000);
                var response = await restClient.ExecuteGetAsync(meRequest);

                if (response.Content != null)
                {
                    var json = JObject.Parse(response.Content);
                    if (response.Content.Contains("\"id\": \"") || response.StatusCode == HttpStatusCode.OK)
                    {
                        responseSt = response.Content;
                        test = true;
                        break;
                    }
                    else if (json["message"] != null &  json["message"].ToString() != null)
                    {
                        Console.WriteLine(mainToken + " - Main token error: " +  json["message"].ToString() + "\n");
                        test = false;
                        break;
                    }
                }
                else
                {
                    if (response.ErrorException != null)
                    {
                        if (CheckIfProxyError(response.ErrorException.Message))
                        {

                        }
                        else
                        {
                            Console.WriteLine(response.ErrorException.Message + " Main Token: " + mainToken);
                            break;
                        }
                    }

                }

            }

            if (test)
            {

                bool verifyNeeded = false;
                var jsonData = JObject.Parse(responseSt);

                options.BaseUrl = new Uri("https://discord.com/api/v9/users/@me/outbound-promotions/codes");

                for (int i = 0; i < 10; i++)
                {

                    await Task.Delay(1000);
                    var response = await restClient.ExecuteGetAsync(meRequest);

                    if (response.Content != null)
                    {

                        if (response.Content.Contains("\"message\": \"You need to verify your account in order to perform this action.\""))
                        {

                            verifyNeeded = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (response.ErrorException != null)
                        {
                            if (CheckIfProxyError(response.ErrorException.Message))
                            {

                            }
                            else
                            {
                                Console.WriteLine(response.ErrorException.Message + " Main Token: " + mainToken);
                                break;
                            }
                        }
                        else
                        {
                            if (response.Content != null)
                            {
                                break;

                            }
                        }


                    }





                }

                try
                {
                    var user = new User(ulong.Parse(jsonData["id"].ToString()), (string)jsonData["username"], (string)jsonData["avatar"], (string)jsonData["discriminator"]
                   , (string)jsonData["banner"], (string)jsonData["bio"], (string)jsonData["pronouns"], (string)jsonData["locale"], (bool)jsonData["nsfw_allowed"],
                   (bool)jsonData["mfa_enabled"], (string)jsonData["email"], (bool)jsonData["verified"], (string)jsonData["phone"], mainToken);
                    user.needsAction = verifyNeeded;

                    if (!user.needsAction)
                    {
                        Console.WriteLine($"[{mainToken}]: " + (string)jsonData["username"] + "#" + (string)jsonData["discriminator"] + " Set as a main user.\n");


                        if (MainUser != null)
                        {
                            if (MainUser.Token != null)
                            {
                                if (MainUser.Token != user.Token)
                                {
                                    MainGuild = null;
                                    GuildImageBox.Image = null;
                                    GuildTextBox.Text = "";
                                    MainInvite = null;
                                }
                            }
                        }
                        MainUser = user;


                        MainUserPictureBox.Image = null;
                        MainUserNameLabel.Text = "";
                        MainUserPictureBox.LoadAsync(MainUser.AvatarUrl);
                        if (MainUser.AvatarUrl.Contains(MainUser.id.ToString()))
                        {
                            MainUserPictureBox.SizeMode = PictureBoxSizeMode.Normal;
                        }
                        else
                        {
                            MainUserPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        }

                        MainUserNameLabel.Text = MainUser.UserName + "#" + MainUser.Discriminator;
                    }
                    else
                    {

                        Console.WriteLine($"[{mainToken}]: " + (string)jsonData["username"] + "#" + (string)jsonData["discriminator"] + " Can't set as a main user because account needs VERIFY!\n");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Token: " +mainToken+ "- "+ ex.Message +"-" + "\n");
                }



            }
            else
            {
                MainUser = null;
                MainUserPictureBox.Image = null;
                MainUserNameLabel.Text = "";
                MainGuild = null;
                GuildImageBox.Image = null;
                GuildTextBox.Text = "";
                MainInvite = null;
            }
        }
        #endregion

        #region Console
        private void ClearConsoleButton_Click(object sender, EventArgs e)
        {
            Console.Clear();

        }

        public void Logger(string val)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + val + "\n");
        }

        public string Username(string username, int line)
        {
            return $"[{line} - {DateTime.Now.ToString("T")}] - {username}: ";
        }
        #endregion

        #region Save System
        private void SaveStateButton_Click(object sender, EventArgs e)
        {

            var jsonData = JObject.Parse(File.ReadAllText(Environment.CurrentDirectory + @"\" + "sessionData"));
            var clearBody = "";

            dynamic reactVerify;

            if (isReactVerifyOpen)
            {
                reactVerify = new
                {
                    isVerifyingWithReact = verifyWithReact,
                    channelId = ReactVerify.channelId,
                    messageId = ReactVerify.messageId,
                    emoji = ReactVerify.emoji,
                    emojiId = ReactVerify.emojiId,
                    isCustomEmoji = ReactVerify.isCustomEmoji,

                };

            }
            else
            {
                reactVerify = jsonData["reactVerify"];
                Console.WriteLine(verifyWithReact);
                reactVerify["isVerifyingWithReact"] = verifyWithReact;


            }

            if (SaveComboBox.SelectedIndex == 0) // All
            {
                var cf = new
                {
                    captchaKey = captchaKey,
                    mainToken = mainToken,
                    inviteLink = inviteUrl,
                    proxies = proxies,
                    tokens = tokens,
                    reactVerify = reactVerify,
                    joinDelay = delayString


                };


                var cc = JObject.FromObject(cf).ToString();


                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", cc.ToString());
            }
            else if (SaveComboBox.SelectedIndex == 1) // Tokens
            {
                jsonData["tokens"].Parent.Remove();
                jsonData.Add("tokens", JToken.FromObject(tokens));
                var cc = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", cc);
            }
            else if (SaveComboBox.SelectedIndex == 2) // Proxy
            {
                jsonData["proxies"].Parent.Remove();
                jsonData.Add("proxies", JToken.FromObject(proxies));
                var cc = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", cc);

            }
            else if (SaveComboBox.SelectedIndex == 3) // Captcha
            {
                jsonData["captchaKey"].Parent.Remove();
                jsonData.Add("captchaKey", JToken.FromObject(captchaKey));
                var cc = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", cc);
            }
            else if (SaveComboBox.SelectedIndex == 4) // Invite
            {
                jsonData["inviteLink"].Parent.Remove();
                jsonData.Add("inviteLink", JToken.FromObject(inviteUrl));
                var cc = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", cc);
            }
            else if (SaveComboBox.SelectedIndex == 5) // Main Token
            {
                jsonData["mainToken"].Parent.Remove();
                jsonData.Add("mainToken", JToken.FromObject(mainToken));
                var cc = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", cc);
            }
            else if (SaveComboBox.SelectedIndex == 6) // React Verify
            {
                jsonData["reactVerify"].Parent.Remove();
                jsonData.Add("reactVerify", JToken.FromObject(reactVerify));
                var cc = JsonConvert.SerializeObject(jsonData, Formatting.Indented);


                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", cc);
            }



            var loadSt = "";
            if (SaveComboBox.SelectedIndex == 0)
            {
                Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + "All datas are saved.\n");
                loadSt = "All datas are saved.";
            }
            else if (SaveComboBox.SelectedIndex == 5)
            {
                Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + "Main token is saved.\n");
                loadSt = "Main token is saved.";
            }
            else if (SaveComboBox.SelectedIndex == 6)
            {

                Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + "React verify is saved.\n");
                loadSt = "React verify data is saved.";
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + SaveComboBox.Items[SaveComboBox.SelectedIndex]+" is saved.\n");
                loadSt = SaveComboBox.Items[SaveComboBox.SelectedIndex]+" is saved.\n";
            }

            var baseEx = loadSt.ToList().Count;
            LoadText.Text = loadSt;
            LoadText.Location = new Point(1081 - baseEx * 20, 800);

        }
        private void ClearStateButton_Click(object sender, EventArgs e)
        {

            var cf = new
            {
                captchaKey = "",
                mainToken = "",
                inviteLink = "",
                proxies = new List<string>(),
                tokens = new List<string>(),

                reactVerify = new
                {
                    isVerifyingWithReact = false,
                    channelId = "",
                    messageId = "",
                    emoji = "",
                    emojiId = "",
                    isCustomEmoji = false,


                },
                joinDelay = ""



            };

            var reactVerify = new
            {
                isVerifyingWithReact = false,
                channelId = "",
                messageId = "",
                emoji = "",
                emojiId = "",
                isCustomEmoji = false,

            };

            var clearBody = JObject.FromObject(cf).ToString();

            var jsonData = JObject.Parse(File.ReadAllText(Environment.CurrentDirectory + @"\" + "sessionData"));

            if (ClearComboBox.SelectedIndex == 0) // All
            {
                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", clearBody);

            }
            else if (ClearComboBox.SelectedIndex == 1) // Tokens
            {
                jsonData["tokens"].Parent.Remove();
                jsonData.Add("tokens", JToken.FromObject(new List<string>()));
                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", jsonData.ToString());
            }
            else if (ClearComboBox.SelectedIndex == 2) // Proxy  
            {
                jsonData["proxies"].Parent.Remove();
                jsonData.Add("proxies", JToken.FromObject(new List<string>()));

                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", jsonData.ToString());
            }
            else if (ClearComboBox.SelectedIndex == 3) // Captcha
            {
                jsonData["captchaKey"] = "";
                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", jsonData.ToString());
            }
            else if (ClearComboBox.SelectedIndex == 4) // Invite
            {
                jsonData["inviteLink"] = "";
                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", jsonData.ToString());
            }
            else if (ClearComboBox.SelectedIndex == 5) // Main Token
            {
                jsonData["mainToken"] = "";
                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", jsonData.ToString());
            }
            else if (ClearComboBox.SelectedIndex == 6) // React Verify
            {
                jsonData["reactVerify"].Parent.Remove();
                jsonData.Add("reactVerify", JToken.FromObject(reactVerify));
                File.WriteAllText(Environment.CurrentDirectory + @"\" + "sessionData", jsonData.ToString());
            }

            var loadSt = "";


            if (ClearComboBox.SelectedIndex == 0)
            {
                Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + "All datas are cleared.\n");
                loadSt = "All datas are cleared.";
            }
            else if (ClearComboBox.SelectedIndex == 5)
            {
                Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + "Main token data is cleared.\n");
                loadSt = "Main token data is cleared.";
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now.ToString("T")}]: " + ClearComboBox.Items[ClearComboBox.SelectedIndex]+" data is cleared.\n");
                loadSt = ClearComboBox.Items[ClearComboBox.SelectedIndex]+" data is cleared.\n";
            }

            var baseEx = loadSt.ToList().Count;
            LoadText.Text = loadSt;
            LoadText.Location = new Point(1081 - baseEx * 20, 800);
        }
        #endregion

        #region User Buttons
        private async void SetUserButton_Click(object sender, EventArgs e)
        {
            int divide = 0;
            if (proxies.Count != 0)
            {
                if (tokens.Count > proxies.Count)
                {
                    divide = tokens.Count / proxies.Count;

                }
            }

            var proxyCt = 0;
            for (int i = 0; i < tokens.Count; i++)
            {
                if (!users.Exists(m => m.Token == tokens[i]))
                {
                    if (proxies.Count > 0)
                    {
                        UserInfoRequest(tokens[i], proxies[proxyCt]);
                        if (divide != 0)
                        {
                            if (i % divide == 0)
                            {
                                proxyCt++;
                            }
                        }
                        else
                        {
                            proxyCt++;
                        }
                    }
                    else
                    {
                        UserInfoRequest(tokens[i], "");
                    }
                }

                await Task.Delay(100);
            }

        }
        private void ClearUserButton_Click(object sender, EventArgs e)
        {
            var baseEx = (users.Count + " users cleared").ToList().Count;
            LoadText.Text = users.Count + " users cleared.";
            Logger(users.Count + " users cleared.");

            LoadText.Location = new Point(1081 - baseEx * 20, 800);
            users.Clear();
        }
        private async void JoinButton_Click(object sender, EventArgs e)
        {

            int divide = 0;

            var proxyCt = 0;

            bool start = false;

            int delay = 1000;
            if(int.TryParse(delayString.Trim(), out int xd))
            {
               
                Console.WriteLine("==============================================");
                Console.WriteLine("You set delay to " + xd + " miliseconds.");
                Console.WriteLine("=============================================");
                Console.WriteLine("");
                delay = xd;
            }
            else
            {
                
                Console.WriteLine("===================================================================");
                Console.WriteLine("You did not set delay and I set it 1000 miliseconds automatically.");
                Console.WriteLine("===================================================================");
                Console.WriteLine("");
            }
            if (MainInvite != null)
            {
                if (MainInvite.code != null)
                {

                    Console.WriteLine("");
                    start = true;
                }
                else
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine("You can't start join because invite code not set.");
                    Console.WriteLine("==============================================");
                }



            }
            else
            {
                Console.WriteLine("==============================================");
                Console.WriteLine("You can't start join because invite not set.");
                Console.WriteLine("==============================================");
            }

            if(start)
            {
                for (int i = 0; i < users.Count; i++)
                {
                    if (MainInvite != null)
                    {
                        if (MainInvite.code != null)
                        {
                            if (proxies.Count > 0)
                            {
                                if (!users[i].needsAction)
                                {

                                    JoinRequest(users[i], MainGuild, proxies[i], reactSetting, i + 1, users.Count + 1);

                                }

                            }
                            else
                            {
                                if (!users[i].needsAction)
                                {
                                    JoinRequest(users[i], MainGuild, "", reactSetting, i + 1, users.Count + 1);
                                }

                            }

                            await Task.Delay(delay);
                        }

                    }

                }
            }
          
        }
        #endregion

        #region Proxy And Tokenws
        private void ClearProxyButton_Click(object sender, EventArgs e)
        {
            var baseEx = proxies.Count.ToString().ToList().Count;
            LoadText.Text = proxies.Count + " proxies cleared.";
            Logger(proxies.Count + " proxies cleared.");
            LoadText.Location = new Point((761 - baseEx * 20) - 20, 800);
            proxies.Clear();

        }
        private void ClearTokenButton_Click(object sender, EventArgs e)
        {
            var baseEx = (tokens.Count + " tokens cleared").ToList().Count;
            LoadText.Text = tokens.Count + " tokens cleared.";
            Logger(tokens.Count + " tokens cleared.");
            LoadText.Location = new Point(1081 - baseEx * 20, 800);
            tokens.Clear();
        }
        private void SelectTokenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            file.Filter = "Token List|*.txt";
            if (file.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(file.FileName);
                if (fi.Exists)
                {

                    tokens = File.ReadAllLines(file.FileName).ToList();

                    tokens.ForEach(m => m.Trim());
                    tokens.RemoveAll(m =>
                    {
                        bool isT = false;

                        if (m.Length == 0)
                        {
                            isT = true;
                        }

                        if (m.Contains(" "))
                        {
                            isT = true;
                        }

                        if (m == "")
                        {
                            isT = true;
                        }

                        return isT;

                    });

                    if (tokens.Count == 0)
                    {
                        LoadText.Text = "Error - load tokens again";
                        LoadText.Location = new Point(561, 800);
                    }
                    else
                    {
                        var baseEx = tokens.Count.ToString().ToList().Count;
                        LoadText.Text = tokens.Count + " tokens loaded";
                        LoadText.Location = new Point(781 - baseEx * 20, 800);
                    }


                }
                else
                {
                    LoadText.Text = "Error - load tokens again";
                    LoadText.Location = new Point(561, 800);

                }
            }
        }
        private void SelectProxyButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Proxy List|*.txt";
            if (file.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(file.FileName);
                if (fi.Exists)
                {

                    proxies = File.ReadAllLines(file.FileName).ToList();
                    proxies.ForEach(m => m.Trim());
                    proxies.RemoveAll(m =>
                    {
                        bool isT = false;

                        if (m.Length == 0)
                        {
                            isT = true;
                        }

                        if (m.Contains(" "))
                        {
                            isT = true;
                        }

                        if (m == "")
                        {
                            isT = true;
                        }

                        return isT;

                    });

                    if (proxies.Count == 0)
                    {
                        LoadText.Text = "Error - load proxies again";
                        LoadText.Location = new Point(541, 800);
                    }
                    else
                    {
                        var baseEx = proxies.Count.ToString().ToList().Count;
                        LoadText.Text = proxies.Count + " proxies loaded";
                        LoadText.Location = new Point(761 - baseEx * 20, 800);
                    }

                    ProxyCountText.Text = "Proxy Count: " + proxies.Count;
                }
                else
                {
                    LoadText.Text = "Error - load proxies again";
                    LoadText.Location = new Point(541, 800);


                }
            }
        }
        private void UseProxyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            useProxy = UseProxyCheckBox.Checked;
        }
        #endregion

        #region React Verify
        private void ReactVerifyButton_Click(object sender, EventArgs e)
        {
            if (ReactVerify == null)
            {
                ReactVerify = new ReactVerifyForm();
                ReactVerify.form = this;
                ReactVerify.Show();
                isReactVerifyOpen = true;


            }
            else
            {
                if (ReactVerify.IsDisposed)
                {
                    ReactVerify = new ReactVerifyForm();
                    ReactVerify.form = this;
                    ReactVerify.Show();
                    isReactVerifyOpen = true;

                }
            }
        }
        private void VerifyWithReactCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!VerifyWithReactCheckBox.Checked)
            {
                ReactVerifyButton.Visible = false;
                if (ReactVerify != null)
                {

                    if (!ReactVerify.IsDisposed)
                    {
                        ReactVerify.Dispose();
                        ReactVerify = null;
                    }

                }
                verifyWithReact = false;
            }
            else
            {
                ReactVerifyButton.Visible = true;
                verifyWithReact = true;

            }
        }
        #endregion

        #region Requests
        public async Task UserInfoRequest(string token, string proxySt)
        {
            bool test = false;
            RestClientOptions options = new RestClientOptions();

            options.BaseUrl = new Uri("https://discord.com/api/v9/users/@me");
            #region Proxy

            if (proxySt != "" && proxySt != "null" && proxySt != null &&proxySt.Contains(":") && useProxy && useProxy)
            {
                //  var sp = proxySt.Split(':');
                //
                //  string proxyUser = sp[2].Replace(":", "").Trim();
                //  string proxyPass = sp[3].Replace(":", "").Trim();
                //
                //
                //  WebProxy myproxy = new WebProxy(sp[0].Replace(":", "").Trim() + ":" + sp[1].Replace(":", "").Trim());
                //
                //  myproxy.Credentials= new NetworkCredential(proxyUser, proxyPass);
                //
                //  options.Proxy = myproxy;
            }

            #endregion

            var responseSt = "";


            RestClient restClient = new RestClient(options);

            var meRequest = new RestRequest();


            meRequest.AddHeader("authority", "discord.com");
            meRequest.AddHeader("accept", "*/*");
            meRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
            meRequest.AddHeader("authorization", token);
            meRequest.AddHeader("content-type", "application/json");
            meRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
            meRequest.AddHeader("origin", "https://discord.com");
            meRequest.AddHeader("referer", "https://discord.com/channels/@me");
            meRequest.AddHeader("sec-fetch-dest", "empty");
            meRequest.AddHeader("sec-fetch-mode", "cors");
            meRequest.AddHeader("sec-fetch-site", "same-origin");
            meRequest.AddHeader("sec-gpc", "1");
            meRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
            meRequest.AddHeader("x-debug-options", "bugReporterEnabled");
            meRequest.AddHeader("x-discord-locale", "en-US");
            meRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");
            meRequest.Timeout = 5000;

            for (int i = 0; i < 10; i++)
            {

                await Task.Delay(1000);
                var response = await restClient.ExecuteGetAsync(meRequest);

                if (response.Content != null)
                {

                    if (response.Content.Contains("\"id\": \""))
                    {
                        responseSt = response.Content;
                        test = true;
                        break;
                    }
                    else
                    {
                        if (!CheckIfProxyError(response.Content))
                        {
                            test = false;
                            Console.WriteLine("Unknown token: " + token);
                            break;
                        }
                        else
                        {

                        }

                    }
                }
                else
                {
                    if (response.ErrorException != null)
                    {
                        if (CheckIfProxyError(response.ErrorException.Message))
                        {

                        }
                        else
                        {
                            Console.WriteLine(response.ErrorException.Message + " Token: " + token);
                            break;
                        }
                    }

                }

            }

            if (test)
            {
                try
                {
                    bool verifyNeeded = false;
                    var jsonData = JObject.Parse(responseSt);

                    options.BaseUrl = new Uri("https://discord.com/api/v9/users/@me/outbound-promotions/codes");

                    for (int i = 0; i < 10; i++)
                    {

                        await Task.Delay(1000);
                        var response = await restClient.ExecuteGetAsync(meRequest);

                        if (response.Content != null)
                        {

                            if (response.Content.Contains("\"message\": \"You need to verify your account in order to perform this action.\""))
                            {

                                verifyNeeded = true;
                                break;
                            }
                            else
                            {
                                if (CheckIfProxyError(response.Content))
                                {
                                    Console.WriteLine("Proxy error and status code: " + response.StatusCode);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (response.ErrorException != null)
                            {
                                if (CheckIfProxyError(response.ErrorException.Message))
                                {
                                    Console.WriteLine("Proxy error and status code: " + response.StatusCode);
                                }
                                else
                                {
                                    Console.WriteLine(response.ErrorException.Message + " Token: " + token);
                                    break;
                                }
                            }
                            else
                            {
                                if (response.Content != null)
                                {


                                }
                            }


                        }





                    }

                    if (!users.Exists(m => m.Token == token))
                    {
                        var user = new User(ulong.Parse(jsonData["id"].ToString()), (string)jsonData["username"], (string)jsonData["avatar"], (string)jsonData["discriminator"]
                      , (string)jsonData["banner"], (string)jsonData["bio"], (string)jsonData["pronouns"], (string)jsonData["locale"], (bool)jsonData["nsfw_allowed"],
                      (bool)jsonData["mfa_enabled"], (string)jsonData["email"], (bool)jsonData["verified"], (string)jsonData["phone"], token);
                        user.needsAction = verifyNeeded;
                        users.Add(user);
                        if (!user.needsAction)
                        {
                            Console.WriteLine($"[{token}]: " + (string)jsonData["username"] + "#" + (string)jsonData["discriminator"] + " Added to users.");
                        }
                        else
                        {

                            Console.WriteLine($"[{token}]: " + (string)jsonData["username"] + "#" + (string)jsonData["discriminator"] + " Added to users but needs VERIFY!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[{token}]: " + (string)jsonData["username"] + "#" + (string)jsonData["discriminator"] + " User already exist");
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Token: " +token+ "- "+ ex.Message +"-" + "\n");
                }
            }
        }
        public async Task GetGuildRequest(string url)
        {
            RestClientOptions options = new RestClientOptions();

            options.BaseUrl = new Uri("https://discord.com/api/v9/invites/" + url);

            var responseSt = "";


            RestClient restClient = new RestClient(options);

            var meRequest = new RestRequest();


            meRequest.AddHeader("authority", "discord.com");
            meRequest.AddHeader("accept", "*/*");
            meRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
            meRequest.AddHeader("content-type", "application/json");
            meRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
            meRequest.AddHeader("origin", "https://discord.com");
            meRequest.AddHeader("sec-fetch-dest", "empty");
            meRequest.AddHeader("sec-fetch-mode", "cors");
            meRequest.AddHeader("sec-fetch-site", "same-origin");
            meRequest.AddHeader("sec-gpc", "1");
            meRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
            meRequest.AddHeader("x-debug-options", "bugReporterEnabled");
            meRequest.AddHeader("x-discord-locale", "en-US");
            meRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");
            meRequest.Timeout = 5000;

            var rp = await restClient.ExecuteGetAsync(meRequest);
            if (rp.Content != null)
            {
                var jsonData = JObject.Parse(rp.Content);
                if (jsonData["message"] != null)
                {
                    Console.WriteLine(Username(MainUser.UserName + "#" + MainUser.Discriminator, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+"Guild not found. Error: " + jsonData["message"] + "\n");

                }
                else
                {
                    var invite = new Invite();

                    try
                    {
                        if (jsonData["inviter"] != null)
                        {
                            invite = new Invite()
                            {
                                code = jsonData["code"].ToString(),
                                expireDate = jsonData["expires_at"].ToString(),
                                guildId = jsonData["guild"]["id"].ToString(),
                                guildName = jsonData["guild"]["name"].ToString(),
                                inviterDiscriminator = jsonData["inviter"]["discriminator"].ToString(),
                                inviterId =  jsonData["inviter"]["id"].ToString(),
                                inviterUsername = jsonData["inviter"]["username"].ToString()


                            };
                        }
                        else
                        {
                            invite = new Invite()
                            {
                                code = jsonData["code"].ToString(),
                                expireDate = jsonData["expires_at"].ToString(),
                                guildId = jsonData["guild"]["id"].ToString(),
                                guildName = jsonData["guild"]["name"].ToString(),



                            };
                        }


                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                        invite = null;

                    }

                    if (invite != null)
                    {

                        meRequest.AddHeader("authorization", MainUser.Token);
                        options.BaseUrl = new Uri($"https://discord.com/api/v9/guilds/{invite.guildId}?with_counts=true");

                        rp = await restClient.ExecuteGetAsync(meRequest);

                        if (rp.StatusCode == HttpStatusCode.OK)
                        {
                            if (rp.Content != null)
                            {
                                jsonData = JObject.Parse(rp.Content);
                                var guild = new Guild()
                                {
                                    banner = jsonData["banner"].ToString(),
                                    description = jsonData["description"].ToString(),
                                    icon = jsonData["icon"].ToString(),
                                    id = jsonData["id"].ToString(),
                                    memberCount = int.Parse(jsonData["approximate_member_count"].ToString()),
                                    splash = jsonData["splash"].ToString(),
                                    name = jsonData["name"].ToString(),
                                    owner_id = jsonData["owner_id"].ToString(),
                                    region = jsonData["region"].ToString(),
                                    vanityUrl = jsonData["vanity_url_code"].ToString(),
                                    code = inviteUrl



                                };
                                MainGuild = guild;
                                MainInvite = invite;

                                options.BaseUrl = new Uri($"https://discord.com/api/v9/guilds/{invite.guildId}/member-verification?with_guild=false");
                                rp = await restClient.ExecuteGetAsync(meRequest);

                                if (rp.Content != null)
                                {
                                    jsonData = JObject.Parse(rp.Content);
                                    if (jsonData["message"] == null)
                                    {
                                        if (jsonData["version"] != null)
                                        {
                                            if (jsonData["form_fields"] != null)
                                            {
                                                guild.thereIsRule = true;
                                                guild.ruleJson = rp.Content;
                                            }
                                        }
                                    }
                                }
                                if (guild.thereIsRule)
                                {
                                    Console.WriteLine(Username(MainUser.UserName + "#" + MainUser.Discriminator, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+MainGuild.name + " Set as a main guild and there is RULE on guild!" + "\n");
                                }
                                else
                                {
                                    Console.WriteLine(Username(MainUser.UserName + "#" + MainUser.Discriminator, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+MainGuild.name + " Set as a main guild and there is NO RULE on guild!" + "\n");
                                }


                                if (guild.icon == null || guild.icon == "" || guild.icon.Trim() == "")
                                {
                                    GuildImageBox.LoadAsync("https://media.discordapp.net/attachments/469612307836698635/977856761388220416/unknown.png");

                                }
                                else
                                {

                                    GuildImageBox.LoadAsync($"https://cdn.discordapp.com/icons/{guild.id}/{guild.icon}.png");
                                }
                                GuildTextBox.Text = guild.name.ToString();
                            }
                            else
                            {
                                Console.WriteLine(Username(MainUser.UserName + "#" + MainUser.Discriminator, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $" Not in {invite.guildName} guild!\n");

                            }


                        }
                        else
                        {
                            Console.WriteLine(Username(MainUser.UserName + "#" + MainUser.Discriminator, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $" Not in {invite.guildName} guild!\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine(Username(MainUser.UserName + "#" + MainUser.Discriminator, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+"Guild not found. Error: " + jsonData["message"] + "\n");

                    }

                }

            }


        }
        public async Task JoinRequest(User user, Guild guild, string proxySt, ReactSettings react, int userNum, int allUsers)
        {
            string username = "("+userNum+ "/" + (allUsers - 1) +") " + user.UserName + "#" + user.Discriminator;

            var DiscordClientOptions = new RestClientOptions();
            var NormalClientOptions = new RestClientOptions();

            #region Proxy
            if (proxySt != "" && proxySt != "null" && proxySt != null &&proxySt.Contains(":") && useProxy)
            {
                var sp = proxySt.Split(':');

                string proxyUser = sp[2].Replace(":", "").Trim();
                string proxyPass = sp[3].Replace(":", "").Trim();


                WebProxy myproxy = new WebProxy(sp[0].Replace(":", "").Trim() + ":" + sp[1].Replace(":", "").Trim());

                myproxy.Credentials= new NetworkCredential(proxyUser, proxyPass);

                DiscordClientOptions.Proxy = myproxy;
            }
            #endregion

            var DiscordRestClient = new RestClient(DiscordClientOptions);
            var NormalRestClient = new RestClient(NormalClientOptions);

            string? userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36";

            var JoinRequest = AddBaseHeaders(user.Token);

            JoinRequest.Timeout = 10000;

            JoinRequest.AddBody("{}", "application/json");
     

            DiscordClientOptions.BaseUrl = new Uri($"https://discord.com/api/v9/invites/{MainGuild.code}");

            var joinResponse = await DiscordRestClient.ExecutePostAsync(JoinRequest);

            var needsCaptcha = false;

            string? CaptchaRqData = "";
            string? CaptchaRqToken = "";
            string? CaptchaSiteKey = "";
            string? CaptchaAnswer = "";


            var youCanContinue = true;

            var joinJson = new JObject();

            if ((int)joinResponse.StatusCode == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(3000);
                    joinResponse = await DiscordRestClient.ExecutePostAsync(JoinRequest);

                    if ((int)joinResponse.StatusCode == 0)
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Proxy Problem] There is a proxy problem on first join, trying to solving. Try count: " +(i + 1)+ "\n");
                      
                    }
                    else
                    {
                        youCanContinue = true;
                        break;
                    }


                }

                if ((int)joinResponse.StatusCode == 0)
                {
                    youCanContinue = false;
                }
            }

            if (youCanContinue) // First
            {
                if ((int)joinResponse.StatusCode == 200)
                {
                    needsCaptcha = false;
                    joinJson = JObject.Parse(joinResponse.Content);

                    var guildName = "";

                    if (joinJson["guild"] != null)
                    {
                        if (joinJson["guild"]["name"] != null)
                        {
                            guildName = joinJson["guild"]["name"].ToString();
                        }

                    }


                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + " [Finished joining] Joined to " + guildName + "\n");

                    youCanContinue = true;
                } // Joined 

                else if ((int)joinResponse.StatusCode == 400)
                {

                    if (joinResponse.Content != null)
                    {
                        joinJson = JObject.Parse(joinResponse.Content.ToString());

                        if (joinJson["captcha_rqdata"] != null)
                        {
                            needsCaptcha = true;

                            CaptchaRqData = joinJson["captcha_rqdata"].ToString();
                            CaptchaRqToken = joinJson["captcha_rqtoken"].ToString();
                            CaptchaSiteKey = joinJson["captcha_sitekey"].ToString();

                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Captcha Task 1/3] "+ "Needs captcha for joining. Starting set task."+ "\n");
                            youCanContinue = true;

                        }
                        else
                        {
                            if (joinJson["message"] != null)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] "+ (int)joinResponse.StatusCode+" error. Message: ."+joinJson["message"]+ "\n");
                                youCanContinue = false;
                            }
                            else
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] "+ (int)joinResponse.StatusCode+" error and there is no message."+ "\n");
                                youCanContinue = false;

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] "+ (int)joinResponse.StatusCode+" error and there is no message."+ "\n");
                        youCanContinue = false;
                    }


                } // Bad Request

                else if ((int)joinResponse.StatusCode == 401)
                {
                    youCanContinue = false;
                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Stopped joining] Not valid or banned token. token[{user.Token}]\n");
                } // Unauthorized

                else if ((int)joinResponse.StatusCode == 429)
                {
                    youCanContinue = false;
                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $" [Stopped joining] Your IP is rate limited.\n");
                }  // Too Many Request

                else if ((int)joinResponse.StatusCode == 403)
                {

                    if (joinResponse.Content != null)
                    {
                        joinJson = JObject.Parse(joinResponse.Content);

                        if (joinJson["message"] != null)
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] 403 error and there is a message. Message: " + joinJson["message"].ToString() + "\n");
                        }
                        else
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] 403 error and there is a message. Message: " + joinJson["message"].ToString()+ "\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] 403 error and there is no message.\n");
                    }

                    youCanContinue = false;
                }  // Forbidden

                else if ((int)joinResponse.StatusCode == 404)
                {
                    if (joinResponse.Content != null)
                    {
                        joinJson = JObject.Parse(joinResponse.Content);

                        if (joinJson["message"] != null)
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] 404 error and there is a message. Message: " + joinJson["message"].ToString()+ "\n");
                        }
                        else
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] 404 error and there is no message."+ "\n");

                        }
                    }
                    else
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] 404 error and there is no message."+ "\n");
                    }

                    youCanContinue = false;

                } // Not Found

                else
                {
                    if (joinResponse.Content != null)
                    {
                        joinJson = JObject.Parse(joinResponse.Content);

                        if (joinJson["message"] != null)
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] "+(int)joinResponse.StatusCode+" error and there is a message. Message: " + joinJson["message"].ToString()+ "\n");
                        }
                        else
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] "+(int)joinResponse.StatusCode+ " error and there is no message."+ "\n");

                        }
                    }
                    else
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] "+ (int)joinResponse.StatusCode+" error and there is no message."+ "\n");
                    }

                    youCanContinue = false;
                } // Others

            } // First
            else
            {
                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve proxy problem.\n");
            }

            if (needsCaptcha) // Captcha Start
            {

                if (CaptchaRqData != "" && CaptchaRqToken != "" && CaptchaSiteKey != "" && CaptchaRqData != null && CaptchaRqToken != null && CaptchaSiteKey != null)
                {
                    NormalClientOptions.BaseUrl = new Uri("https://api.capmonster.cloud/createTask");

                    var captchaJson = new JObject();

                    string taskId = "";
                    var captchaSetRequestBodyObj = new
                    {
                        clientKey = captchaKey,
                        task = new
                        {
                            type = "HCaptchaTaskProxyless",
                            websiteURL = "https://discord.com",
                            websiteKey = CaptchaSiteKey,
                            data = CaptchaRqData,
                            userAgent = userAgent,
                            isInvisible = true
                        }
                    };

                    var captchaSetRequest = new RestRequest();
                    captchaSetRequest.AddBody(JObject.FromObject(captchaSetRequestBodyObj).ToString(), "application/json");
                    captchaSetRequest.AddHeader("Content-Type", "application/json");
                    captchaSetRequest.Timeout = 5000;
                    var captchaSetResponse = await NormalRestClient.ExecutePostAsync(captchaSetRequest);

                    if ((int)captchaSetResponse.StatusCode == 0)
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't start solving captcha because request timeout.\n");

                        youCanContinue = false;
                    }
                    else
                    {
                        if (captchaSetResponse.Content != null)
                        {
                            captchaJson = JObject.Parse(captchaSetResponse.Content);

                            if (captchaJson["errorId"] != null)
                            {
                                if (int.TryParse((string)captchaJson["errorId"], out int test))
                                {
                                    if ((int)captchaJson["errorId"] == 0)
                                    {
                                        if (captchaJson["taskId"] != null)
                                        {
                                            if (captchaJson["taskId"].ToString() != "")
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Captcha Task 2/3] Captcha task set. Task ID:"+captchaJson["taskId"].ToString()+ "\n");
                                                taskId = captchaJson["taskId"].ToString();
                                                youCanContinue = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't set captcha task.\n");
                                                youCanContinue = false;
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't set captcha task.\n");
                                            youCanContinue = false;
                                        }
                                    }
                                    else
                                    {
                                        if (captchaJson["errorCode"] != null)
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't set captcha task. Message: " + captchaJson["errorCode"].ToString()+ "\n");
                                            youCanContinue = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't set captcha task.\n");
                                            youCanContinue = false;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't set captcha task.\n");
                                    youCanContinue = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't set captcha task.\n");
                                youCanContinue = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't set captcha task.\n");
                            youCanContinue = false;
                        }
                    }

                    if (youCanContinue) // Get Captcha Answer
                    {

                        var captchaGetRequest = new RestRequest();
                        var captchaGetRequestBodyObj = new
                        {
                            clientKey = captchaKey,
                            taskId = int.Parse(taskId)
                        };
                        
                        captchaGetRequest.AddBody(JObject.FromObject(captchaGetRequestBodyObj).ToString(), "application/json");
                        captchaGetRequest.AddHeader("Content-Type", "application/json");
                        captchaGetRequest.Timeout = 5000;

                        var captchaGetResponse = new RestResponse();

                        int resetProgramContentNull = 0;
                        int resetProgramErrorIdNull = 0;
                        int resetProgramStatusNull = 0;
                        int processingCounter = 0;

                        NormalClientOptions.BaseUrl = new Uri("https://api.capmonster.cloud/getTaskResult");

                        for (int i = 0; i < 25; i++)
                        {
                            if(resetProgramContentNull >= 4)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve captcha because request content empty.\n");
                                youCanContinue = false;
                                break;
                            }

                            if (resetProgramErrorIdNull >= 4)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve captcha because can't solve captcha.\n");
                                youCanContinue = false;
                                break;
                            }
                            if (resetProgramStatusNull >= 4)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve captcha because status is empty.\n");
                                youCanContinue = false;
                                break;
                            }

                            captchaGetResponse =  await NormalRestClient.ExecuteGetAsync(captchaGetRequest);

                            await Task.Delay(3000);

                            if ((int)captchaGetResponse.StatusCode == 0)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve captcha because request timeout.\n");

                                youCanContinue = false;
                                break;
                            }
                            else
                            {
                                if(captchaGetResponse.Content != null)
                                {
                                    resetProgramContentNull = 0;

                                    captchaJson = JObject.Parse(captchaGetResponse.Content);

                                    if(captchaJson["errorId"] != null)
                                    {
                                        if(captchaJson["errorId"].ToString() == "1")
                                        {
                                            resetProgramErrorIdNull = 0;

                                            if(captchaJson["errorCode"] != null)
                                            {
                                                
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve captcha. Message: "+ captchaJson["errorCode"].ToString()+"\n");
                                                youCanContinue = false;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve captcha and don't know reason.\n");
                                                youCanContinue = false;
                                                break;
                                            }

                                        }
                                        else if(captchaJson["errorId"].ToString() == "0")
                                        {
                                            resetProgramErrorIdNull = 0;
                                            if(captchaJson["status"] != null)
                                            {
                                                resetProgramStatusNull = 0;
                                                if (captchaJson["status"].ToString().ToLower() == "processing")
                                                {
                                                    if(processingCounter == 0)
                                                    {
                                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Solving captcha] Waiting for captcha answer.\n");
                                                        processingCounter++;
                                                    }
                                                }
                                                else if(captchaJson["status"].ToString().ToLower() == "ready")
                                                {
                                                    if(captchaJson["solution"] != null)
                                                    {
                                                        if(captchaJson["solution"]["gRecaptchaResponse"] != null)
                                                        {
                                                           if(captchaJson["solution"]["gRecaptchaResponse"].ToString() != null)
                                                            {
                                                                CaptchaAnswer = captchaJson["solution"]["gRecaptchaResponse"].ToString();
                                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Captcha Task 3/3] Captcha solved and starting to join.\n");
                                                                youCanContinue = true;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't get captcha answer.\n");
                                                                youCanContinue = false;
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't get captcha answer.\n");
                                                            youCanContinue = false;
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't get captcha answer.\n");
                                                        youCanContinue = false;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                resetProgramStatusNull++;
                                            }

                                        }
                                        else
                                        {
                                            resetProgramErrorIdNull++;

                                        }
                                    }
                                    else
                                    {
                                        resetProgramErrorIdNull++;
                                    }
                                }
                                else
                                {
                                    resetProgramContentNull++;
                                }



                            }
                        }

                    }
                }
                else
                {
                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't gell all captcha infos.\n");

                    youCanContinue = false;
                }

                if(youCanContinue && CaptchaAnswer != "")
                {

                    var joinObj = new 
                    {
                    captcha_key = CaptchaAnswer,
                    captcha_rqtoken = CaptchaRqToken
                    
                    };

                    JoinRequest = AddBaseHeaders(user.Token);

                    JoinRequest.Timeout = 10000;
               

                    JoinRequest.AddBody(JObject.FromObject(joinObj).ToString(), "application/json");
                
                     joinResponse = await DiscordRestClient.ExecutePostAsync(JoinRequest);


                    if ((int)joinResponse.StatusCode == 0)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            await Task.Delay(3000);
                            joinResponse = await DiscordRestClient.ExecutePostAsync(JoinRequest);


                            if ((int)joinResponse.StatusCode == 0)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Proxy Problem] There is a proxy problem on last join, trying to solving. Try count: " +(i + 1)+ "\n");

                            }
                            else
                            {
                                youCanContinue = true;
                                break;
                            }


                        }

                        if ((int)joinResponse.StatusCode == 0)
                        {
                            youCanContinue = false;
                        }
                    }

                    if (youCanContinue) // First
                    {
                        if ((int)joinResponse.StatusCode == 200)
                        {
                            needsCaptcha = false;
                            joinJson = JObject.Parse(joinResponse.Content);

                            var guildName = "";

                            if (joinJson["guild"] != null)
                            {
                                if (joinJson["guild"]["name"] != null)
                                {
                                    guildName = joinJson["guild"]["name"].ToString();
                                }

                            }


                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + " [Finished last joining] Joined to " + guildName  + "\n");

                            youCanContinue = true;
                        } // Joined 

                        else if ((int)joinResponse.StatusCode == 400)
                        {

                            if (joinResponse.Content != null)
                            {
                                joinJson = JObject.Parse(joinResponse.Content.ToString());

                                if (joinJson["captcha_rqdata"] != null)
                                {

                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] There is captcha problem and can't start joining."+ "\n");
                                    youCanContinue = false;

                                }
                                else
                                {
                                    if (joinJson["message"] != null)
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] "+ (int)joinResponse.StatusCode+" error. Message: ."+joinJson["message"]+ "\n");
                                        youCanContinue = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] "+ (int)joinResponse.StatusCode+" error and there is no message."+ "\n");
                                        youCanContinue = false;

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] "+ (int)joinResponse.StatusCode+" error and there is no message."+ "\n");
                                youCanContinue = false;
                            }


                        } // Bad Request

                        else if ((int)joinResponse.StatusCode == 401)
                        {
                            youCanContinue = false;
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Stopped last joining] Not valid or banned token. token[{user.Token}]\n");
                        } // Unauthorized

                        else if ((int)joinResponse.StatusCode == 429)
                        {
                            youCanContinue = false;
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $" [Stopped last joining] Your IP is rate limited.\n");
                        }  // Too Many Request

                        else if ((int)joinResponse.StatusCode == 403)
                        {

                            if (joinResponse.Content != null)
                            {
                                joinJson = JObject.Parse(joinResponse.Content);

                                if (joinJson["message"] != null)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] 403 error and there is a message. Message: " + joinJson["message"].ToString() + "\n");
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] 403 error and there is a message. Message: " + joinJson["message"].ToString()+ "\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] 403 error and there is no message.\n");
                            }

                            youCanContinue = false;
                        }  // Forbidden

                        else if ((int)joinResponse.StatusCode == 404)
                        {
                            if (joinResponse.Content != null)
                            {
                                joinJson = JObject.Parse(joinResponse.Content);

                                if (joinJson["message"] != null)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] 404 error and there is a message. Message: " + joinJson["message"].ToString()+ "\n");
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] 404 error and there is no message."+ "\n");

                                }
                            }
                            else
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] 404 error and there is no message."+ "\n");
                            }

                            youCanContinue = false;

                        } // Not Found

                        else
                        {
                            if (joinResponse.Content != null)
                            {
                                joinJson = JObject.Parse(joinResponse.Content);

                                if (joinJson["message"] != null)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] "+(int)joinResponse.StatusCode+" error and there is a message. Message: " + joinJson["message"].ToString()+ "\n");
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped last joining] "+(int)joinResponse.StatusCode+ " error and there is no message."+ "\n");

                                }
                            }
                            else
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped  last joining] "+ (int)joinResponse.StatusCode+" error and there is no message."+ "\n");
                            }

                            youCanContinue = false;
                        } // Others

                    } // First
                    else
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped joining] Can't solve proxy problem.\n");
                    }


                } // Last join

            } // Captcha Start

            if (youCanContinue) // Start rule
            {
              
                if(guild.thereIsRule)
                {
                  
                    if (guild.ruleJson != null && guild.ruleJson.Trim() != "")
                    {
                       
                        var ruleRequest = AddBaseHeaders(user.Token);



                        ruleRequest.Method = Method.Put;

                        ruleRequest.AddHeader("Content-Type", "application/json");
                        ruleRequest.AddBody(MainGuild.ruleJson, "application/json");
                        DiscordClientOptions.BaseUrl = new Uri($"https://discord.com/api/v9/guilds/{MainGuild.id}/requests/@me");

                        ruleRequest.Timeout = 10000;

                        joinResponse = await DiscordRestClient.ExecutePutAsync(ruleRequest);

                        if ((int)joinResponse.StatusCode == 0)
                        {
                          
                            for (int i = 0; i < 10; i++)
                            {
                                await Task.Delay(3000);
                                joinResponse = await DiscordRestClient.ExecutePutAsync(ruleRequest);

                                if ((int)joinResponse.StatusCode == 0)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Proxy Problem] There is a proxy problem on rule verification, trying to solving. Try count: " +(i + 1)+ "\n");
                                  
                                }
                                else
                                {
                                    youCanContinue = true;
                                    break;
                                }


                            }

                            if((int)joinResponse.StatusCode == 0)
                            {
                                youCanContinue = false;
                            }
                                

                          
                        }
                      
                        if (youCanContinue) // First
                        {
                           
                            if ((int)joinResponse.StatusCode == 201)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Passed rule verify] Passed rules succesfully.\n");
                                youCanContinue = true;
                            } // Passed

                            else if((int)joinResponse.StatusCode == 403)
                            {
                                if(joinResponse.Content != null)
                                {
                                    joinJson = JObject.Parse(joinResponse.Content);

                                    if(joinJson["code"] != null)
                                    {
                                        if (joinJson["code"].ToString().Trim() == "40007")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Failed rule verify] Banned or can't join to guild.\n");
                                        }else if(joinJson["code"].ToString().Trim() == "50001")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Failed rule verify] Can't access to this guild.\n");
                                        }
                                        else
                                        {
                                            if(joinJson["message"] != null)
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed rule verify] Can't pass rules. Message: {joinJson["message"]} \n");
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed rule verify] Can't pass rules. And don't know the reason. \n");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed rule verify] Can't pass rules. And don't know the reason. \n");
                                    }

                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed rule verify] Can't pass rules. And don't know the reason. Status code: {(int)joinResponse.StatusCode} \n");
                                   
                                }
                                youCanContinue = true;
                            } // Can't pass

                            else if((int)joinResponse.StatusCode == 410)
                            {
                              

                                if(joinResponse.Content != null)
                                {
                                    joinJson = JObject.Parse(joinResponse.Content);
                                    if (joinJson["code"] != null)
                                    {
                                        if (joinJson["code"].ToString().Trim() == "50001")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Already passed rules] Rules already passed. \n");

                                        }
                                        else
                                        {
                                            if (joinJson["message"] != null)
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Already passed rules] It's already passed rules. \n");
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Already passed rules?] It say's already passed but we can't be sure (No message). \n");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Already passed rules?] It say's already passed but we can't be sure (No code). \n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Already passed rules?] It say's already passed but we can't be sure (No content). \n");
                                }

                                youCanContinue = true;
                            } // Already Passed

                            else if((int)joinResponse.StatusCode == 400)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Invalid rule info] Rules are invalid. \n");
                                youCanContinue = true;
                            } // Invalid body

                            else if ((int)joinResponse.StatusCode == 401)
                            {
                                youCanContinue = false;
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Stopping verify session] Not valid or banned token. token[{user.Token}]\n");
                            } // Unauthorized

                            else if ((int)joinResponse.StatusCode == 429)
                            {
                                youCanContinue = false;
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Stopping verify session] Your IP is rate limited.\n");
                            }  // Too Many Request

                            else
                            {
                                youCanContinue = true;
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed rule verify] Can't pass rules and don't know reason. Status code: {(int)joinResponse.StatusCode} \n\n");
                            } // Others

                        } // First
                        else
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped rule verifying] Can't solve proxy problem.\n");
                            youCanContinue = true;
                        }

                    }
                    else
                    {
                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "There is rule on "  + guild.name + " but rule json is empty");
                        youCanContinue = true;
                    }
                  

                }

            } // Start rule verify

            if(youCanContinue) // Start react verify
            {
                if(reactSetting != null)
                {
                    if(verifyWithReact)
                    {
                        var reactRequest = AddBaseHeaders(user.Token);



                        reactRequest.Method = Method.Put;

                        reactRequest.AddHeader("Content-Type", "application/json");
                      
                        if(reactSetting.isCustomEmoji)
                        {
                            var reaction = reactSetting.emojiName.Trim().Replace(":","").Trim() + "%3A" + reactSetting.emojiId.Trim();
                            DiscordClientOptions.BaseUrl = new Uri($"https://discord.com/api/v9/channels/{reactSetting.channelId.Trim()}/messages/{reactSetting.messageId.Trim()}/reactions/{reaction}/%40me?location=message");
                        }
                        else
                        {
                            var reaction = System.Web.HttpUtility.UrlEncode(reactSetting.emojiName);
                            DiscordClientOptions.BaseUrl = new Uri($"https://discord.com/api/v9/channels/{reactSetting.channelId.Trim()}/messages/{reactSetting.messageId.Trim()}/reactions/{reaction}/%40me?location=message");
                        }
                      

                        reactRequest.Timeout = 10000;

                        joinResponse = await DiscordRestClient.ExecutePutAsync(reactRequest);

                        if ((int)joinResponse.StatusCode == 0)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                await Task.Delay(3000);
                                joinResponse = await DiscordRestClient.ExecutePutAsync(reactRequest);

                                if ((int)joinResponse.StatusCode == 0)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Proxy Problem] There is a proxy problem on react verification, trying to solving. Try count: " +(i + 1)+ "\n");

                                }
                                else
                                {
                                    youCanContinue = true;
                                    break;
                                }


                            }

                            if ((int)joinResponse.StatusCode == 0)
                            {
                                youCanContinue = false;
                            }



                        }

                        if(youCanContinue) // First
                        {
                            if((int)joinResponse.StatusCode == 204)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Passed react verify] Reacted succesfully.\n");
                                youCanContinue = true;
                            } // Reacted

                            else if((int)joinResponse.StatusCode == 400)
                            {
                                if(joinResponse.Content != null)
                                {
                                    var reactJson = JObject.Parse(joinResponse.Content);

                                    if(reactJson["code"] != null)
                                    {
                                        if(reactJson["code"].ToString().Trim() == "10014")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed because of invalid emoji.\n");
                                        }else if(reactJson["code"].ToString().Trim() == "50035")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed because you input something bad?..\n");
                                        }
                                        else if(reactJson["code"].ToString().Trim() == "10008")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed because of invalid message ID.\n");
                                        }
                                        else if (reactJson["code"].ToString().Trim() == "10003")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed because of invalid channel ID.\n");
                                        }
                                        else
                                        {
                                            if(reactJson["message"] != null)
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And there is a message. Message: {reactJson["message"]}\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And don't know the reason. Status code: {(int)joinResponse.StatusCode}\n");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And don't know the reason. Status code: {(int)joinResponse.StatusCode} \n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And there is no content. Status code: {(int)joinResponse.StatusCode} \n");

                                }
                            } // Bad request

                            else if ((int)joinResponse.StatusCode == 403) 
                            {
                                if (joinResponse.Content != null)
                                {
                                    var reactJson = JObject.Parse(joinResponse.Content);

                                    if (reactJson["code"] != null)
                                    {
                                        if (reactJson["code"].ToString().Trim() == "50001")
                                        {
                                            if (reactJson["message"] != null)
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed (probably missing access). And there is a message. Message: {reactJson["message"]}\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed (probably missing access). And don't know the reason. Status code: {(int)joinResponse.StatusCode}\n");
                                            }
                                        }
                                        else
                                        {
                                            if (reactJson["message"] != null)
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And there is a message. Message: {reactJson["message"]}\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And don't know the reason. Status code: {(int)joinResponse.StatusCode}\n");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And don't know the reason. Status code: {(int)joinResponse.StatusCode} \n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And there is no content. Status code: {(int)joinResponse.StatusCode} \n");

                                }
                                youCanContinue = true;
                            } // Forbidden

                            else if ((int)joinResponse.StatusCode == 404)
                            {
                                if (joinResponse.Content != null)
                                {
                                    var reactJson = JObject.Parse(joinResponse.Content);

                                    if (reactJson["code"] != null)
                                    {
                                        if (reactJson["code"].ToString().Trim() == "10008")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed because of unknown message ID.\n");
                                        }
                                        else if (reactJson["code"].ToString().Trim() == "10003")
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed because of unknown channel ID.\n");
                                        }
                                        else
                                        {
                                            if (reactJson["message"] != null)
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And there is a message. Message: {reactJson["message"]}\n");
                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And don't know the reason. Status code: {(int)joinResponse.StatusCode}\n");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And don't know the reason. Status code: {(int)joinResponse.StatusCode} \n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And there is no content. Status code: {(int)joinResponse.StatusCode} \n");

                                }
                                youCanContinue = true;
                            } // Not found

                            else if ((int)joinResponse.StatusCode == 405)
                            {
                                youCanContinue = true;
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed (probably bad URL). And don't know reason. {(int)joinResponse.StatusCode} \n");
                            } // Method now allowed

                            else if ((int)joinResponse.StatusCode == 401)
                            {
                                youCanContinue = false;
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Stopping react verify] Not valid or banned token. token[{user.Token}]\n");
                            } // Unauthorized

                            else if ((int)joinResponse.StatusCode == 429)
                            {
                                youCanContinue = false;
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $" [Stopping react verify] Your IP is rate limited.\n");
                            }  // Too Many Request

                            else
                            {
                                youCanContinue = true;
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + $"[Failed react verify] Reaction failed. And don't know reason. {(int)joinResponse.StatusCode} \n");

                            } // Others

                        } // React Verify Status
                        else
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "[Stopped react verify] Can't solve proxy problem.\n");
                            youCanContinue = true;

                        }
                    }
                }

            }

        }

        public static RestRequest AddBaseHeaders(string token)
        {
            var request = new RestRequest();
            request.AddHeader("authority", "discord.com");
            request.AddHeader("accept", "*/*");
            request.AddHeader("accept-language", "tr-TR,tr;q=0.9");
            request.AddHeader("authorization", token);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
            request.AddHeader("origin", "https://discord.com");
            request.AddHeader("referer", "https://discord.com/channels/@me");
            request.AddHeader("sec-fetch-dest", "empty");
            request.AddHeader("sec-fetch-mode", "cors");
            request.AddHeader("sec-fetch-site", "same-origin");
            request.AddHeader("sec-gpc", "1");
            request.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
            request.AddHeader("x-debug-options", "bugReporterEnabled");
            request.AddHeader("x-discord-locale", "en-US");
            request.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");
            request.AddHeader("Content-Type", "application/json");
            return request;
        }

        #region Old Join request
        /*  public async Task OldJoinRequest(User user, string url, string proxySt, ReactSettings react)
          {
              #region Join Request
              var isJoined = false;
              var proxyError = false;

              string username = user.UserName + "#" + user.Discriminator;

              var clientOptions = new RestClientOptions();
              #region Proxy
              if (proxySt != "" && proxySt != "null" && proxySt != null &&proxySt.Contains(":") && useProxy)
              {
                  var sp = proxySt.Split(':');

                  string proxyUser = sp[2].Replace(":", "").Trim();
                  string proxyPass = sp[3].Replace(":", "").Trim();


                  WebProxy myproxy = new WebProxy(sp[0].Replace(":", "").Trim() + ":" + sp[1].Replace(":", "").Trim());

                  myproxy.Credentials= new NetworkCredential(proxyUser, proxyPass);

                  clientOptions.Proxy = myproxy;
              }
              #endregion

              clientOptions.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36";
              clientOptions.BaseUrl = new Uri("https://discord.com/api/v9/invites/" + url);
              var client = new RestClient(clientOptions);


              var joinRequest = new RestRequest();

              joinRequest.AddHeader("authority", "discord.com");
              joinRequest.AddHeader("accept", "*/
        /*
            joinRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
            joinRequest.AddHeader("authorization", user.Token);
            joinRequest.AddHeader("content-type", "application/json");
            joinRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
            joinRequest.AddHeader("origin", "https://discord.com");
            joinRequest.AddHeader("referer", "https://discord.com/channels/@me");
            joinRequest.AddHeader("sec-fetch-dest", "empty");
            joinRequest.AddHeader("sec-fetch-mode", "cors");
            joinRequest.AddHeader("sec-fetch-site", "same-origin");
            joinRequest.AddHeader("sec-gpc", "1");
            joinRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
            joinRequest.AddHeader("x-debug-options", "bugReporterEnabled");
            joinRequest.AddHeader("x-discord-locale", "en-US");
            joinRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");

            var body = "{}";
            joinRequest.Method = Method.Post;
            joinRequest.AddBody(body, "application/json");
            joinRequest.AddHeader("Content-Type", "application/json");
            #endregion

            var responseText = "";

            #region Join Response
            var response = await client.ExecuteAsync(joinRequest);


            if (response.Content != null)
            {
                responseText = response.Content;
                proxyError = false;

            }



            if (response.ErrorMessage != null)
            {

                if (CheckIfProxyError(response.ErrorMessage))
                {
                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Proxy error. Trying again! - " + response.Content+ " - \n");
                    proxyError = true;
                }
            }
            #endregion

            #region Proxy Fix
            if (proxyError)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (!proxyError)
                    {
                        break;
                    }
                    await Task.Delay(3000);
                    response = await client.ExecuteAsync(joinRequest);

                    if (response.Content != null)
                    {
                        responseText = response.Content;
                        proxyError = false;
                    }


                    if (response.ErrorMessage != null)
                    {

                        if (CheckIfProxyError(response.ErrorMessage))
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Proxy error. Trying again! - " + response.Content+ " - \n");
                            proxyError = true;
                        }
                    }
                }
            }
            #endregion

            if (!proxyError)
            {
                if (responseText != "")
                {

                    var startVerify = false;

                    #region Join Test
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var guildName = responseText.Substring(responseText.IndexOf("\"name\":") + "\"name\":".Length);
                        guildName = guildName.Trim();
                        guildName = guildName.Remove(0, 1);
                        guildName = guildName.Remove(guildName.IndexOf("\""));
                        guildName = guildName.Trim();


                        if (MainGuild != null)
                        {

                            if (MainGuild.thereIsRule)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Joined to " + $"\"{guildName}\" (There is RULE on guild)\n");
                                var ruleRequest = new RestRequest();
                                ruleRequest.AddHeader("authority", "discord.com");
                                ruleRequest.AddHeader("accept", "*/
        /*
                                ruleRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
                                ruleRequest.AddHeader("authorization", user.Token);
                                ruleRequest.AddHeader("content-type", "application/json");
                                ruleRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
                                ruleRequest.AddHeader("origin", "https://discord.com");
                                ruleRequest.AddHeader("referer", "https://discord.com/channels/@me");
                                ruleRequest.AddHeader("sec-fetch-dest", "empty");
                                ruleRequest.AddHeader("sec-fetch-mode", "cors");
                                ruleRequest.AddHeader("sec-fetch-site", "same-origin");
                                ruleRequest.AddHeader("sec-gpc", "1");
                                ruleRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
                                ruleRequest.AddHeader("x-debug-options", "bugReporterEnabled");
                                ruleRequest.AddHeader("x-discord-locale", "en-US");
                                ruleRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");


                                ruleRequest.Method = Method.Put;

                                ruleRequest.AddHeader("Content-Type", "application/json");
                                ruleRequest.AddBody(MainGuild.ruleJson, "application/json");

                                clientOptions.BaseUrl = new Uri($"https://discord.com/api/v9/guilds/{MainGuild.id}/requests/@me");
                                bool ruleProxyError = false;
                                var rp = await client.PutAsync(ruleRequest);

                                bool failed = false;
                                if (rp.Content != null)
                                {
                                    var jsonData = JObject.Parse(rp.Content);

                                    if (jsonData["message"] != null)
                                    {
                                        ruleProxyError = false;
                                        failed = true;

                                    }
                                    else if (jsonData["application_status"] != null)
                                    {
                                        if (jsonData["application_status"].ToString().ToLower().Contains("appr"))
                                        {
                                            failed = false;
                                        }
                                    }
                                    else
                                    {
                                        ruleProxyError = false;

                                    }
                                }
                                if (rp.ErrorMessage != null)
                                {
                                    if (CheckIfProxyError(rp.ErrorMessage))
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Proxy error. Trying again!\n");
                                        ruleProxyError = true;
                                    }
                                }
                                if (!failed)
                                {
                                    if (ruleProxyError)
                                    {
                                        for (int c = 0; c < 20; c++)
                                        {
                                            await Task.Delay(3000);

                                            rp = await client.PutAsync(ruleRequest);

                                            if (rp.Content != null)
                                            {
                                                var jsonData = JObject.Parse(rp.Content);

                                                if (jsonData["message"] != null)
                                                {
                                                    ruleProxyError = false;
                                                    failed = true;
                                                    break;
                                                }
                                                else if (jsonData["application_status"] != null)
                                                {
                                                    if (jsonData["application_status"].ToString().ToLower().Contains("appr"))
                                                    {
                                                        failed = false;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    ruleProxyError = false;
                                                    c += 15;

                                                }
                                            }
                                            if (rp.ErrorMessage != null)
                                            {
                                                if (CheckIfProxyError(rp.ErrorMessage))
                                                {
                                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Proxy error. Trying again!\n");
                                                    ruleProxyError = true;
                                                }
                                            }
                                        }
                                    }
                                }


                                if (!failed)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "succesfully pass the rules on " + $"\"{guildName}\"\n");
                                    startVerify = true;
                                }
                                else
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "can't pass the rules on " + $"\"{guildName}\"\n");
                                    startVerify = true;
                                }

                            }
                            else
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Joined to " + $"\"{guildName}\" (There is NO RULE on guild)\n");
                                startVerify = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Joined to " + $"\"{guildName}\"\n");
                            startVerify = true;
                        }
                        isJoined = true;

                        if (verifyWithReact && startVerify)
                        {

                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Starting to react verify on" + $"\"{guildName}\"\n");

                            var emoji = "";

                            if (reactSetting.isCustomEmoji)
                            {
                                emoji = reactSetting.emojiName.Replace(":", "");
                                emoji += "%"+reactSetting.emojiId;
                            }
                            else
                            {
                                emoji = reactSetting.emojiName.Replace(":", "");
                            }
                            var reqUrlVerify = $"https://discord.com/api/v9/channels/{reactSetting.channelId}/messages/{reactSetting.messageId}/reactions/{emoji}/%40me?location=Message";

                            var reactVerifyRequest = new RestRequest();

                            reactVerifyRequest.AddHeader("authority", "discord.com");
                            reactVerifyRequest.AddHeader("accept", "*/
        /*
                            reactVerifyRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
                            reactVerifyRequest.AddHeader("authorization", user.Token);
                            reactVerifyRequest.AddHeader("content-type", "application/json");
                            reactVerifyRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
                            reactVerifyRequest.AddHeader("origin", "https://discord.com");
                            reactVerifyRequest.AddHeader("referer", "https://discord.com/channels/@me");
                            reactVerifyRequest.AddHeader("sec-fetch-dest", "empty");
                            reactVerifyRequest.AddHeader("sec-fetch-mode", "cors");
                            reactVerifyRequest.AddHeader("sec-fetch-site", "same-origin");
                            reactVerifyRequest.AddHeader("sec-gpc", "1");
                            reactVerifyRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
                            reactVerifyRequest.AddHeader("x-debug-options", "bugReporterEnabled");
                            reactVerifyRequest.AddHeader("x-discord-locale", "en-US");
                            reactVerifyRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");

                            reactVerifyRequest.Method = Method.Put;


                            clientOptions.BaseUrl = new Uri(reqUrlVerify);

                            var rp = await client.PutAsync(reactVerifyRequest);


                            if (rp.StatusCode == HttpStatusCode.NoContent)
                            {
                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Succesfully react verified on " + $"\"{guildName}\"\n");
                            }
                            else
                            {
                                if (rp.StatusCode == HttpStatusCode.Unauthorized)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Can't react verify because this token is not working!");
                                }
                                else if (rp.StatusCode == HttpStatusCode.TooManyRequests)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Can't react verify because you send too many request!");
                                }
                                else if (rp.StatusCode == HttpStatusCode.BadRequest || rp.StatusCode == HttpStatusCode.NotFound)
                                {
                                    //YARRAK
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Can't react verify because: ");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!responseText.Contains("\"message\":"))
                        {
                            if (responseText.Contains("captcha_key"))
                            {

                                if (responseText.Contains("captcha_sitekey"))
                                {

                                    if (responseText.Contains("captcha_rqtoken"))
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Needs captcha for join!" + "\n");
                                        isJoined = false;

                                    }

                                }

                            }
                            else
                            {
                                if (response.ErrorException != null)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+response.ErrorException.Message + "\n");
                                    isJoined = true;
                                }
                            }
                        }

                        if (responseText.Contains("\"message\":"))
                        {

                            var message = responseText.Substring(responseText.IndexOf("message\":") + "message\":".Length);
                            message = message.Trim();
                            message = message.Remove(0, 1);
                            message = message.Remove(message.IndexOf("\""));

                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + message + "\n");
                            isJoined = true;
                        }




                    }
                    #endregion
                }
                else
                {

                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Can't join to server!\n");
                }


                if (!isJoined)
                {
                    if (responseText.Contains("captcha"))
                    {
                        #region Captcha Request
                        string taskId = "";
                        var jsonData = JObject.Parse(responseText);
                        string solution = "";
                        var siteKey = jsonData["captcha_sitekey"].ToString();
                        var rqToken = jsonData["captcha_rqtoken"].ToString();
                        var rqData = jsonData["captcha_rqdata"].ToString();


                        var jsonSt = "";


                        HttpWebRequest requestCaptcha = (HttpWebRequest)WebRequest.Create(@"https://api.capmonster.cloud/createTask");
                        requestCaptcha.Method = "POST";
                        requestCaptcha.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36";
                        requestCaptcha.KeepAlive = false;


                        var bodyCaptcha = "{    \"clientKey\":\""+captchaKey+"\",    \"task\":    {        \"type\":\"HCaptchaTaskProxyless\",      " +
                            "  \"websiteURL\":\"https://discord.com\",        \"websiteKey\":\""+siteKey+"\",     " +
                            "   \"data\":\""+rqData+"\",       " +
                            " \"userAgent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.67 Safari/537.36\",           \"isInvisible\":true    }}";

                        byte[] bytes2 = Encoding.UTF8.GetBytes(bodyCaptcha);

                        using (var st = requestCaptcha.GetRequestStream())
                        {
                            st.Write(bytes2, 0, bytes2.Length);

                        }
                        #endregion

                        #region Solve Captcha Response
                        try
                        {
                            using (var rp = await requestCaptcha.GetResponseAsync() as HttpWebResponse)
                            {
                                using (var st = rp.GetResponseStream())
                                {

                                    jsonSt = new StreamReader(st).ReadToEnd();

                                }

                                rp.Dispose();

                            }
                        }

                        catch (WebException ex)
                        {
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Can't start solving captcha!\n");


                        }
                        #endregion

                        #region Task ID
                        if (jsonSt.Contains("task"))
                        {

                            taskId =jsonSt.Substring(jsonSt.IndexOf("\"taskId\":") + "\"taskId\":".Length);
                            taskId = taskId.Remove(taskId.IndexOf("}"));
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+"Captcha solving started. Task Id: " + taskId + "\n");
                        }
                        #endregion

                        #region Wait Captcha
                        if (taskId != "")
                        {
                            var bodyCaptcha2 = "{    \"clientKey\":\""+captchaKey+"\",    \"taskId\": "+taskId+"}";
                            var clientCaptchaGet = new RestClient("https://api.capmonster.cloud/getTaskResult");
                            RestRequest restRequest = new RestRequest();
                            restRequest.Method = Method.Get;
                            restRequest.AddBody(bodyCaptcha2, "application/json");
                            restRequest.AddHeader("Content-Type", "application/json");


                            int HowMany = 0;

                            for (int i = 0; i < 50; i++)
                            {
                                var responseCaptchaGet = await clientCaptchaGet.ExecuteAsync(restRequest);
                                await Task.Delay(4000);

                                if (HowMany >= 15)
                                {
                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+  "Can't solve captcha!\n");
                                    break;
                                }
                                if (responseCaptchaGet.Content != null)
                                {


                                    if (responseCaptchaGet.Content.Contains("\"errorId\":0"))
                                    {

                                        if (responseCaptchaGet.Content.Contains("\"solution\":{\"gRecaptchaResponse\":"))
                                        {

                                            solution = responseCaptchaGet.Content.Substring(responseCaptchaGet.Content.IndexOf("\"gRecaptchaResponse\":\"") + "\"gRecaptchaResponse\":\"".Length);
                                            solution = solution.Remove(solution.IndexOf("\"}"));
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+  "Captcha solved!\n");
                                            break;

                                        }
                                    }
                                    else
                                    {
                                        HowMany++;
                                    }

                                }



                            }






                        }
                        #endregion

                        #region Last Join
                        if (solution != "")
                        {
                            responseText = "";
                            #region Last Join Request
                            joinRequest = new RestRequest();
                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber())+  "Joining starts!\n");
                            var Joinbody = "{    \"captcha_key\": \""+solution+"\",    \"captcha_rqtoken\": \""+rqToken+"\"}";

                            joinRequest.AddHeader("authority", "discord.com");
                            joinRequest.AddHeader("accept", "*/
        /*
                            joinRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
                            joinRequest.AddHeader("authorization", user.Token);
                            joinRequest.AddHeader("content-type", "application/json");
                            joinRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
                            joinRequest.AddHeader("origin", "https://discord.com");
                            joinRequest.AddHeader("referer", "https://discord.com/channels/@me");
                            joinRequest.AddHeader("sec-fetch-dest", "empty");
                            joinRequest.AddHeader("sec-fetch-mode", "cors");
                            joinRequest.AddHeader("sec-fetch-site", "same-origin");
                            joinRequest.AddHeader("sec-gpc", "1");
                            joinRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
                            joinRequest.AddHeader("x-debug-options", "bugReporterEnabled");
                            joinRequest.AddHeader("x-discord-locale", "en-US");
                            joinRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");


                            joinRequest.Method = Method.Post;

                            joinRequest.AddHeader("Content-Type", "application/json");
                            joinRequest.AddBody(Joinbody, "application/json");



                            for (int i = 0; i < 50; i++)
                            {
                                if (i != 0)
                                {

                                    if (!proxyError)
                                    {
                                        break;
                                    }
                                }

                                await Task.Delay(3000);

                                var LastResponse = await client.ExecuteAsync(joinRequest);

                                if (LastResponse.Content != null)
                                {
                                    responseText = LastResponse.Content;
                                    proxyError = false;
                                }



                                if (LastResponse.ErrorMessage != null)
                                {

                                    if (CheckIfProxyError(LastResponse.ErrorMessage))
                                    {
                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Proxy error. Trying again!\n");
                                        proxyError = true;
                                    }
                                }


                                if (responseText != null)
                                {
                                    if (responseText.Contains("inviter"))
                                    {
                                        var guildName = responseText.Substring(responseText.IndexOf("\"name\":") + "\"name\":".Length);
                                        guildName = guildName.Trim();
                                        guildName = guildName.Remove(0, 1);
                                        guildName = guildName.Remove(guildName.IndexOf("\""));
                                        guildName = guildName.Trim();

                                        if (MainGuild != null)
                                        {

                                            if (MainGuild.thereIsRule)
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Joined to " + $"\"{guildName}\" (There is RULE on guild)\n");
                                                var ruleRequest = new RestRequest();
                                                ruleRequest.AddHeader("authority", "discord.com");
                                                ruleRequest.AddHeader("accept", "*/
        /*
                                                ruleRequest.AddHeader("accept-language", "tr-TR,tr;q=0.9");
                                                ruleRequest.AddHeader("authorization", user.Token);
                                                ruleRequest.AddHeader("content-type", "application/json");
                                                ruleRequest.AddHeader("cookie", "__dcfduid=758033a0d84211ec9caf733e2b575db9; __sdcfduid=758033a1d84211ec9caf733e2b575db98d0ca39a1bd2002cfe14f206798526d4a6bf199b7bc54a88b487da0e94e1ec50; __cf_bm=2dAL92ohw8WPQAMuwRorN3wxUtrFZSEAE1LUQXbOAks-1653062931-0-Ab9PxNsraoIWxkqotfSqGnWzLxeobHai20xH50mVzjsBUvscQreIDzMNan7Vbm46HQVZjRqgGHw/77xY/WYJbb52XAcq4xrWM+dpTKs3aKpIWKcfdW7SdzqrMMAewnvbzw==; OptanonConsent=isIABGlobal=false&datestamp=Fri+May+20+2022+19%3A08%3A53+GMT%2B0300+(GMT%2B03%3A00)&version=6.33.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1&AwaitingReconsent=false; locale=en-US; __dcfduid=da8f943490ef11ec8d8442010a0a03db; __sdcfduid=da8f943490ef11ec8d8442010a0a03db5dde32157b14354231a1bfe8f6e66863844766a6daf80ca4a204843a99751d30");
                                                ruleRequest.AddHeader("origin", "https://discord.com");
                                                ruleRequest.AddHeader("referer", "https://discord.com/channels/@me");
                                                ruleRequest.AddHeader("sec-fetch-dest", "empty");
                                                ruleRequest.AddHeader("sec-fetch-mode", "cors");
                                                ruleRequest.AddHeader("sec-fetch-site", "same-origin");
                                                ruleRequest.AddHeader("sec-gpc", "1");
                                                ruleRequest.AddHeader("x-context-properties", "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6Ijk3NzAyODkyMjUxODY4MzY1OCIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI5NzcwMjg5MjMwNTk3NDQ4NDAiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9");
                                                ruleRequest.AddHeader("x-debug-options", "bugReporterEnabled");
                                                ruleRequest.AddHeader("x-discord-locale", "en-US");
                                                ruleRequest.AddHeader("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6InRyLVRSIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzEwMS4wLjQ5NTEuNjcgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjEwMS4wLjQ5NTEuNjciLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LnJlZGRpdC5jb20vIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5yZWRkaXQuY29tIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjEyOTAzMCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");


                                                ruleRequest.Method = Method.Put;

                                                ruleRequest.AddHeader("Content-Type", "application/json");
                                                ruleRequest.AddBody(MainGuild.ruleJson, "application/json");
                                                clientOptions.BaseUrl = new Uri($"https://discord.com/api/v9/guilds/{MainGuild.id}/requests/@me");
                                                bool ruleProxyError = false;
                                                var rp = await client.PutAsync(ruleRequest);
                                                bool failed = false;
                                                if (rp.Content != null)
                                                {
                                                    jsonData = JObject.Parse(rp.Content);

                                                    if (jsonData["message"] != null)
                                                    {
                                                        ruleProxyError = false;
                                                        failed = true;
                                                    }
                                                    else if (jsonData["application_status"] != null)
                                                    {
                                                        if (jsonData["application_status"].ToString().ToLower().Contains("appr"))
                                                        {
                                                            failed = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ruleProxyError = false;

                                                    }
                                                }
                                                if (rp.ErrorMessage != null)
                                                {
                                                    if (CheckIfProxyError(rp.ErrorMessage))
                                                    {
                                                        Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Proxy error. Trying again!\n");
                                                        ruleProxyError = true;
                                                    }
                                                }
                                                if (!failed)
                                                {
                                                    if (ruleProxyError)
                                                    {
                                                        for (int c = 0; c < 20; c++)
                                                        {
                                                            await Task.Delay(3000);

                                                            rp = await client.PutAsync(ruleRequest);

                                                            if (rp.Content != null)
                                                            {
                                                                jsonData = JObject.Parse(rp.Content);

                                                                if (jsonData["message"] != null)
                                                                {
                                                                    ruleProxyError = false;
                                                                    failed = true;
                                                                    break;
                                                                }
                                                                else if (jsonData["application_status"] != null)
                                                                {
                                                                    if (jsonData["application_status"].ToString().ToLower().Contains("appr"))
                                                                    {
                                                                        failed = false;
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ruleProxyError = false;
                                                                    c += 15;

                                                                }
                                                            }
                                                            if (rp.ErrorMessage != null)
                                                            {
                                                                if (CheckIfProxyError(rp.ErrorMessage))
                                                                {
                                                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Proxy error. Trying again!\n");
                                                                    ruleProxyError = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }


                                                if (!failed)
                                                {
                                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "succesfully pass the rules on " + $"\"{guildName}\"\n");
                                                }
                                                else
                                                {
                                                    Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "can't pass the rules on " + $"\"{guildName}\"\n");
                                                }

                                            }
                                            else
                                            {
                                                Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Joined to " + $"\"{guildName}\" (There is NO RULE on guild)\n");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(Username(username, new System.Diagnostics.StackFrame(0, true).GetFileLineNumber()) + "Joined to " + $"\"{guildName}\"\n");
                                        }



                                        isJoined = true;
                                        break;
                                    }
                                }


                            }


                            #endregion
                        }
                        #endregion
                    }
                }
            }
        }

        */
        #endregion

        #endregion

        #region Join Delay

        private void JoinDelayTextBox_TextChanged(object sender, EventArgs e)
        {
            delayString = JoinDelayTextBox.Text.Trim();
        }

        private void JoinDelayTextBox_GotFocus(object sender, EventArgs e)
        {
            if (JoinDelayTextBox.Text == "Enter join delay... ")
            {
                JoinDelayTextBox.Text = "";
                JoinDelayTextBox.ForeColor = System.Drawing.SystemColors.ControlText;

            }
        }

        private void JoinDelayTextBox_LostFocus(object sender, EventArgs e)
        {
            if (JoinDelayTextBox.Text.Length == 0)
            {
                JoinDelayTextBox.Text = "Enter join delay... ";
                JoinDelayTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            }
            else
            {
                if (JoinDelayTextBox.Text == "Enter join delay... ")
                {
                    JoinDelayTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                }
            }
        }

        #endregion
    }
}
