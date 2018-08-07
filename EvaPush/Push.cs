using Fiddler;
using HwPush.HwBase;
using HwPush.HwModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EvaPush
{
    public partial class Push : Form
    {
        public Push()
        {
            InitializeComponent();
        }
        public Push(string[] args)
        {
            PublicClass.GlobalQQNumber = Int64.Parse(args[0]);
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "华为安卓7升级 v" + Application.ProductVersion.ToString();
#if !DEBUG
            if (int.Parse(DateTime.Now.ToString("yyyMMdd")) != 20161108 && int.Parse(DateTime.Now.ToString("yyyMMdd")) != 20161109 && int.Parse(DateTime.Now.ToString("yyyMMdd")) != 20161110)
            {
                MessageBox.Show("软件过期");
                System.Environment.Exit(0);
                return;
            }
#endif
            string qqnumber = PublicClass.GlobalQQNumber.ToString();

            if (qqnumber == "119564557" || qqnumber == "891587944" || qqnumber == "289408880" || qqnumber == "834714126" || qqnumber == "591219179" || qqnumber == "75951556"|| qqnumber == "94475747")
            {

            }
            else

            {
                MessageBox.Show("没权限使用");
                System.Environment.Exit(0);
            }






            //FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;
            //int iPort = 8877;
            //Fiddler.FiddlerApplication.Startup(iPort, oFCSF);
            //oSecureEndpoint = FiddlerApplication.CreateProxyEndpoint(iSecureEndpointPort, true, sSecureEndpointHostname);



            AddTXT("小烈哥华为升级");
            AddTXT("本软件只支持升级安卓7");
            AddTXT("请设置完下面的参数后手动点击启动代理");
            AddTXT("每次修改完参数都要重新点击启动代理");
            //AddTXT("检测证书状态");
            //if (Fiddler.CertMaker.rootCertExists() == true)
            //{
            //    AddTXT("安全证书状态正常");
            //    AddTXT("如果需要删除证书,请使用‘卸载安全证书’功能");
            //}
            //else
            //{
            //    AddTXT("安全证书状态异常,推送可能会失败,如果失败请点击‘安装安全证书’"); 
            //}








        }



        int resqustnum = 0; string okimei = "";
        private void Go(int iPort = 8877)
        { //设置别名
            Fiddler.FiddlerApplication.SetAppDisplayName("FiddlerCoreHw");

            Fiddler.CONFIG.IgnoreServerCertErrors = false;
            FiddlerApplication.Prefs.SetBoolPref("fiddler.network.streaming.abortifclientaborts", true);


            //启动方式
            FiddlerCoreStartupFlags oFCSF = FiddlerCoreStartupFlags.Default;

            //定义http代理端口
      
            //启动代理程序，开始监听http请求
            //端口,是否使用windows系统代理（如果为true，系统所有的http访问都会使用该代理）
            Fiddler.FiddlerApplication.Startup(iPort, false, true, true);



            // 我们还将创建一个HTTPS监听器，当FiddlerCore被伪装成HTTPS服务器有用
            // 而不是作为一个正常的CERN样式代理服务器。
            Proxy oSecureEndpoint = null;
            int iSecureEndpointPort = 8888;
            string sSecureEndpointHostname = "127.0.0.1";
            oSecureEndpoint = FiddlerApplication.CreateProxyEndpoint(iSecureEndpointPort, true, sSecureEndpointHostname);

            AddTXT("开始监控 下面需要出现 成功链接代理 才能正常获取版本");

            int isconn = 0; int iscomit = 0;
            //定义会话，每一个请求都将封装成一个会话
            List<Fiddler.Session> oAllSessions = new List<Fiddler.Session>();
            string firmware = ""; string hwconfig = ""; string thisIMEI = "";
            okimei = textBox2.Text;
            Fiddler.FiddlerApplication.BeforeRequest += delegate (Fiddler.Session oS)
            {
                try
                {
                    oS.bBufferResponse = true;
                    Monitor.Enter(oAllSessions);



            



                        if (oS.fullUrl.IndexOf("hicloud.com") > -1)
                    {
                        AddTXT(oS.fullUrl);
                        //AddTXT("10%");
                        string v = oS.GetRequestBodyAsString();
                        if (oS.fullUrl.IndexOf("authorize.action") > -1)
                        {
                            AddTXT(v);
                        }
                        //AddTXT(v);
                        if (oS.fullUrl.IndexOf("http://query.hicloud.com:80/sp_ard_common/v2/Check.action") > -1
                            || oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/v2/Check.action") > -1)
                        {
                            //AddTXT("44%");

                         

                         
                            
                            //oS.utilSetRequestBody(v);
                            resqustnum++;
                        }


                        if (oS.fullUrl.IndexOf(".zip") > -1)
                        {
                            oS.bBufferResponse = false;
                        }


                        if (oS.fullUrl.IndexOf("https://updatebeta.hicloud.com/TDS/data/") > -1)
                        {
                             
                            //AddTXT("11%");
                            //v = "{\"rules\":{\"IMEI\":\"869158020296105\"}}";

                            //AddTXT("12%");
                            oS.utilSetRequestBody(v);

                            //AddTXT("13%");
                            if (oS.fullUrl.IndexOf(".zip") > -1)
                            {
                                AddTXT("41%");
                                oS.bBufferResponse = false;
                                AddTXT("正在下载升级包...");
                            }
                            if (oS.fullUrl.IndexOf("changelog.xml") > -1)
                            {
                                AddTXT("21%");
                                AddTXT("加载日志");
                            }
                            if (oS.fullUrl.IndexOf("filelist.xml") > -1)
                            {
                                AddTXT("31%");
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
                    AddTXT("92%");
                    AddTXT(ee.ToString());
                }
            };
            Fiddler.FiddlerApplication.BeforeResponse += delegate (Fiddler.Session oS)
            {
                if (isconn == 0)
                {
                    isconn++;
                    AddTXT("成功链接代理");
                }

                oS.utilDecodeResponse();




          


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
                                thisIMEI = cp.value;
                                AddTXT("串码" + thisIMEI);
                            }
                        }
                    }
                }



                 
                 
                if (oS.fullUrl.IndexOf("authorize.action") > -1)
                {
                   // I6IjEifV19

                    var auta=oS.GetResponseBodyAsString();
                    AddTXT(auta);

                    //oS.utilSetResponseBody(auta.Replace("I6IjEifV19", "I6IjAifV19"));

                }
                if (oS.fullUrl.IndexOf("UpdateReport.action") > -1)
                {
                    // I6IjEifV19

                    var auta1 = oS.GetRequestBodyAsString();
                    AddTXT(auta1);
                    var auta = oS.GetResponseBodyAsString();
                    AddTXT(auta);

                    //oS.utilSetResponseBody(auta.Replace("I6IjEifV19", "I6IjAifV19"));

                }






                if (oS.fullUrl.IndexOf("http://query.hicloud.com:80/sp_ard_common/v2/Check.action") > -1
                || oS.fullUrl.IndexOf("http://query.hicloud.com/sp_ard_common/v2/Check.action") > -1)
                {
                    if (!checkBox1.Checked)
                    {
                        var a = thisIMEI.IndexOf(okimei);
                        if (a < 0)
                        {
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            AddTXT("IMEI不对不能使用");
                            return;
                        }
                    }
                    #region 获取版本
                    AddTXT("0%");
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
                        AddTXT("1%");

                        string str = oS.GetResponseBodyAsString();

                        string v = oS.GetRequestBodyAsString();
                    
                        Mate8RequestBody rule = JsonConvert.DeserializeObject<Mate8RequestBody>(v);
                        firmware = rule.rules.DeviceName;

                        if (iscomit == 0)
                        {
                            if (firmware == "NXT-AL10"
                               || firmware == "NXT-CL00"
                               || firmware == "NXT-TL00"
                               || firmware == "NXT-DL00")
                            {
                                if (rule.rules.PackageType == "full_back")
                                {
                                    iscomit++;
                                    var vf = firmware + rule.rules.C_version;
                                    str = ("{|status|:|0|,|components|:[{|name|:|[小烈哥]" + vf + "B386|,|version|:|" + vf + "B386|,|versionID|:|65694|,|description|:|386|,|ruleAttr|:||,|createTime|:|2016-09-18T06:55:50+0000|,|url|:|http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1255/g104/v65694/f1/|}]}").Replace("|", "\"");

                                }
                                if (rule.rules.PackageType == "full")
                                {
                                    iscomit++;
                                    var vf = firmware + rule.rules.C_version;
                                    str = ("{|status|:|0|,|components|:[{|name|:|[小烈哥]" + vf + "B550-log|,|version|:|" + vf + "B550-log|,|versionID|:|66474|,|description|:|550-log|,|ruleAttr|:||,|createTime|:|2016-09-18T06:55:50+0000|,|url|:|http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1255/g104/v66474/f1/|}]}").Replace("|", "\"");

                                }
                            }






                            if (1 == 2)
                            {
                                if (firmware == "HUAWEI NXT-AL10"
                                || firmware == "HUAWEI NXT-CL00")
                                {
                                    str = "{|status|:|0|,|components|:[{|name|:|[小烈哥]NXT-AL10C00B523|,|version|:|NXT-AL10C00B523|,|versionID|:|65483|,|description|:|523|,|ruleAttr|:||,|createTime|:|2016-09-18T06:55:50+0000|,|url|:|http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1255/g104/v65483/f1/|,|reserveUrl|:|update8.hicloud.com|,|versionType|:|1|}]}".Replace("|", "\"");
                                    if (firmware == "HUAWEI NXT-CL00")
                                    {
                                        str = str.Replace("NXT-AL10C00", "NXT-CL00C00");
                                    }
                                }
                                if (firmware == "HUAWEI NXT-TL00"
                               || firmware == "HUAWEI NXT-DL00")
                                {
                                    str = "{|status|:|0|,|components|:[{|name|:|[小烈哥]NXT-TL00C01B523|,|version|:|NXT-TL00C01B523|,|versionID|:|65490|,|description|:|523|,|ruleAttr|:||,|createTime|:|2016-09-18T06:55:50+0000|,|url|:|http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1255/g104/v65490/f1/|,|reserveUrl|:|update8.hicloud.com|,|versionType|:|1|}]}".Replace("|", "\"");
                                    if (firmware == "HUAWEI NXT-DL00")
                                    {
                                        str = str.Replace("NXT-TL00C01", "NXT-DL00C17");
                                    }
                                }


                                if (firmware == "NXT-TL00"
                               || firmware == "NXT-DL00")
                                {
                                    str = "{|status|:|0|,|components|:[{|name|:|[小烈哥]NXT-TL00C01B531-log|,|version|:|NXT-TL00C01B531-log|,|versionID|:|65880|,|description|:|531|,|ruleAttr|:||,|createTime|:|2016-09-18T06:55:50+0000|,|url|:|http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1255/g104/v65880/f1/|,|reserveUrl|:|update8.hicloud.com|,|versionType|:|1|}]}".Replace("|", "\"");
                                    if (firmware == "NXT-DL00")
                                    {
                                        str = str.Replace("NXT-TL00C01", "NXT-DL00C17");
                                    }
                                }


                                if (firmware == "EVA-AL00"
                                 || firmware == "EVA-TL00"
                                 || firmware == "EVA-UL00"
                                 || firmware == "EVA-CL00")
                                {
                                    str = "{|status|:|0|,|components|:[{|name|:|[小烈哥]EVA-TL00C01B323|,|version|:|EVA-TL00C01B323|,|versionID|:|65543|,|description|:|323|,|ruleAttr|:||,|createTime|:|2016-09-18T06:55:50+0000|,|url|:|http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1256/g104/v65543/f1/|,|reserveUrl|:|update8.hicloud.com|,|versionType|:|1|}]}".Replace("|", "\"");
                                    if (firmware == "EVA-AL00")
                                    {
                                        str = str.Replace("EVA-TL00C01", "EVA-AL00C00");
                                    }

                                }
                                if (firmware == "EVA-AL10")
                                {
                                    str = "{|status|:|0|,|components|:[{|name|:|[小烈哥]EVA-AL10C00B323|,|version|:|EVA-AL10C00B323|,|versionID|:|65542|,|description|:|323|,|ruleAttr|:||,|createTime|:|2016-09-18T06:55:50+0000|,|url|:|http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1256/g104/v65542/f1/|,|reserveUrl|:|update8.hicloud.com|,|versionType|:|1|}]}".Replace("|", "\"");
                                    //if (firmware == "EVA-AL00")
                                    //{
                                    //    str = str.Replace("EVA-TL00C01", "EVA-AL00C00");
                                    //}

                                }
                            }
                        }
                        //if (iscomit >0)
                        //{
                        //    str = "{\"status\":\"1\"}";
                        //}
                        oS.utilSetResponseBody(str);




                        //if (v2.IndexOf("EVA-AL00C00B193") > -1) {
                        //    v2 = v2.Replace("EVA-AL00C00B193", "EVA-AL10C00B193");
                        //    oS.utilSetResponseBody(v2);
                        //}

                        //AddTXT(str);


                        AddTXT("开始加载升级数据");
                        //string v1 = "{\"status\":\"0\",\"components\":[{\"name\":\"EVA-AL00C00B193\",\"version\":\"EVA-AL00C00B193\",\"versionID\":\"64011\",\"description\":\"【B192-B193差分包】EVA-AL00C00B193\",\"ruleAttr\":\"type_A\",\"createTime\":\"2016-10-18T10:48:42+0000\",\"url\":\"https://updatebeta.hicloud.com/TDS/data/FF38E764923BF183D14E26F58988076A9E4423EC65B51AE3526B4888E05AECC2/1477137653/files/p3/s15/G1434/g1433/v64011/f1/\",\"reserveUrl\":\"\",\"versionType\":\"2\"}]}";
                        AddTXT("2%");
                        //oS.utilSetResponseBody(v1);
                        AddTXT("3%");
                    }
                    catch (Exception ee)
                    {
                        AddTXT("91%");
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

     

       
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FiddlerApplication.Shutdown();
            System.Environment.Exit(0);
            return;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            okimei = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FiddlerApplication.Shutdown();
            Go(int.Parse(textBox3.Text));
        }
    }
}