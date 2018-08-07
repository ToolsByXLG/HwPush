using HwPush.HwBase;
using HwPush.HwModel;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Forms;
namespace HwPush.Push
{
    public partial class QQLogin : Form
    {
        public QQLogin()
        {
            InitializeComponent();
        }
        string strClient = ""; string VersionStrJM = "";string publicKey = "";string canquns = "";
        private void QQLogin_Load(object sender, EventArgs e)
        {
            this.Text = "华为手机优先推送 v" + Application.ProductVersion.ToString();

            try
            {

                Version ver = System.Environment.OSVersion.Version;

                if (ver.Major == 5 && ver.Minor == 1)
                {
                    strClient = "Win XP";
                }
                else if (ver.Major == 6 && ver.Minor == 0)
                {
                    strClient = "Win Vista";
                }
                else if (ver.Major == 6 && ver.Minor == 1)
                {
                    strClient = "Win 7";
                }
                else if (ver.Major == 6 && ver.Minor == 2)
                {
                    strClient = "Win 10";
                }
                else if (ver.Major == 5 && ver.Minor == 0)
                {
                    strClient = "Win 2000";
                }
                else
                {
                    strClient = "未知";
                }
                this.Text = this.Text + " 系统:" + strClient;// + " 群:577072975";
            }
            catch (Exception ee) { MessageBox.Show("获取系统版本异常!" + ee.Message, "错误", MessageBoxButtons.OK); }
            WebBrowser wbo = new WebBrowser();
            //wbo.Url = new Uri("http://qun.qq.com/member.html#gid=577072975");

            wbo.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbo_DocumentCompleted);
            webBrowser1.Url = new Uri("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=715030901&daid=73&hide_close_icon=1&pt_no_auth=1&s_url=http://qun.qq.com/member.html");
            VersionStrJM = PublicClass.GetYouDaoShare();
            publicKey = PublicClass.GetpublicKey();
            textBox1.Text = PublicClass.GetKeyFile();
            canquns = PublicClass.GetCanQun();
            //webBrowser2.Url = new Uri("https://xltfm.taobao.com");
        }
        string cbck = "";
        void wbo_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser back = (WebBrowser)sender;
            back.Document.ExecCommand("Copy", false, null);
            cbck = back.Document.Cookie;

        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser back = (WebBrowser)sender;
            if (e.Url != new Uri("http://qun.qq.com/member.html"))
            {
                back.Document.Cookie = cbck;
                back.Document.ExecCommand("Copy", false, null);
            }
            else
           if (e.Url == new Uri("http://qun.qq.com/member.html"))
            {

                webBrowser1.Stop();
                webBrowser1.Height = 0;
                webBrowser1.Width = 0;
                back.Document.Cookie = cbck;
                back.Document.Body.InnerHtml = "<p>正在打开升级界面...请稍等...</p>";
                back.Document.ExecCommand("Copy", false, null);

                string qq_cookie_login = back.Document.Cookie;

                int qqs = qq_cookie_login.IndexOf(" uin=o");
                string qqsstr = qq_cookie_login.Substring(qqs + 6);
                int qqe = qqsstr.IndexOf(";");
                qqnumber = qqsstr.Substring(0, qqe).TrimStart('0');
                QQModel qqinfo = new QQModel();

                try
                {
                    LoadQQ(qqnumber, qq_cookie_login, ref qqinfo);
                }
                catch (Exception ee)
                {
                    if (ee.Message.IndexOf("3.5") > -1)
                    {
                        MessageBox.Show("请安装.NET3.5或以上版本框架!" + "\n\r" + ee.Message, "错误", MessageBoxButtons.OK);
                    }
                    else {
                        MessageBox.Show("加载QQ异常!" + "\n\r" + ee.Message, "错误", MessageBoxButtons.OK);
                    }
                    System.Environment.Exit(0);
                    return;
                }
                try
                {


                    // if (MyVersion.UpdateList != null)
                    //{
                    
                    HwPush fbs = new                    HwPush();
                    // fbs.MyVersion = MyVersion;
                    fbs.qqinfo = qqinfo;
                    fbs.qqnumber = qqnumber;
                    fbs.VersionStrJM = VersionStrJM;
                    fbs.publicKey = publicKey;
                    this.Hide();
                    fbs.Show();

                    //}
                    // else
                    // {
                    //    MessageBox.Show("版本库异常!" + bbk, "错误", MessageBoxButtons.OK);
                    //     System.Environment.Exit(0);
                    //    return;
                    // }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("版本库或者跳转异常!" + " " + ee.Message + " " + bbk, "错误", MessageBoxButtons.OK);
                    System.Environment.Exit(0);
                    return;
                }
            }

        }
        string qqnumber = ""; string bbk = "";
        //Mate8Model MyVersion = new Mate8Model();
        private void LoadQQ(string qqnumber, string ck, ref QQModel qqinfo)
        {
            mems myqqinfoSuper = new mems();
            int IsSuper = 0;
            try
            {
                long qq = 0;
                try
                {
                    qq = Convert.ToInt64(qqnumber);
                }
                catch
                {
                    MessageBox.Show("QQ号获取异常!", "错误", MessageBoxButtons.OK);

                    System.Environment.Exit(0);
                    return;
                }

                qqinfo = PublicClass.GetQQInfo(ck);//低调群
                //qqinfo.mems.Where(x => x.card.ToUpper().Contains("v8".ToUpper()));
         


                var qqinfoSuper = PublicClass.GetQQInfo(ck, "586095831");//超级低调群
                if (qqinfoSuper.mems != null)
                    qqinfo = qqinfoSuper;

                try
                {
                    if (qqinfo.mems == null)
                    {
                        
                            var qqinfonc = PublicClass.GetQQInfo(ck, "518363466");//支持ada群
                            if (qqinfonc.mems != null)
                                qqinfo = qqinfonc;
                            //int svrtime = qqinfo.svr_time;
                            //DateTime dtime = UnixTimestamp.ConvertIntDateTime(svrtime);
                            //if (dtime > DateTime.Parse("2016-08-16"))
                            //{
                            //    qqinfo = new QQModel();
                            //    MessageBox.Show("本群软件使用权限超时(2016-08-15)", "错误", MessageBoxButtons.OK);
                            //    System.Environment.Exit(0);
                            //    return;
                            //}
                            //var memsada = qqinfo.mems.Where(m => m.uin == qq).FirstOrDefault();
                            //if (memsada.lv.point < 25)
                            //{
                            //    qqinfo = new QQModel();
                            //    MessageBox.Show("您的活跃值小于25("+ memsada.lv.point.ToString() + ")", "错误", MessageBoxButtons.OK);
                            //    System.Environment.Exit(0);
                            //    return;
                            //} 
                    }
                }
                catch
                {
                    //报错了就算了.
                }



                if (qqinfo != null)
                {
                    var qqlist = qqinfo.mems;

                    if (qqlist != null)
                    {

                        if (canquns.IndexOf(qqinfo.QunNumber.ToString()) < 0)
                        {
                            MessageBox.Show("已停止对群:" + qqinfo.QunNumber.ToString() + "进行推送!", "错误", MessageBoxButtons.OK);
                            System.Environment.Exit(0);
                            return;
                        }



                        try
                        {
                            if ((qqinfo.mems[0].uin != 119564557) || qqinfo.mems[0].role != 0)
                            {
                                MessageBox.Show("登录异常", "错误", MessageBoxButtons.OK);
                                System.Environment.Exit(0);
                                return;
                            }
                            qqinfo.mems = qqlist.Where(m => m.uin == qq).ToList();
                        }
                        catch
                        {
                            MessageBox.Show("获取QQ号信息异常!", "错误", MessageBoxButtons.OK);
                            System.Environment.Exit(0);
                            return;
                        }
                        //if (myqqinfo != null)
                        //{

                        //    if (qqinfoSuper != null)
                        //    {
                        //        var qqlistSuper = qqinfoSuper.mems;

                        //        if (qqlistSuper != null)
                        //        {
                        //            myqqinfoSuper = qqlist.Where(m => m.uin == qq).SingleOrDefault();

                        //            if (myqqinfoSuper != null)
                        //                IsSuper = 1;
                        //        }
                        //    }



                        //    //string card = myqqinfo.card;
                        //    //string Version = PublicClass.NameToVersion(card);
                        //    //bbk = Version;
                        //    //if (string.IsNullOrEmpty(Version))
                        //    //{
                        //    //    MessageBox.Show("请修改你的群名片,名片中必须含有你的版本信息!\n\r例如:\"全网\",\"移动\",\"联通\",\"电信\"!", "错误", MessageBoxButtons.OK);
                        //    //    System.Environment.Exit(0);
                        //    //    return;
                        //    //}
                        //    //MessageBox.Show("你的版本为:"+ Version );



                        //}
                        //else
                        //{
                        //    MessageBox.Show("群里面没有你哦~~~", "错误", MessageBoxButtons.OK);
                        //    System.Environment.Exit(0);
                        //    return;
                        //}
                    }
                    //else
                    //{
                    //    MessageBox.Show("用户库异常,你确定你加我们的群了?~~~", "错误", MessageBoxButtons.OK);
                    //    System.Environment.Exit(0);
                    //    return;
                    //}
                }
                else
                {
                    MessageBox.Show("用户库异常,可能是TX修改了登录机制,请告诉小烈哥?~~~", "错误", MessageBoxButtons.OK);
                    System.Environment.Exit(0);
                    return;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("未知错误~~~" + ee.Message, "错误", MessageBoxButtons.OK);
                System.Environment.Exit(0);
                return;
            }
        }

        private void QQLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keystr = "";
            if (PublicClass.SetKeyFile(textBox1.Text))
            {
                string keystren = PublicClass.GetKeyFile();
                int WeekOfYear = 0;
                int Year = 0;
                int Month = 0;
                int Day = 0;
                PublicClass.GetYearAndWeekOfYear(ref Year, ref Month, ref Day, ref WeekOfYear);
                try
                {
                    try
                    {
                        keystr = DESEncrypt.Decrypt(keystren, "小烈哥" + WeekOfYear + "威武" + Year + "~!@#$%^&*()_+QQ:119564557."+ publicKey) ;
                        MessageBox.Show("Key的有效期为" + Year + "年第" + WeekOfYear + "周到第" + (WeekOfYear + 1) + "周");

                    }
                    catch (Exception ee)
                    {
                        keystr = DESEncrypt.Decrypt(keystren, "小烈哥" + (WeekOfYear - 1) + "威武" + Year + "~!@#$%^&*()_+QQ:119564557." + publicKey);

                        MessageBox.Show("Key的有效期为" + Year + "年第" + (WeekOfYear - 1) + "周到第" + WeekOfYear + "周 请提前更新Key");
                    }
                }
                catch (Exception eex)
                {
                    MessageBox.Show(eex.Message);
                }
            }
        }
    }
}
