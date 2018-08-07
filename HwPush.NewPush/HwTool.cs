using HwPush.HwModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using HwPush.HwBase;
using HwPush.Model;
using HwPush.Base;
using Fiddler;
using System.Threading;

namespace HwPush.NewPush
{
    public partial class HwTool : Form
    {
        public string qqnumber = "";
        public string qqck = "";
        public string qqinfostr = "";
        public myinfo my_info = new myinfo();

        public string PublicKey = "";
        public string PrivateKey = "";
        public long? User_IMEI;



        //public string ApiUrl = "http://120.24.163.11:8878/api/";
        //public string ApiUrl = "http://52.198.54.127:8878/api/";
        public string ApiUrl = "http://localhost:8878/api/";
        public HwTool()
        {
            InitializeComponent();
        }

        private void HwTool_Load(object sender, EventArgs e)
        {

            this.Text = "华为手机优先推送 v" + HTMLHelper.Ver;

            qqnumber = PublicClass.GlobalQQNumber.ToString();
            my_info = JsonConvert.DeserializeObject<myinfo>(qqinfostr);
            string nick = my_info.result.nick;
            //my_info.result.qqnumber = int.Parse(qqnumber);
            //qqinfostr = JsonConvert.SerializeObject(my_info);


            label1.Text = "欢迎您: " + qqnumber + "  " + nick + "";
            listBox4.SetSelected(0, true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            GetKey();
            GetTest();
            label2.Text = "本地IP:\n\r " + PublicClass.GetLocalIp();


            this.BeginInvoke((MethodInvoker)delegate
            {
                GetQQInfo();
                GetValidVersion();
                DialogResult dlsm = MessageBox.Show("本软件的所有包都来自于华为官方,本软件不对升级后导致的任何后果负责!\r\n本软件会收集用户的手机串码和QQ号码信息!\r\n如果介意,请点击否会自动关闭本软件!!\r\n如果点击是,则认同此本声明!", "声明", MessageBoxButtons.YesNo);
                if (dlsm == DialogResult.No)
                {
                    System.Environment.Exit(0);
                    return;
                }
                else
                {
                    SetProxy();
                }
            });
        }
        private void GetTest()
        {
            //var pk = HTMLHelper.Post_Http(ApiUrl + "Version/GetModelVersion", "");
            //var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
            //var rsaqqinfo = pkd.Data.ToString();


            //if (pkd.ResultCode != 1000)
            //{
            //    MessageBox.Show(pkd.Message);
            //    if (pkd.ResultCode == -2)
            //    {
            //        button1_Click(null, null);

            //    }
            //    System.Environment.Exit(0);
            //    return;
            //}


        }
        private void GetQQInfo()
        {
            var pk = HTMLHelper.Post_Http(ApiUrl + "Users/GetQQInfo", "");
            var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
            var rsaqqinfo = pkd.Data.ToString();


            if (pkd.ResultCode != 1000)
            {
                MessageBox.Show(pkd.Message);
                if (pkd.ResultCode == -2)
                {
                    button1_Click(null,null);

                }
                System.Environment.Exit(0);
                return;
            }

            string jsonqqinfo = RsaHelper.RSACrypto.Decrypt(PrivateKey, rsaqqinfo);
            var qqinfo = JsonConvert.DeserializeObject<Hw_Users>(jsonqqinfo);
            label1.Text = "欢迎您:" + qqinfo.QQNumber + " " + qqinfo.UserName + " \n\r剩余积分:" + qqinfo.Gold + " 绑定串码: " + ((qqinfo.IMEI == null) ? "未绑定" : qqinfo.IMEI.Value.ToString());
            User_IMEI = qqinfo.IMEI;
        }
        private void UpdateIMEI(string imei)
        {
            string rsaqqIMEI = RsaHelper.RSACrypto.Encrypt(PublicKey, imei);
            var pk = HTMLHelper.Post_Http(ApiUrl + "Users/UpdateIMEI", rsaqqIMEI);
            var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
            var rsaqqinfo = pkd.Data.ToString();
            string jsonqqinfo = RsaHelper.RSACrypto.Decrypt(PrivateKey, rsaqqinfo);
            var qqinfo = JsonConvert.DeserializeObject<Hw_Users>(jsonqqinfo);
            label1.Text = "欢迎您:" + qqinfo.UserName + " \n\r剩余积分:" + qqinfo.Gold + " 绑定串码: " + ((qqinfo.IMEI == null) ? "未绑定" : qqinfo.IMEI.Value.ToString());
            User_IMEI = qqinfo.IMEI;
        }
        private void GetValidVersion()
        {
            var pk = HTMLHelper.Post_Http(ApiUrl + "Version/GetValidVersion", "");
            var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
            var rsavinfo = pkd.Data.ToString();



            string jsonvinfo = RsaHelper.RSACrypto.Decrypt(PrivateKey, rsavinfo);
            var qqinfo = JsonConvert.DeserializeObject<List<listp>>(jsonvinfo);
            qqinfo.ForEach(x =>
            {
                listBox1.Items.Add(x.FirmWare);
            });
        }
        public class listp
        {
            public string FirmWare;
        }
        private void SetProxy(int iPort = 8877)
        {
            //设置别名
            Fiddler.FiddlerApplication.SetAppDisplayName("FiddlerCoreHw");

            //启动方式
            //FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;
            Fiddler.CONFIG.IgnoreServerCertErrors = false;
            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);


            //启动代理程序，开始监听http请求
            //端口,是否使用windows系统代理（如果为true，系统所有的http访问都会使用该代理）
            Fiddler.FiddlerApplication.Startup(iPort, false, true, true);

            int iSecureEndpointPort = 8888;
            string sSecureEndpointHostname = "127.0.0.1";
            Proxy oSecureEndpoint = null;
            // 我们还将创建一个HTTPS监听器，当FiddlerCore被伪装成HTTPS服务器有用
            // 而不是作为一个正常的CERN样式代理服务器。
            oSecureEndpoint = FiddlerApplication.CreateProxyEndpoint(iSecureEndpointPort, true, sSecureEndpointHostname);

            AddTXT("请将手机和电脑连接在同一网段");
            AddTXT("再设置手机WIFI的代理服务器为电脑IP");
            AddTXT("再设置手机WIFI的代理端口为8877");
            AddTXT("");
            AddTXT("开始监控 下面需要出现 成功链接代理 才能正常获取版本");


            Go();
        }
        int resqustnum = 0;
        private void Go()
        {
            int isconn = 0;
            //定义会话，每一个请求都将封装成一个会话
            List<Fiddler.Session> oAllSessions = new List<Fiddler.Session>();
            string newv = ""; string newdv = ""; long? newIMEI = null; long? thisIMEI = null; bool isnew = false; int newid = 0;
            string hwconfig = "";
            Fiddler.FiddlerApplication.BeforeRequest += delegate (Fiddler.Session oS)
            {
                if (isconn == 0)
                {
                    isconn++;
                    AddTXT("成功链接代理");
                }
                try
                {
                    oS.bBufferResponse = true;
                    Monitor.Enter(oAllSessions);
                    //if (oS.fullUrl.IndexOf("hicloud.com") > -1)
                    if (oS.fullUrl.IndexOf("updatebeta.hicloud.com/TDS/data/") > -1
                    || oS.fullUrl.IndexOf("servicesupport/updateserver/getConfig") > -1
                    || oS.fullUrl.IndexOf("query.hicloud.com") > -1)
                    {
                        AddTXT(oS.host);
                        //AddTXT(oS.hostname);
                        //AddTXT("10%");
                        string v = oS.GetRequestBodyAsString();
                        //AddTXT(v);






                        if (oS.fullUrl.IndexOf("servicesupport/updateserver/getConfig") > -1)
                        {
                            hwconfig = oS.GetRequestBodyAsString();
                            HwConfig hwc = JsonConvert.DeserializeObject<HwConfig>(hwconfig);
                            if (hwc != null)
                            {
                                List<condPara> cpl = hwc.condParaList;
                                if (cpl != null)
                                {
                                    condPara cp = cpl.Where(x => x.key == "IMEI").SingleOrDefault();
                                    if (cp != null)
                                    {
                                        thisIMEI = long.Parse(cp.value);
                                        AddTXT("成功获取串码:" + thisIMEI);

                                        if (User_IMEI == null)
                                        {
                                            User_IMEI = thisIMEI;
                                            UpdateIMEI(cp.value);
                                            AddTXT("串码绑定成功");
                                        }
                                    }
                                }
                            }
                        }











                        if (oS.fullUrl.IndexOf("http://query.hicloud.com:80/sp_ard_common/v2/Check.action") > -1
                            || oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/v2/Check.action") > -1)
                        {



                            string ipc = oS.clientIP.Replace("::ffff:", "");
                            AddTXT("您的手机IP为:" + ipc);
                            if (!PublicClass.IsLAN(ipc))
                            {
                                AddTXT("不允许网远程推送~");
                                return;
                                //myqqinfo.role
                            }




                            Mate8RequestBody rule = JsonConvert.DeserializeObject<Mate8RequestBody>(v);
                            if (resqustnum == 0)
                            {
                                newv = v;
                                //newdv = rule.rules.D_version;
                                newdv = "B" + rule.rules.FirmWare.Split('B')[1].Substring(0, 3);
                                string rsaDeviceName = RsaHelper.RSACrypto.Encrypt(PublicKey, rule.rules.DeviceName);
                                var getlv = HTMLHelper.Post_Http(ApiUrl + "Version/GetLatestVersion", rsaDeviceName);
                                var getlvm = JsonConvert.DeserializeObject<AjaxResCode>(getlv);
                                object da = getlvm.Data;

                                if (da != null && int.Parse(da.ToString().Substring(0, 3)) <= int.Parse(rule.rules.FirmWare.Split('B')[1].Substring(0, 3))
                                || da == null)
                                {
                                    string rsaFirmWare = RsaHelper.RSACrypto.Encrypt(PublicKey, v /*rule.rules.FirmWare*/);
                                    var ref1 = HTMLHelper.Post_Http(ApiUrl + "Version/UpdateVersion", rsaFirmWare);
                                    var ref1m = JsonConvert.DeserializeObject<AjaxResCode>(ref1);
                                    newid = int.Parse(ref1m.Data.ToString());
                                    AddTXT("成功验证版本库");
                                    if (thisIMEI > 0)
                                    {
                                        string rsaIMEI = RsaHelper.RSACrypto.Encrypt(PublicKey, newid + "|" + thisIMEI + "|0");//IMEI
                                        var getv = HTMLHelper.Post_Http(ApiUrl + "Version/SetIMEI", rsaIMEI);
                                        var getvm = JsonConvert.DeserializeObject<AjaxResCode>(getv);
                                        AddTXT("成功验证串码");
                                    }

                                }
                                else
                                {
                                    AddTXT("开始获取在线版本");
                                    //string rsav = RsaHelper.RSACrypto.Encrypt(PublicKey, v /*rule.rules.FirmWare*/);
                                    var getv = HTMLHelper.Post_Http(ApiUrl + "Version/GetVersion", rsaDeviceName);
                                    var getvm = JsonConvert.DeserializeObject<AjaxResCode>(getv);
                                    object dav = getvm.Data;
                                    if (dav != null)
                                    {
                                        if (!string.IsNullOrEmpty(PrivateKey))
                                        {
                                            string rsaqqinfo = RsaHelper.RSACrypto.Decrypt(PrivateKey, dav.ToString());
                                            Hw_UserVersionLibrary uvl = JsonConvert.DeserializeObject<Hw_UserVersionLibrary>(rsaqqinfo);
                                            newv = uvl.Json;
                                            newdv = uvl.version;
                                            newIMEI = uvl.IMEI;
                                            AddTXT("在线版本获取成功");
                                        }
                                    }
                                }

                            }
                            v = newv.Replace(newdv.Substring(0, 3), rule.rules.FirmWare.Split('B')[1].Substring(0, 3));
                            oS.utilSetRequestBody(v);
                            resqustnum++;
                        }
                        if (oS.fullUrl.IndexOf(".zip") > -1)
                        {
                            oS.bBufferResponse = false;
                        }
                        if (oS.fullUrl.IndexOf("updatebeta.hicloud.com/TDS/data/") > -1)
                        {                      
                            Mate8RequestBody IMEIv = JsonConvert.DeserializeObject<Mate8RequestBody>(v);
                            if (User_IMEI == null)
                            {
                                User_IMEI = long.Parse(IMEIv.rules.IMEI);
                                UpdateIMEI(IMEIv.rules.IMEI);
                                AddTXT("串码绑定成功");
                            }
                            if (newid > 0)
                            {
                                isnew = false;

                                string rsaIMEI = RsaHelper.RSACrypto.Encrypt(PublicKey, newid + "|" + IMEIv.rules.IMEI + "|1");//IMEI
                                var getv = HTMLHelper.Post_Http(ApiUrl + "Version/SetIMEI", rsaIMEI);
                                var getvm = JsonConvert.DeserializeObject<AjaxResCode>(getv);
                            }
                            //AddTXT("11%");
                            v = "{\"rules\":{\"IMEI\":\"" + newIMEI + "\"}}";
                            //AddTXT(v);
                            //AddTXT("12%");
                            oS.utilSetRequestBody(v);

                            //AddTXT("13%");
                            if (oS.fullUrl.IndexOf(".zip") > -1)
                            {
                                oS.bBufferResponse = false;
                                AddTXT("正在下载升级包...");
                            }
                            if (oS.fullUrl.IndexOf("changelog.xml") > -1)
                            {
                                AddTXT("加载日志");
                            }
                            if (oS.fullUrl.IndexOf("filelist.xml") > -1)
                            {
                                AddTXT("加载文件列表");
                            }

                        }
                    }


                    //AddTXT("51%");





                    oAllSessions.Add(oS);
                    Monitor.Exit(oAllSessions);
                    oS["X-AutoAuth"] = "(default)";
                    //AddTXT("52%");
                }
                catch (Exception ee)
                {
                    resqustnum = 0;
                    AddTXT(ee.ToString());
                }
            };
            Fiddler.FiddlerApplication.BeforeResponse += delegate (Fiddler.Session oS)
            {

                oS.utilDecodeResponse();

                if (oS.fullUrl.IndexOf("http://query.hicloud.com:80/sp_ard_common/v2/Check.action") > -1
                || oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/v2/Check.action") > -1)
                {
                    #region 获取版本
                    if (isconn == 1)
                    {
                        isconn++;
                        if (oS.fullUrl.IndexOf("http://query.hicloud.com:80/sp_ard_common/v2/Check.action") > -1)
                        {
                            //ctype = 1;
                            AddTXT("正在使用WIFI代理获取升级版本");
                        }
                        else
                        if (oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/v2/Check.action") > -1)
                        {
                            //ctype = 3;
                            AddTXT("正在使用华为手机助手获取升级版本(旧)");
                        }
                        else { AddTXT("路径异常"); }
                    }
                    try
                    {
                        string v = oS.GetResponseBodyAsString();

                        if (v.IndexOf("\"status\":\"1\"") > -1 || v.IndexOf("\"status\":\"-1\"") > -1)
                        {
                            AddTXT("版本获取完毕");
                            resqustnum = 0;
                        }

                        //if (v.IndexOf("EVA-AL00C00B193") > -1||v.IndexOf("EVA-AL00C00B195") > -1)
                        {
                            //v = v.Replace("EVA-AL00C00B193", "EVA-AL10C00B193");
                            oS.utilSetResponseBody(v);
                        }

                        //AddTXT(v);


                        AddTXT("开始加载升级数据");
                        //string v1 = "{\"status\":\"0\",\"components\":[{\"name\":\"EVA-AL00C00B193\",\"version\":\"EVA-AL00C00B193\",\"versionID\":\"64011\",\"description\":\"【B192-B193差分包】EVA-AL00C00B193\",\"ruleAttr\":\"type_A\",\"createTime\":\"2016-10-18T10:48:42+0000\",\"url\":\"https://updatebeta.hicloud.com/TDS/data/FF38E764923BF183D14E26F58988076A9E4423EC65B51AE3526B4888E05AECC2/1477137653/files/p3/s15/G1434/g1433/v64011/f1/\",\"reserveUrl\":\"\",\"versionType\":\"2\"}]}";

                        //oS.utilSetResponseBody(v1);

                    }
                    catch (Exception ee)
                    {
                        AddTXT(ee.ToString());
                    }
                    #endregion
                }
            };
        }




        private void AddTXT(string str)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                textBox1.AppendText((!string.IsNullOrEmpty(str) ? DateTime.Now.ToString("HH:mm:ss.fff") : "") + " " + str + "\r\n");
            });
        }



        private void GetKey()
        {
            AddTXT("开始加载证书");
            IOHelper.DeleteFile(".rsa");
            PublicKey = IOHelper.Read("PublicKey.rsa");
            if (string.IsNullOrEmpty(PublicKey))
            {
                string aa = HTMLHelper.Post_Http(ApiUrl + "Key/GetPublicKey", "");
                var bb = JsonConvert.DeserializeObject<AjaxResCode>(aa);
                PublicKey = bb.Data.ToString();
                IOHelper.CreateFile("PublicKey.rsa");
                IOHelper.Write("PublicKey.rsa", PublicKey);
                AddTXT("PublicKey.rsa 下载成功");
            }
            else
            {
                AddTXT("PublicKey.rsa 加载成功");
            }
            PublicClass.GlobalPublicKey = PublicKey;
            if (string.IsNullOrEmpty(qqnumber))
            {
                MessageBox.Show("QQ号状态错误!","错误", MessageBoxButtons.OK);
                System.Environment.Exit(0);
                return;
            }
            PrivateKey = IOHelper.Read(qqnumber + ".rsa");
            if (string.IsNullOrEmpty(PrivateKey))
            {
                var pk = HTMLHelper.Post_Http(ApiUrl + "Key/GetPrivateKey", "");
                var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
                PrivateKey = pkd.Data.ToString();


                if (pkd.ResultCode == 2)
                {
                    MessageBox.Show("停止证书下载!");
                }
                else
                {
                    if (pkd.ResultCode == 0)
                    {
                        string rsaqqinfo = RsaHelper.RSACrypto.Encrypt(PublicKey, qqinfostr);
                        var newqq = HTMLHelper.Post_Http(ApiUrl + "Users/RegQQ", rsaqqinfo);
                        var newqqres = JsonConvert.DeserializeObject<AjaxResCode>(newqq);
                        PrivateKey = newqqres.Data.ToString();
                        AddTXT("欢迎" + qqnumber + "首次使用本软件");
                    }

                    IOHelper.CreateFile(qqnumber + ".rsa");
                    IOHelper.Write(qqnumber + ".rsa", PrivateKey);
                }
                AddTXT(qqnumber + ".rsa 下载成功");
            }
            else {
                AddTXT(qqnumber + ".rsa 加载成功");
            }
            PublicClass.GlobalPrivateKey = PrivateKey;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dlsm = MessageBox.Show("是否替换新Key，如果可以正常获取版本，请不要替换Key!", "Key更新", MessageBoxButtons.YesNo);
            if (dlsm == DialogResult.Yes)
            {
                IOHelper.DeleteFile(qqnumber + ".rsa");
                IOHelper.DeleteFile("PublicKey.rsa");
                GetKey();
                MessageBox.Show("证书更新成功!");
            }
        }

        private void HwTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dlsm = MessageBox.Show("是否安装安全证书，如果可以正常获取版本，请不要安装安全证书!", "安全证书安装", MessageBoxButtons.YesNo);
            if (dlsm == DialogResult.Yes)
            {
                if (Fiddler.CertMaker.trustRootCert() == true)
                {
                    AddTXT("安全证书安装成功");
                }
            }
        }
    }
}