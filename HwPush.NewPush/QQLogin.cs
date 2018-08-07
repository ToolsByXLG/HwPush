using HwPush.HwBase;
using HwPush.HwModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HwPush.NewPush
{
    public partial class QQLogin : Form
    {
        public QQLogin()
        {
            InitializeComponent();
        }
        string strClient = ""; string VersionStrJM = ""; string publicKey = ""; string canquns = "";
        private void QQLogin_Load(object sender, EventArgs e)
        {
            HTMLHelper.Ver = Application.ProductVersion.ToString();
            this.Text = "华为手机优先推送 v" + HTMLHelper.Ver;

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

            wbo.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbo_DocumentCompleted);
            webBrowser1.Url = new Uri("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=715030901&daid=73&hide_close_icon=1&pt_no_auth=1&s_url=http://qun.qq.com/member.html");

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

                this.Hide();
                back.Document.Cookie = cbck;
                back.Document.Body.InnerHtml = "<p>正在打开升级界面...请稍等...</p>";
                back.Document.ExecCommand("Copy", false, null);

                string qq_cookie_login = back.Document.Cookie;

                int qqs = qq_cookie_login.IndexOf(" uin=o");
                string qqsstr = qq_cookie_login.Substring(qqs + 6);
                int qqe = qqsstr.IndexOf(";");
                qqnumber = qqsstr.Substring(0, qqe).TrimStart('0');
                QQModel qqinfo = new QQModel();
                string qqinfostr = "";
                try
                {
                    qqinfostr = PublicClass.GetQQMyInfo(qq_cookie_login);

                }
                catch (Exception ee)
                {
                    if (ee.Message.IndexOf("3.5") > -1)
                    {
                        MessageBox.Show("请安装.NET3.5或以上版本框架!" + "\n\r" + ee.Message, "错误", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("加载QQ异常!" + "\n\r" + ee.Message, "错误", MessageBoxButtons.OK);
                    }
                    System.Environment.Exit(0);
                    return;
                }
                try
                {


                    // if (MyVersion.UpdateList != null)
                    //{

                    HwTool fbs = new HwTool();

                    //fbs.qqnumber = qqnumber;
                    PublicClass.GlobalQQNumber = Int64.Parse(qqnumber);
                    fbs.qqinfostr = qqinfostr;
                    fbs.qqck = qq_cookie_login;
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


        private void QQLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}