using Fiddler;
using HwPush.HwBase;
using HwPush.HwModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;

namespace HwPush.Push
{
    public partial class HwPush : Form
    {
        public HwPush()
        {
            InitializeComponent();
        }
        int iSecureEndpointPort = 8888;
        string sSecureEndpointHostname = "127.0.0.1";
        Proxy oSecureEndpoint = null;
        public Mate8Model MyVersion = new Mate8Model();
        public List<HwModelInfo> LHMI = new List<HwModelInfo>();
        public List<HwModelInfo> LHMISelect = new List<HwModelInfo>();
        public QQModel qqinfo = new QQModel();
        public string qqnumber;
        public string VersionStrJM;
        public string publicKey;
        string Vtype = "";
        string SelectedVersionName = "";
        public int FullIsGo = 0;
        public int issms = 1;//是否发邮件
        public int isnc = 0;//是否内测
        public int isqy = 0;//是否群员
        public int roles = 2;//是否群员
        public int ctype = 0;//客户端类型
        public string hwconfig = "";//串码的等资料
        public string qqstring = "";//QQ资料
        public bool isan7 = false;//是否安7
        public List<long> hmdlist = new List<long>();//黑名单
        public int isok = 0;//推送成功
        public int iscm = 1;//是否有串码
        public int isycts = 0;//是否延迟推送
        public DateTime svrtime = new DateTime();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "华为手机优先推送 v" + Application.ProductVersion.ToString();

            // this.BeginInvoke((MethodInvoker)delegate
            // {
            DialogResult dlsm = MessageBox.Show("本软件的所有包都来自于华为官方,本软件不对升级后导致的任何后果负责!\r\n如果介意,请点击否会自动关闭本软件!!\r\n如果点击是,则认同此本声明!", "声明", MessageBoxButtons.YesNo);
            if (dlsm == DialogResult.No)
            {
                System.Environment.Exit(0);
                return;
            }
            // });

            if (qqinfo == null)
            {
                System.Environment.Exit(0);
            }

            mems myqqinfo = null;

            List<mems> lme = qqinfo.mems;

            if (lme != null)
            {
                myqqinfo = lme.SingleOrDefault();
                qqstring = JsonConvert.SerializeObject(myqqinfo);
                qqnumber = myqqinfo.uin.ToString();
                isqy = 1;

                svrtime = UnixTimestamp.ConvertIntDateTime(qqinfo.svr_time);
            }
            else
            {
                isqy = 0;
            }

            //if (qqnumber == "119564557" || qqnumber == "891587944" || qqnumber == "289408880" || qqnumber == "834714126" || qqnumber == "591219179")
            //{

            //}
            //else

            //{
            //    MessageBox.Show("没权限使用");
            //    System.Environment.Exit(0);
            //}

            LoadVersion();


            LoadHMD();




            AddTXT("请将手机和电脑连接在同一路由器");
            AddTXT("请设置手机WIFI的代理服务器为电脑IP的8877端口");
            AddTXT("");
            AddTXT("如果不会设置代理服务器，请查看操作说明文档");
            AddTXT("http://note.youdao.com/share/?id=07bf9af8e5e82f536e48a490d3618415&type=note");
            AddTXT("软件更新链接：http://pan.baidu.com/s/1qX0LdeO 密码：rhjx");
            AddTXT("");


            //string card = myqqinfo.card;


            label3.Text = qqnumber;


            //您的手机型号为:" + MyVersion.ModelName + "\n\r
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (myqqinfo != null)
                {
                    label4.Text = "尊敬的:\n\r" + (string.IsNullOrEmpty(myqqinfo.card) ? myqqinfo.nick : myqqinfo.card) + " " + rolestr(myqqinfo.role);
                   // + "您好!\n\r您的群内活跃值为:" + myqqinfo.lv.point;
                    roles = myqqinfo.role;
                }
                else
                {
                    label4.Text = "尊敬的:\n\r游客您好!\n\r需要加入群:577072975 才能使用推送功能\n\r您可以点击底部的\"加群\"按钮申请加入";
                }
            });
            AddTXT("");


            //Mate8List.Mate8Model.ForEach(x =>
            //{
            //    x.UpdateList.ForEach(m =>
            //    {
            //        if (!listBox1.Items.Contains(m.version))
            //        {
            //            if (isnc == 1)
            //            {
            //                listBox1.Items.Insert(0, m.version);
            //            }
            //            else
            //            {
            //                if (Convert.ToDateTime(m.createTime) < UnixTimestamp.ConvertIntDateTime(qqinfo.svr_time))
            //                {
            //                    listBox1.Items.Insert(0, m.version);
            //                }
            //            }
            //        }
            //    });
            //});
            //listBox1.SetSelected(0, true);

            //设置别名
            Fiddler.FiddlerApplication.SetAppDisplayName("FiddlerCoreHw");

            Fiddler.CONFIG.IgnoreServerCertErrors = false;
            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);


            //启动方式
            FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;

            //定义http代理端口
            int iPort = 8877;
            //启动代理程序，开始监听http请求
            //端口,是否使用windows系统代理（如果为true，系统所有的http访问都会使用该代理）
            Fiddler.FiddlerApplication.Startup(iPort, false, true, true);



            // 我们还将创建一个HTTPS监听器，当FiddlerCore被伪装成HTTPS服务器有用
            // 而不是作为一个正常的CERN样式代理服务器。
            Proxy oSecureEndpoint = null;
            int iSecureEndpointPort = 8888;
            string sSecureEndpointHostname = "127.0.0.1";
            oSecureEndpoint = FiddlerApplication.CreateProxyEndpoint(iSecureEndpointPort, true, sSecureEndpointHostname);
            AddTXT("");
            AddTXT("开始监控 下面需要出现 成功链接代理 才能正常获取版本");


            try
            {
                Go();
            }
            catch
            {
                Go();
            }
        }
        public string rolestr(int role)
        {
            switch (role)
            {
                case 0:
                    return "群主";
                case 1:
                    return "群管";
                case 2:
                    return "群员";
                default:
                    return "群员";
            }

        }

        private void GetVersion()
        {
            string VersionStr = "";
            try
            {
                string keystren = PublicClass.GetKeyFile();
                if (string.IsNullOrEmpty(keystren))
                {
                    MessageBox.Show("在工具旁边没找到Key文件哦,名字就是\"Key.txt\",别乱改!\n\r如果你没有Key,请找群管或者群主要!", "错误", MessageBoxButtons.OK);
                    System.Environment.Exit(0);
                    return;
                }



                int WeekOfYear = 0;
                int Year = 0;
                int Month = 0;
                int Day = 0;
                int svrtime = qqinfo.svr_time;
                PublicClass.GetYearAndWeekOfYear(ref Year, ref Month, ref Day, ref WeekOfYear, UnixTimestamp.ConvertIntDateTime(svrtime));
                AddTXT("当前时间为" + Year + "年第" + WeekOfYear + "周(自然周)");
                string keystr = "";
                try
                {
                    keystr = DESEncrypt.Decrypt(keystren, "小烈哥" + WeekOfYear + "威武" + Year + "~!@#$%^&*()_+QQ:119564557." + publicKey);
                    //keystr = keystr.Replace("小烈哥" + WeekOfYear + "威武", "小烈哥" + Month + "威武");
                    AddTXT("Key的有效期为" + Year + "年第" + WeekOfYear + "周到第" + (WeekOfYear + 1) + "周");

                }
                catch (Exception ee)
                {
                    keystr = DESEncrypt.Decrypt(keystren, "小烈哥" + (WeekOfYear - 1) + "威武" + Year + "~!@#$%^&*()_+QQ:119564557." + publicKey);
                    //keystr = keystr.Replace("小烈哥" + (WeekOfYear - 1) + "威武", "小烈哥" + Month + "威武");

                    AddTXT("Key的有效期为" + Year + "年第" + (WeekOfYear - 1) + "周到第" + WeekOfYear + "周 请提前更新Key");
                }
                AddTXT("");
                try
                {
                    //keystr = "小烈哥威武~!@#$%^&*()_+QQ:119564557." + publicKey;
                    VersionStr = DESEncrypt.Decrypt(VersionStrJM.Trim(), keystr);
                    AddTXT("版本库加载成功");
                    AddTXT("");
                }

                catch (Exception ee)
                {
                    MessageBox.Show("软件版本已停用,请联系管理员?~~~" + ee.Message, "错误", MessageBoxButtons.OK);
                    System.Environment.Exit(0);
                    return;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Key解析错误,检查是否过期?~~~" + ee.Message, "错误", MessageBoxButtons.OK);
                System.Environment.Exit(0);
                return;
            }

            try
            {
                VersionStr = HTMLHelper.DelHtml(VersionStr, "br").Replace("&nbsp;", "");

                //VersionStr = "[{|VersionName|:|P9|,|PhoneModel|:|EVA|,|Operator|:null,|VersionDetail|:[{|Name|:|B323|,|IsValid|:|1|,|VersionType|:|1|,|oldversion|:|B323|,|version|:|B323|,|description|:|测B323|,|createTime|:|2016 - 010 - 29T10: 00:00|,|BG|:|1256|,|sg|:|104|,|v|:|65542|,|f|:|1|,|SortId|:0}],|IsValid|:|1|,|SortId|:849,|MobileModel|:[{|Name|:|P9电信|,|PhoneModel|:|EVA - CL00C92|,|Operator|:|telecom|,|IsValid|:|1|,|SortId|:4},{|Name|:|P9联通|,|PhoneModel|:|EVA - DL00C17|,|Operator|:|dualcu|,|IsValid|:|1|,|SortId|:5},{|Name|:|P9移动|,|PhoneModel|:|EVA - TL00C01|,|Operator|:|cmcc|,|IsValid|:|1|,|SortId|:6},{|Name|:|P9全网通AL00|,|PhoneModel|:|EVA - AL00C00|,|Operator|:|all|,|IsValid|:|1|,|SortId|:7},{|Name|:|P9全网通AL10|,|PhoneModel|:|EVA - AL10C00|,|Operator|:|all|,|IsValid|:|1|,|SortId|:8}]},{|VersionName|:|Mate8|,|PhoneModel|:|HUAWEI NXT|,|Operator|:null,|VersionDetail|:[{|Name|:|B523|,|IsValid|:|1|,|VersionType|:|1|,|oldversion|:|B523|,|version|:|B523|,|description|:|测B523|,|createTime|:|2016 - 010 - 29T10: 00:00|,|BG|:|1255|,|sg|:|104|,|v|:|65483|,|f|:|1|,|SortId|:0}],|IsValid|:|1|,|SortId|:950,|MobileModel|:[{|Name|:|m8移动|,|PhoneModel|:|NXT - TL00C01|,|Operator|:|cmcc|,|IsValid|:|1|,|SortId|:3},{|Name|:|M8全网|,|PhoneModel|:|NXT - AL10C00|,|Operator|:|all|,|IsValid|:|1|,|SortId|:4},{|Name|:|M8电信|,|PhoneModel|:|NXT - CL00C92|,|Operator|:|telecom|,|IsValid|:|1|,|SortId|:2},{|Name|:|M8联通|,|PhoneModel|:|NXT - DL00C17|,|Operator|:|dualcu|,|IsValid|:|1|,|SortId|:1}]}]".Replace('|', '"');
                //VersionStr = IOHelper.Read("NewV.txt");
                //VersionStr = PublicClass.GetYouDaoShare();














                LHMI = JsonConvert.DeserializeObject<List<HwModelInfo>>(VersionStr);

                //排序
                LHMI = LHMI.OrderBy(x => x.SortId).ToList();
                LHMI.ForEach(x => x.VersionDetail.OrderBy(m => m.SortId));

                //MyVersion = Mate8List.Mate8Model.Where(m => m.ModelName == Version).SingleOrDefault();
            }
            catch (Exception ee)
            {
                MessageBox.Show("版本库异常,请告诉小烈哥?~~~" + ee.Message, "错误", MessageBoxButtons.OK);
                System.Environment.Exit(0);
                return;
            }

        }
        private void AddTXT(string str)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                string strtxt = textBox1.Text;
                bool jindu = false;

                if (strtxt.Length > 0 && strtxt.Substring(strtxt.Length - 1, 1) == "%" && str.IndexOf("%") > -1 && str.IndexOf("%") < 4)
                {
                    jindu = true;
                }

               // textBox1.AppendText((!string.IsNullOrEmpty(str) ? (jindu ? " " : "\r\n") + DateTime.Now.ToString("HH:mm:ss.fff") : "") + " " + str);



                string strput = "";
                if (!string.IsNullOrEmpty(str))
                {
                    strput = (jindu ? " " : ("\r\n" + DateTime.Now.ToString("HH:mm:ss.fff"))) + " " + str;
                    textBox1.AppendText(strput);
                }
         



            });
        }


        private void Go()
        {
            int isconn = 0;
            //定义会话，每一个请求都将封装成一个会话
            List<Fiddler.Session> oAllSessions = new List<Fiddler.Session>();

            Fiddler.FiddlerApplication.BeforeRequest += delegate (Fiddler.Session oS)
            {






                oS.bBufferResponse = true;
                Monitor.Enter(oAllSessions);
                if (oS.fullUrl.IndexOf("hicloud.com") > -1)
                {
                    if (oS.fullUrl.IndexOf("servicesupport/updateserver/getConfig") > -1)
                    {
                        hwconfig = oS.GetRequestBodyAsString();
                        //AddTXT(hwconfig);
                    }
                    if (oS.fullUrl.IndexOf("http://update.hicloud.com") > -1 && oS.fullUrl.IndexOf("TDS/data/files") > -1 && oS.fullUrl.IndexOf(".zip") > -1)
                    {
                        oS.bBufferResponse = false;
                        AddTXT("正在下载...");
                        //Fiddler.FiddlerApplication.Shutdown();
                        //return;
                    }

                    if ((oS.fullUrl.IndexOf("hicloud.com") > -1 && oS.fullUrl.IndexOf("Check.action") > -1) || (oS.fullUrl.IndexOf("http://update.hicloud.com:8180/TDS/data/files") > -1 && oS.fullUrl.IndexOf(".zip") == -1))
                    {
                        IOHelper.CreateFile("Log.txt");
                        //if (isconn == 1)
                        //{
                        //    oS.utilSetRequestBody(oS.GetRequestBodyAsString().Replace("B321", "B168"));
                        //}
                        IOHelper.WriteLine("Log.txt", "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "][请求]     " /*+ oS.fullUrl*/ + "\n\r" + oS.GetRequestBodyAsString());

                    }




                }

                oAllSessions.Add(oS);
                Monitor.Exit(oAllSessions);
                oS["X-AutoAuth"] = "(default)";
            };
            Fiddler.FiddlerApplication.BeforeResponse += delegate (Fiddler.Session oS)
            {
                if (isconn == 0)
                {
                    isconn++;
                    AddTXT("成功链接代理");
                }
                string resstr = "";
                oS.utilDecodeResponse();
                if ((oS.fullUrl.IndexOf("hicloud.com") > -1 && oS.fullUrl.IndexOf("Check.action") > -1) || (oS.fullUrl.IndexOf("http://update.hicloud.com:8180/TDS/data/files") > -1 && oS.fullUrl.IndexOf(".zip") == -1))
                {
                    //if (iscm < 1 && string.IsNullOrEmpty(hwconfig))
                    //{
                    //    AddTXT("串码获取失败,请重新获取更新(多点获取版本" + (3 - iscm).ToString() + "次,别问)~");
                    //    iscm++;
                    //    return;
                    //}
                    //else
                    //{
                    //    iscm = 3;
                    //}
                    resstr = oS.GetResponseBodyAsString();


                    IOHelper.CreateFile("Log.txt");
                    IOHelper.WriteLine("Log.txt", "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "][返回]     "/* + oS.fullUrl */+ "\n\r" + resstr);

                }
                if (oS.fullUrl.IndexOf("http://query.hicloud.com:80/sp_ard_common/v2/Check.action") > -1
                || oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/v2/Check.action") > -1
                || oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/UrlCommand/CheckNewVersion.aspx") > -1)
                {
                    string ipc = oS.clientIP.Replace("::ffff:", "");
                    if (!IsLAN(ipc) && roles == 2)
                    {
                        AddTXT("您无外网远程推送权限~");
                        return;
                        //myqqinfo.role
                    }

                    //http://update.hicloud.com:8180/TDS/data/files/p3/s15/G962/g77/v48270/f2/full/update.zip
                    AddTXT("0%");
                    if (oS.fullUrl.IndexOf("http://query.hicloud.com:80/sp_ard_common/v2/Check.action") > -1)
                    {
                        ctype = 1;
                        if (isconn == 0)
                            AddTXT("正在使用WIFI代理获取升级版本");
                    }
                    else
                        if (oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/UrlCommand/CheckNewVersion.aspx") > -1)
                    {
                        ctype = 2;
                        if (isconn == 0)
                            AddTXT("正在使用华为手机助手获取升级版本(新)");
                    }
                    else
                        if (oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/v2/Check.action") > -1)
                    {
                        ctype = 3;
                        if (isconn == 0)
                            AddTXT("正在使用华为手机助手获取升级版本");
                    }
                    else { AddTXT("路径异常"); }
                    if (isconn == 1)
                    {
                        isconn++;
                    }
                   
                    try
                    {





                        string reqstr = oS.GetRequestBodyAsString();




                        Mate8RequestBody rules = new Mate8RequestBody();
                        if (ctype == 2)
                        {
                            IOHelper.CreateFile("Log.txt");
                            IOHelper.WriteLine("Log.txt", "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "][请求XML]     " /*+ oS.fullUrl*/ + "\n\r" + reqstr);
                            rules = PublicClass.GetRulesByXml(reqstr);
                        }
                        else //if (1 == 1)
                        {
                            rules = JsonConvert.DeserializeObject<Mate8RequestBody>(reqstr);
                        }
                        AddTXT("5%");
                        if (rules.rules == null)
                        {
                            AddTXT("无法获取您的版本，请关闭软件并重新打开"); return;
                        }
                        var DeviceName = rules.rules.DeviceName;
                        var C_version = rules.rules.C_version;

                        var FirmWare = rules.rules.FirmWare;

                        if (rules.rules.OS == "Android 7.0")
                        { AddTXT("本软件不支持" + rules.rules.OS + "系统使用,请下载最新包使用三键刷机!"); return; }


                        if (Vtype == "1" && (rules.rules.PackageType != "patch"||rules.rules.PackageType != "increment"))
                        { return; }
                        //if (Vtype == "2" && rules.rules.PackageType != "patch")
                        //{ return; }
                        if (ctype == 3)
                        {
                            if (Vtype == "3" && rules.rules.PackageType != "full_back")
                            {
                                return;
                            }
                            
                        }
                        else
                        {
                            if (Vtype == "3" && rules.rules.PackageType != "full")
                            {
                                return;
                            }
                            if (Vtype == "3")
                            {
                                AddTXT("不建议使用WIFI代理降级");
                            }
                          
                        }
                        if (Vtype == "4" && rules.rules.PackageType != "full")
                        { AddTXT("完整包请在手机上使用[系统更新-高级-下载最新完整包]"); return; }









                        var FirmWareEMUI = "";
                        int isjk = 0;
                        try
                        {
                            AddTXT("6%");
                            FirmWareEMUI = rules.rules.FirmWare.Split('_')[0];
                            if (FirmWareEMUI.IndexOf("EMUI") > -1)
                            {
                                DeviceName = FirmWareEMUI + "_" + DeviceName;
                                isjk++;
                            }
                        }
                        catch { AddTXT("7%"); }
                        if (isconn == 2)
                        {
                            AddTXT("您的手机型号为:" + DeviceName);
                        }


                        isconn++;
                        AddTXT("10%");
                        AddTXT("正在自动适配版本库");
                        // HMISelect = LHMISelect.Where(m => m.PhoneModel.Replace(" ", "") == DeviceName.Replace(" ", "")).FirstOrDefault();//自动选择版本库
                        var HMISelect = LHMISelect.Where(m => DeviceName.Replace(" ", "").StartsWith(m.PhoneModel.Replace(" ", ""))).FirstOrDefault();
                        if (HMISelect == null)
                        {

                            AddTXT("15%");
                            var HMIAll = LHMI.Where(m => DeviceName.Replace(" ", "").StartsWith(m.PhoneModel.Replace(" ", ""))).FirstOrDefault();

                            if (HMIAll == null)
                            {
                                AddTXT("16%");
                                AddTXT("找不到对应的型号,请联系管理员");
                                AddTXT("100%");
                                return;
                            }
                            else
                            {
                                AddTXT("17%");



                                if (checkBox3.Checked)
                                {
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {
                                        listBox2.SelectedItem = HMIAll.VersionName;
                                    });
                                    Thread.Sleep(100);
                                    var NewLHMISelect = LHMISelect.Where(m => DeviceName.Replace(" ", "").StartsWith(m.PhoneModel.Replace(" ", ""))).FirstOrDefault();//自动选择版本库

                                    AddTXT("自动切换正确的版本库");
                                    HMISelect = NewLHMISelect;
                                    AddTXT("切换成功");
                                }
                                else if (!checkBox3.Checked)
                                {
                                    AddTXT("请勾选自动切换版本");
                                }

                            }
                        }
                        //Mate8Model Mate8Model = Mate8List.Mate8Model.Where(m => m.ModelName == DeviceName).FirstOrDefault();
                        //MyVersion = Mate8Model;





                        AddTXT("18%");

                        string mobilemodel = "";
                        if (HMISelect.MobileModel != null)
                        {
                            AddTXT("19%");
                            MobileModel mm = HMISelect.MobileModel.Where(x => (DeviceName.Replace(" ", "") + ((isjk > 0) ? "" : C_version.Replace(" ", ""))).EndsWith(x.PhoneModel)).FirstOrDefault();
                            if (mm != null)
                            {
                                AddTXT("20%");
                                mobilemodel = mm.PhoneModel;
                            }

                        }






                        AddTXT("21%");
                        var VD = HMISelect.VersionDetail;
                        AddTXT("22%");
                        VersionDetail VDSelect = null;
                        if (Vtype == "3" || Vtype == "4")
                        {
                            AddTXT("25%");
                            //if (FullIsGo == 0)
                            //{
                            //FullIsGo++;
                            VDSelect = VD.Where(m => m.Name.Replace(" ", "") == SelectedVersionName.Replace(" ", "")).FirstOrDefault();
                            //}
                            //else
                            //{

                            //AddTXT("74%");

                            //}
                            AddTXT("26%");
                        }
                        else
                        {
                            AddTXT("27%");

                            List<string> li = new List<string>();
                            //手动选择
                            if (checkBox1.Checked)
                            {
                                AddTXT("28%");
                                li = listBox1.SelectedItems.Cast<string>().ToList();
                            }
                            if (li.Count > 0)
                            {
                                AddTXT("29%");
                                VDSelect = VD.Where(m => li.Contains(m.Name.Replace(" ", "")) && (mobilemodel + ((isjk > 0) ? "_" : "") + m.oldversion.Replace(" ", "")).Replace("__", "_") == rules.rules.FirmWare.Replace(" ", "")).FirstOrDefault();
                                AddTXT("30%");
                            }
                            else
                            {
                                AddTXT("31%");
                                VDSelect = VD.Where(m => (mobilemodel + ((isjk > 0) ? "_" : "") + m.oldversion.Replace(" ", "")).Replace("__", "_") == rules.rules.FirmWare.Replace(" ", "")).FirstOrDefault();
                                AddTXT("32%");
                            }
                            AddTXT("33%");
                        }
                        AddTXT("40%");
                        if (VDSelect != null)
                        {
                            //try
                            //{
                            //    if (int.Parse(listBox1.SelectedItem.ToString().Remove(0, 1)) <= int.Parse(mus.version.Remove(0, 1)))
                            //    {

                            //        AddTXT("100%");
                            //        AddTXT("您的版本为：" + rules.rules.FirmWare + ",请重新选择版本,本软件暂时不提供降级服务!");
                            //        return;
                            //    }
                            //}
                            //catch (Exception ee)
                            //{
                            //    AddTXT("100%");
                            //    AddTXT("出错了,原因如下:" + ee.Message);
                            //    return;
                            //}
                            AddTXT("50%");
                            AddTXT("您手机的版本为：" + rules.rules.FirmWare);
                            AddTXT("您找到的版本为：" + mobilemodel + VDSelect.version);
                            AddTXT("51%");

                            List<VersionInfo> lvif = new List<VersionInfo>();
                            AddTXT("52%");
                            AddTXT("开始加载升级数据");
                            VersionInfo vif = new VersionInfo
                            {
                                name = "[官方]" + mobilemodel + VDSelect.version,
                                version = mobilemodel + VDSelect.version,
                                versionID = VDSelect.v,
                                description = VDSelect.description,
                                createTime = VDSelect.createTime,
                                url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + VDSelect.BG + "/g" + VDSelect.sg + "/v" + VDSelect.v + "/f" + VDSelect.f + "/"
                            };
                            lvif.Add(vif);
                            AddTXT("53%");
                            Mate8Version m8v = new Mate8Version
                            {
                                status = "0",
                                components = lvif
                            };


                            if (isycts == 1)
                            {
                                AddTXT("您的权限较低,现在开始判断您是否可以推送本包");
                                if (svrtime < Convert.ToDateTime(VDSelect.createTime).AddDays(3))
                                {
                                    AddTXT("您的权限离本包可推送时间还差" + (Convert.ToDateTime(VDSelect.createTime).AddDays(3) - svrtime).TotalHours + "小时");
                                    AddTXT("100%");
                                    return;
                                }
                            }
                            

                            bool ishmd = false;
                            try
                            {
                                AddTXT("55%");
                                long IMEI = 0;
                                HwConfig hwc = JsonConvert.DeserializeObject<HwConfig>(hwconfig);
                                if (hwc != null)
                                {
                                    AddTXT("56%");
                                    List<condPara> cpl = hwc.condParaList;
                                    if (cpl != null)
                                    {
                                        AddTXT("56%");
                                        condPara cp = cpl.Where(x => x.key == "IMEI").SingleOrDefault();
                                        if (cp != null)
                                        {
                                            AddTXT("57%");
                                            IMEI = long.Parse(cp.value);
                                            AddTXT("58%");
                                        }
                                    }
                                }
                                AddTXT("60%");
                                long qqnumb = 0;
                                long.TryParse(qqnumber, out qqnumb);
                                AddTXT("61%");
                                ishmd = hmdlist.Where(x => x == IMEI || x == qqnumb).Count() > 0;
                                AddTXT("62%");
                            }
                            catch { AddTXT("66%"); }













                            isan7 = VDSelect.description.IndexOf("测") > -1;
                            AddTXT("67%");



                            AddTXT("升级数据加载成功");
                            AddTXT("68%");



                            try
                            {
                                AddTXT("70%");
                                if (issms == 1 && (isan7 || ishmd))
                                {
                                    AddTXT("71%");

                                    MailMessage mailObj = new MailMessage();
                                    mailObj.From = new MailAddress("w_lwy@163.com", "小烈哥"); //发送人邮箱地址
                                    mailObj.To.Add("w_lwy@163.com");   //收件人邮箱地址   
                                    //mailObj.To.Add(label3.Text.Trim() + "@qq.com");   //收件人邮箱地址
                                    mailObj.Subject = (ishmd ? "黑名单 " : "") + label3.Text.Trim() + "获取" + MyVersion.ModelName + "版本成功";  //主题
                                    mailObj.Body = reqstr + "\n\r" + (string.IsNullOrEmpty(hwconfig) ? "串码获取失败" : hwconfig) + "\n\r" + qqstring + "\n\r" + VDSelect.description;    //正文

                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = "smtp.163.com";         //smtp服务器名称
                                    smtp.UseDefaultCredentials = true;
                                    smtp.Credentials = new NetworkCredential("w_lwy@163.com", "hwupdate163");  //发送人的登录名和密码

                                    //Thread t = new Thread(() =>
                                    //{
                                    try
                                    {
                                        AddTXT("72%");
                                        smtp.Send(mailObj);
                                    }
                                    catch
                                    {
                                        AddTXT("73%");
                                        AddTXT("邮件送失败,请不要阻止邮件发送.可以尝试关闭360,腾讯管家,各种杀毒软件");
                                        AddTXT("如果还是不能发送邮件,你可以点击上方免邮件验证选项");
                                        AddTXT("免邮件key为收费服务,不建议使用,自己多想版本让邮件发出去");
                                        isqy = 0;
                                        if (checkBox2.Checked)
                                        {
                                            AddTXT("74%");
                                            if (textBox2.Text.ToUpper() == PublicClass.GetMd5(qqnumber + UnixTimestamp.ConvertIntDateTime(qqinfo.svr_time).ToString("yyyyMMdd") + Application.ProductVersion.ToString()).ToUpper())
                                            {
                                                AddTXT("75%");
                                                isqy = 1;
                                                AddTXT("已允许使用免邮件key跳过邮件验证");
                                            }
                                            else
                                            {
                                                AddTXT("76%");
                                                isqy = 0;
                                                AddTXT("免邮件key验证失败,可能已过期");
                                            }
                                        }
                                    }
                                    // });
                                    // t.Start();
                                    issms--;
                                }
                                AddTXT("77%");
                                if (isqy == 1)//是否群员
                                {


                                    AddTXT("80%");

                                    string json = JsonConvert.SerializeObject(m8v);

                                    if (ctype == 2)
                                    {
                                        //IOHelper.CreateFile("Log.txt");
                                        //IOHelper.WriteLine("Log.txt", "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "][返回JSON]     " /*+ oS.fullUrl*/ + "\n\r" + json);

                                        AddTXT("81%");
                                        string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                                        xml += "<root><status>0</status><components><component><name>" + vif.name
                                        + "</name><version>" + vif.version
                                        + "</version><versionID>" + vif.versionID
                                        + "</versionID><description>" + vif.description
                                        + "</description><createtime>" + vif.createTime
                                        + "</createtime><url>" + vif.url
                                        + "</url></component></components></root>";

                                        json = xml;
                                        //json = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><root>" + doc1.OuterXml + "</root>";
                                        AddTXT("82%");


                                        //IOHelper.WriteLine("Log.txt", "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "][返回XML]     " /*+ oS.fullUrl*/ + "\n\r" + json);

                                    }
                                    if (!ishmd)
                                    {
                                        AddTXT("83%");
                                        oS.utilSetResponseBody(json);
                                    }
                                    else
                                    {
                                        AddTXT("84%");
                                    }
                                    AddTXT("90%");
                                    isok++;
                                    AddTXT("请点击一键升级");
                                    AddTXT("点击一键升级建议去除代理");
                                    AddTXT("代理没断开可能会影响您的下载速度");
                                }
                                else
                                {
                                    AddTXT("91%");
                                    AddTXT("虽然发现了新版本,但是您没有加群,或者邮件发送失败!");
                                }
                                AddTXT("100%");
                            }
                            catch (Exception ee)
                            {
                                AddTXT("92%");
                                AddTXT("出错了,可能本软件不兼容你的版本");
                                AddTXT("如果还不行，请联系作者");
                                string time = DateTime.Now.ToString("yyyyMMdd");
                                IOHelper.CreateFile("Exception" + time);
                                IOHelper.WriteLine("Exception" + time, DateTime.Now.ToString() + "    " + ee.ToString());

                                AddTXT("100%");
                            }
                            //AddTXT("100%");
                        }
                        else
                        {


                            AddTXT("100%");
                            AddTXT("您手机的版本为：" + rules.rules.FirmWare);

                            AddTXT("没找到更新的版本，谢谢使用");
                            AddTXT("如果官方已经推送更新的版本，请联系作者");
                        }

                    }
                    catch (Exception ee)
                    {
                        AddTXT("94%");
                        AddTXT("出错了,可能本软件不兼容你的版本，你可以尝试点击又上角的升级版本库");
                        AddTXT("如果还不行，请联系作者");
                        string time = DateTime.Now.ToString("yyyyMMdd");
                        IOHelper.CreateFile("Exception" + time);
                        IOHelper.WriteLine("Exception" + time, DateTime.Now.ToString() + "    " + ee.ToString());
                    }

                }

            };
        }



        private bool IsLAN(string ip)
        {
            try
            {
                string[] strArray = ip.Split(new char[] { '.' });
                int[] intArray = strArray.Select(m => int.Parse(m)).ToArray();
                if (intArray.Length > 1)
                {
                    if (intArray[0] == 10 || intArray[0] == 172 || intArray[0] == 192)
                    {
                        if (intArray[0] == 172 && (intArray[1] < 16 || intArray[1] > 31))
                        {
                            return false;
                        }
                        if (intArray[0] == 192 && intArray[1] != 168)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (intArray[0] == 127)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private string GetQQKeyFile()
        {
            return IOHelper.Read("Key.txt");
        }
        private Mate8List GetVersions()
        {
            Mate8List Mate8List = new Mate8List();
            string VersionStr = IOHelper.Read("Mate8Version");
            try
            {
                Mate8List = JsonConvert.DeserializeObject<Mate8List>(VersionStr);
            }
            catch
            {
                VersionStr = PublicClass.UpdateVersions();
                Mate8List = JsonConvert.DeserializeObject<Mate8List>(VersionStr);
                IOHelper.Write("Mate8Version", VersionStr);
            }
            return Mate8List;
        }




        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //AddTXT("您选择升级的版本为:" + listBox1.SelectedItem.ToString() + ",现在可以开始设置代理并获取新版");
            FullIsGo = 0;
            SelectedVersionName = listBox1.SelectedItem.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            issms = 1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            BindList();
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            var selelctLHMI = LHMI.Where(m => m.VersionName == listBox2.SelectedItem.ToString()).FirstOrDefault();
            //selelctLHMI.PhoneModel
            AddTXT("您选择升级的版本为:" + selelctLHMI.PhoneModel + "");
            BindList();
        }
        private void BindList()
        {
            checkBox1.Checked = false;
            listBox1.Items.Clear();
            Vtype = radioButton1.Checked ? "1" : radioButton2.Checked ? "2" : radioButton3.Checked ? "3" : radioButton4.Checked ? "4" : "1";
            LHMISelect = Clone(LHMI.Where(m => m.VersionName == listBox2.SelectedItem.ToString()).ToList());//过滤失效的   

            LHMISelect.ForEach(m =>
            {
                // m.VersionDetail = m.VersionDetail.Where(x => x.IsValid == "1").ToList();//过滤失效的

                if (m.VersionDetail.Count > 0)
                {

                    m.VersionDetail = m.VersionDetail.Where(x => x.VersionType == Vtype).ToList();//过滤失效的
                    m.VersionDetail.ForEach(x =>
                    {

                        if (!listBox1.Items.Contains(x.Name))
                        {
                            listBox1.Items.Insert(0, x.Name);
                        }
                    });
                }
            });
            if (Vtype == "3" || Vtype == "4")
            {
                listBox1.SelectionMode = SelectionMode.One;
                if (!listBox1.Enabled)
                {
                    AddTXT("您准备推送完整包,请在可升级版本中选择完整包版本");
                }
                listBox1.Enabled = true;
                if (listBox1.Enabled && listBox1.Items.Count > 0)
                {
                    listBox1.SetSelected(0, true);
                }

            }
            else
            {
                listBox1.SelectionMode = SelectionMode.MultiSimple;
                listBox1.Enabled = false;
            }
        }
        public T Clone<T>(T RealObject)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(RealObject));
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            string ips = PublicClass.GetLocalIp();
            MessageBox.Show(ips, "您的IP地址,都试试");
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            VersionStrJM = PublicClass.GetYouDaoShare();
            LoadVersion();
        }
        private void LoadHMD()
        {
            try
            {
                string hmd = PublicClass.GetHMD();
                hmdlist = hmd.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToList();
            }
            catch { }
        }
        private void LoadVersion()
        {
            //DialogResult dlrtb = MessageBox.Show("本软件有小烈哥淘宝店的广告,是否显示?\r\n谢谢大家支持~\r\n大家如果有需要也可以买一罐!", "提示", MessageBoxButtons.YesNo);
            //if (dlrtb == DialogResult.Yes)
            //{
            //    ShowTaobao();
            //}

            GetVersion();
            LHMI = LHMI.Where(m => m.IsValid == "1").ToList();//过滤失效的
            LHMI.ForEach(m =>
            {
                m.VersionDetail = m.VersionDetail.Where(x => x.IsValid == "1").ToList();//过滤失效的
                //m.VersionDetail = m.VersionDetail.Where(x => x.VersionType == "Vtype").ToList();//过滤失效的

                if (m.VersionDetail.Count > 0)
                {
                    if (!listBox2.Items.Contains(m.VersionName))
                    {
                        listBox2.Items.Insert(0, m.VersionName);
                    }
                }
            });
            listBox2.SetSelected(0, true);


            this.BeginInvoke((MethodInvoker)delegate
            {
                isnc = 0;
                if (qqinfo.QunNumber == 586095831 || qqinfo.QunNumber == 481976130||qqinfo.QunNumber == 518363466)
                {

                    //DialogResult dlr = MessageBox.Show("您有使用完整包推送的资格,是否使用", "提示", MessageBoxButtons.YesNo);
                    if (qqinfo.QunNumber == 518363466)
                    {
                        isycts = 1;
                    }

                    //if (dlr == DialogResult.Yes)
                    {
                        isnc = 1;
                        //Mate8List.Mate8Model = Mate8List.Mate8Model.Where(x => x.ModelNumber == "1124").ToList(); 
                        radioButton2.Visible = true;
                        radioButton4.Visible = true;
                        radioButton2.Checked = true;
                    }
                   // else
                    //{
                    //    isnc = 0;
                        //Mate8List.Mate8Model = Mate8List.Mate8Model.Where(x => x.ModelNumber != "1124").ToList();
                   // }
                }
                else
                {
                    radioButton2.Visible = false;
                    radioButton4.Visible = false;
                    radioButton1.Checked = true;
                    isnc = 0;
                    //Mate8List.Mate8Model = Mate8List.Mate8Model.Where(x => x.ModelNumber != "1124").ToList();
                }



                BindList();

            });





        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
             {
                 PublicClass.OpenIE("http://shang.qq.com/wpa/qunwpa?idkey=21a28b0fc58939d55e205e55e55fd74966b0b93bfe5e2924bbc8126d660fb199");
             });
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
          {
              PublicClass.OpenIE("http://note.youdao.com/share/?id=07bf9af8e5e82f536e48a490d3618415&type=note");
          });
        }
        private void ShowTaobao()
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                PublicClass.OpenIE("https://xltfm.taobao.com");
            });
        }

        private void toolStripStatusLabel5_Click(object sender, EventArgs e)
        {
            ShowTaobao();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MessageBox.Show("本功能不会用的别乱点!!!\n\r下面必须要选一个或多个版本", "警告");
            }
            if (radioButton1.Checked || radioButton2.Checked)
            {
                listBox1.Enabled = checkBox1.Checked;
            }
            else
            {
                AddTXT("您准备推送完整包,必须手动选择你需要的版本");
            }
        }

        private void HwUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {


            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            IOHelper.CreateFile("ClosingLog" + time + ".txt");
            IOHelper.WriteLine("ClosingLog" + time + ".txt", textBox1.Text);



            if (isok > 0)
            {
                PublicClass.OpenIE("https://xltfm.taobao.com");
                //ShowTaobao();
            }

            //System.Environment.Exit(0);
        }

        private void HwUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.Visible = true;
                MessageBox.Show("请在旁边的文本框中输入免邮件key\n\rkey有效期一自然天", "提示");
            }
            else { textBox2.Visible = false; }
        }
    }
}