using CCWin;
using CCWin.SkinControl;
using HwPush.Base;
using HwPush.CheckVersion.Models;
using HwPush.HwBase;
using HwPush.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HwPush.CheckVersion
{
    public partial class ScanVersion : CCSkinMain
    {

        public string ApiUrl = "http://120.24.163.11:8878/api/";
        //public string ApiUrl = "http://52.198.54.127:8878/api/";
        //public string ApiUrl = "http://localhost:8878/api/";
        string QQNumber = "";
        public ScanVersion()
        {
            InitializeComponent();
        }
        List<Hw_ScanVersion> SvList = new List<Hw_ScanVersion>();
        private void ScanVersion_Load(object sender, EventArgs e)
        {
            IOHelper.CreateDir("Log");
            IOHelper.CreateDir("FileList");
            IOHelper.CreateDir("Url");
            IOHelper.CreateDir("NotFound");
            QQNumber = IOHelper.Read("QQ.txt");
            if (string.IsNullOrWhiteSpace(QQNumber))
            {
                skinAlphaWaterTextBox1.Text = "请在软件旁边新建QQ.txt文件,里面填写上自己的QQ号码,并且在旁边放好Key文件.";
                return;
            }
            else
            {
                if (",289408880,119564557,891587944,834714126,591219179,717219383,".IndexOf("," + QQNumber + ",") < 0)
                {
                    skinAlphaWaterTextBox2.Text += "权限:您没有本软件的使用权\n\r";
                }

                PublicClass.GlobalQQNumber = Int64.Parse(QQNumber);
                skinAlphaWaterTextBox1.Text = QQNumber;
            }

            string PublicKey = IOHelper.Read("PublicKey.rsa");
            if (string.IsNullOrWhiteSpace(PublicKey))
            {
                skinAlphaWaterTextBox2.Text += "Key文件:PublicKey.rsa不存在\n\r";

            }
            string PrivateKey = IOHelper.Read(QQNumber + ".rsa");

            if (string.IsNullOrWhiteSpace(PrivateKey))
            {
                skinAlphaWaterTextBox2.Text += "Key文件:" + QQNumber + ".rsa不存在\n\r";

            }
            if (!string.IsNullOrWhiteSpace(skinAlphaWaterTextBox2.Text)) { return; }
            PublicClass.GlobalPublicKey = PublicKey;
            PublicClass.GlobalPrivateKey = PrivateKey;
            GetSaved();
            List<Hw_ScanVersion> SvListBg = SvList.Where(x => x.Sg == null).ToList();
            this.checkedListBox1.DataSource = SvListBg;
            this.checkedListBox1.DisplayMember = "Bg";
            this.checkedListBox1.ValueMember = "Id";
        }
        private async void skinButton1_Click(object sender, EventArgs e)
        {


            // List<Hw_ScanVersion> SvListBg = SvList.Where(x => x.Sg == null).ToList();
            // List<Hw_ScanVersion> SvListSg = SvList.Where(x => x.Sg != null).ToList();
            GetSaved();
            List<Hw_ScanVersion> SvListBg = SvList.Where(x => x.Sg == null).ToList();




            int BgS = int.Parse(skinTextBox1.Text);
            int BgE = int.Parse(skinTextBox2.Text);

            List<VersionUrl> NewVersions = new List<VersionUrl>();
            for (int i = BgS; i <= BgE; i++)
            {
                if (SvListBg.Where(m => m.Bg == i).FirstOrDefault() == null)
                {
                    NewVersions.Add(new VersionUrl
                    {
                        BG = i.ToString()
                    });
                }
            }

            NewVersions = NewVersions.OrderBy(x => x.v).OrderBy(x => x.BG).OrderBy(x => x.sg).OrderBy(x => x.f).ToList();

            await Scan(NewVersions);




























            //List<VersionUrl> AllVersions = new List<VersionUrl>();

            //foreach (var m in NewVersions)
            //{
            //    VersionUrl n = new VersionUrl
            //    {
            //        Name = m.Name,
            //        BG = m.BG,
            //        sg = m.sg
            //    };
            //    AllVersions.Add(n);

            //}


            //AllVersions = AllVersions.OrderBy(x => x.f).OrderBy(x => x.BG).OrderBy(x => x.sg).ToList();
            ////skinProgressBar1.Maximum = NewVersions.Count;//设置最大长度值
            ////skinProgressBar1.Value = 0;//设置当前值
            ////skinProgressBar1.Step = 1;//设置没次增长多少

            //await Scan(AllVersions);



        }
        List<Hw_ScanVersion> GetScanVersion = new List<Hw_ScanVersion>();

        private async Task Scan(List<VersionUrl> AllVersions)
        {
            skinProgressBar1.Maximum = AllVersions.Count;//设置最大长度值
            skinProgressBar1.Value = 0;//设置当前值
            skinProgressBar1.Step = 1;//设置没次增长多少


            int xcs = int.Parse(skinTextBox5.Text);
            await Task.Run(() =>
            {
                int k = 0;
                int all = 0;
                int allcount = AllVersions.Count;
                bool b = true;

                while (b)
                {
                    if (k > xcs)
                    {
                        continue;
                    }
                    k++;
                    if (all == allcount)
                    {
                        break;
                    }


                    List<string> hasv = new List<string>();
                    if (hasv.Contains(AllVersions[all].v))
                    {
                        all++;
                        k--;
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            try
                            {
                                skinProgressBar1.Value += skinProgressBar1.Step;
                            }
                            catch { }
                        });
                        continue;
                    }
                    Thread t = new Thread(async () =>
                    {
                        if (all < allcount)
                        {
                            var m = AllVersions[all++];
                            #region 跑数据
                            try
                            {
                                HttpClientHandler hch = new HttpClientHandler()
                                {
                                    //Proxy = new WebProxy("127.0.0.1", 8877),
                                    Proxy = new WebProxy(),
                                    UseProxy = true,
                                    AllowAutoRedirect = false
                                };
                                HttpClient hc = new HttpClient(hch);


                                HttpRequestMessage hr = new HttpRequestMessage();


                                string url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "";
                                if (!string.IsNullOrWhiteSpace(m.sg))
                                {
                                    url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "/g" + m.sg + "";


                                    if (!string.IsNullOrWhiteSpace(m.v) && m.v != "0")
                                    {
                                        url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "/g" + m.sg + "/v" + m.v + "";
                                        if (!string.IsNullOrWhiteSpace(m.f))
                                        {
                                            url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "/g" + m.sg + "/v" + m.v + "/f" + m.f + "/full/changelog.xml";
                                        }
                                    }
                                }
                                hr.RequestUri = new Uri(url);


                                HttpResponseMessage res = new HttpResponseMessage();

                                try
                                {

                                    res = await hc.SendAsync(hr);
                                }
                                catch (Exception ex)
                                {
                                    IOHelper.CreateDir("Exception");
                                    this.BeginInvoke((MethodInvoker)delegate
                                    {
                                        IOHelper.CreateFile("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                                        IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", DateTime.Now.ToString());
                                        IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ex.ToString());
                                    });
                                }



                                if (res.StatusCode == HttpStatusCode.OK)
                                {
                                    string Result = res.Content.ReadAsStringAsync().Result;
                                    string[] refSegments = res.RequestMessage.RequestUri.Segments;


                                    int Bg = 0, Sg = 0, V = 0, F = 0;
                                    if (refSegments.Length > 6)
                                    {
                                        for (int i = 6; i < refSegments.Length; i++)
                                        {
                                            string refs = refSegments[i];
                                            if (i == 6)
                                            {
                                                Bg = int.Parse(refs.TrimStart('G').TrimEnd('/'));
                                            }
                                            else if (i == 7)
                                            {
                                                Sg = int.Parse(refs.TrimStart('g').TrimEnd('/'));
                                            }
                                            else if (i == 8)
                                            {
                                                V = int.Parse(refs.TrimStart('v').TrimEnd('/'));
                                                hasv.Add(refs.TrimStart('v').TrimEnd('/'));
                                            }
                                            else if (i == 9)
                                            {
                                                F = int.Parse(refs.TrimStart('f').TrimEnd('/'));
                                            }
                                        }
                                    }
                                    var vname = "未知";
                                    try
                                    {
                                        vname = Result.Substring(Result.IndexOf("TCPU\" version=\"") + 15, Result.IndexOf("\"", Result.IndexOf("TCPU\" version=\"") + 15) - Result.IndexOf("TCPU\" version=\"") - 15);
                                    }
                                    catch { }



                                    string rurl = res.RequestMessage.RequestUri.AbsoluteUri;

                                    string filenameLog = "", filenameUrl = "", filenameList = "";
                                    try
                                    {
                                        filenameLog = "Log\\" + V + ".xml";
                                        filenameUrl = "Url\\" + vname + "_G" + Bg + "_g" + Sg + "_v" + V + "_f" + F + ".txt";
                                        filenameList = "FileList\\" + V + ".xml";
                                    }
                                    catch
                                    {
                                        filenameLog = "Log\\" + V + ".xml";
                                        filenameUrl = "Url\\" + vname + "_" + rurl.Substring(rurl.IndexOf("/G") + 1, rurl.IndexOf("/full") - rurl.IndexOf("/G") - 1).Replace("/full", "").Replace("/", "_") + ".txt";
                                        filenameList = "FileList\\" + V + ".xml";
                                    }

                                    if (IOHelper.Exists(filenameLog))
                                    {
                                        IOHelper.DeleteFile(filenameLog);
                                    }
                                    if (IOHelper.Exists(filenameUrl))
                                    {
                                        IOHelper.DeleteFile(filenameUrl);
                                    }
                                    IOHelper.CreateFile(filenameLog);
                                    IOHelper.CreateFile(filenameUrl);
                                    IOHelper.WriteLine(filenameLog, Result);
                                    IOHelper.WriteLine(filenameUrl, res.RequestMessage.RequestUri.AbsoluteUri);
                                    AddTXT(filenameUrl);
                                    GetFileList(res.RequestMessage.RequestUri.AbsoluteUri.Replace("changelog.xml", "filelist.xml"),filenameList);
                                }

                                else if (res.StatusCode == HttpStatusCode.Moved)
                                {
                                    Hw_ScanVersion sv = new Hw_ScanVersion();

                                    string Result = res.RequestMessage.RequestUri.Segments[6];
                                    string[] refSegments = res.RequestMessage.RequestUri.Segments;
                                    if (refSegments.Length > 6)
                                    {
                                        for (int i = 6; i < refSegments.Length; i++)
                                        {
                                            string refs = refSegments[i];
                                            if (i == 6)
                                            {
                                                sv.Bg = int.Parse(refs.TrimStart('G').TrimEnd('/'));
                                            }
                                            else if (i == 7)
                                            {
                                                sv.Sg = int.Parse(refs.TrimStart('g').TrimEnd('/'));
                                            }
                                            else if (i == 8)
                                            {
                                                sv.V = int.Parse(refs.TrimStart('v').TrimEnd('/'));
                                            }
                                            else if (i == 9)
                                            {
                                                sv.F = int.Parse(refs.TrimStart('f').TrimEnd('/'));
                                            }
                                        }
                                    }
                                    AddTXT(sv.Bg.ToString() + "/" + sv.Sg.ToString());
                                    GetScanVersion.Add(sv);

                                }
                                else if (res.StatusCode != HttpStatusCode.NotFound)
                                {
                                    string Result = "";
                                    Result = res.RequestMessage.RequestUri.Segments[6];


                                    string[] refSegments = res.RequestMessage.RequestUri.Segments;

                                    string Resultrul = "";
                                    if (refSegments.Length > 6)
                                    {
                                        for (int i = 6; i < refSegments.Length; i++)
                                        {
                                            Resultrul += refSegments[i];
                                        }
                                        string filename = "NotFound\\" + ((int)res.StatusCode).ToString() + "List" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                                        IOHelper.CreateFile(filename);
                                        IOHelper.WriteLine(filename, Resultrul);

                                        //AddTXT(Resultrul);
                                    }
                                }
                                //await Task.Delay(1);
                            }
                            catch (Exception ex)
                            {
                                //await Task.Delay(100);

                                IOHelper.CreateDir("Exception");
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    IOHelper.CreateFile("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                                    IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", DateTime.Now.ToString());
                                    IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ex.ToString());
                                });
                            }

                            k--;
                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                try
                                {
                                    skinProgressBar1.Value += skinProgressBar1.Step;
                                }
                                catch { }
                            });

                            #endregion

                        }

                    });
                    t.Start();

                    if (all % 100 == 0)
                    {
                        GC.Collect(); xcs = int.Parse(skinTextBox5.Text);
                    }

                }
            });




        }

        private async void GetFileList(string url,string filenameList)
        {
            HttpClientHandler hch = new HttpClientHandler()
            {
                //Proxy = new WebProxy("127.0.0.1", 8877),
                Proxy = new WebProxy(),
                UseProxy = true,
                AllowAutoRedirect = false
            };
            HttpClient hc = new HttpClient(hch);


            HttpRequestMessage hr = new HttpRequestMessage();



            hr.RequestUri = new Uri(url);


            HttpResponseMessage res = new HttpResponseMessage();

            try
            {
                res = await hc.SendAsync(hr);
            }
            catch (Exception ex)
            {
                IOHelper.CreateDir("Exception");
                this.BeginInvoke((MethodInvoker)delegate
                {
                    IOHelper.CreateFile("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
                    IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", DateTime.Now.ToString());
                    IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ex.ToString());
                });
            }
            if (res.StatusCode == HttpStatusCode.OK)
            {
                string Result = res.Content.ReadAsStringAsync().Result;                
                if (IOHelper.Exists(filenameList))
                {
                    IOHelper.DeleteFile(filenameList);
                }
                IOHelper.CreateFile(filenameList); 
                IOHelper.WriteLine(filenameList, Result);  
            }
        }
        private void GetSaved()
        {
            try
            {
                string pk = HTMLHelper.Post_Http(ApiUrl + "Scan/GetVersion");
                var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);

                string PrivateKey = IOHelper.Read(skinAlphaWaterTextBox1.Text + ".rsa");
                if (!string.IsNullOrEmpty(PrivateKey))
                {

                    string rsaqqinfo = RsaHelper.RSACrypto.Decrypt(PrivateKey, pkd.Data.ToString());


                    List<Hw_ScanVersion> list = JsonConvert.DeserializeObject<List<Hw_ScanVersion>>(rsaqqinfo);
                    SvList = list.Where(x => x.V == null).OrderBy(y => y.Bg).ToList();
                }
            }
            catch (Exception ex) { }
        }

   

        private void skinButton2_Click(object sender, EventArgs e)
        {
            skinButton4_Click(null, null);
            //try
            //{
            //    GetSaved();
            //    List<Hw_ScanVersion> SvListBg = SvList.Where(x => x.Sg == null).ToList();
            //    List<Hw_ScanVersion> NewList = new List<Hw_ScanVersion>();

            //    GetScanVersion.ForEach(x =>
            //    {
            //        if (SvListBg.Where(m => m.Bg == x.Bg).Count() == 0)
            //        {
            //            NewList.Add(x);
            //        }
            //    });
            //    string PublicKey = IOHelper.Read("PublicKey.rsa");
            //    if (!string.IsNullOrEmpty(PublicKey))
            //    {
            //        var Dis = NewList.Distinct().ToList();
            //        string rsaqqinfo = RsaHelper.RSACrypto.Encrypt(PublicKey, JsonConvert.SerializeObject(Dis));
            //        string pk = HTMLHelper.Post_Http(ApiUrl + "Scan/SetVersion", rsaqqinfo);

            //        var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
            //        MessageBox.Show(pkd.Message + ":" + pkd.Data);
            //    }


            //}
            //catch (Exception ee) { MessageBox.Show("超时,重试:" + ee.Message); }

        }

        private async void skinButton3_Click(object sender, EventArgs e)
        {



            int SgS = int.Parse(skinTextBox3.Text);
            int SgE = int.Parse(skinTextBox4.Text);
            List<Hw_ScanVersion> SvListSg = SvList.Where(x => x.Sg != null).ToList();
            List<VersionUrl> NewVersions = new List<VersionUrl>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                int? Bg = ((Hw_ScanVersion)item).Bg;
                if (SvListSg.Where(m => m.Sg != null && m.Bg == Bg).Count() == 0)
                {
                    for (int i = SgS; i <= SgE; i++)
                    {

                        NewVersions.Add(new VersionUrl
                        {
                            BG = Bg.ToString(),
                            sg = i.ToString(),
                        });
                    }
                }
            }


            await Scan(NewVersions);



        }

        private void skinButton4_Click(object sender, EventArgs e)
        {
            try
            {
                GetSaved();
                //List<Hw_ScanVersion> SvListSg = SvList.Where(x => x.Sg != null).ToList();
                List<Hw_ScanVersion> NewList = new List<Hw_ScanVersion>();

                GetScanVersion.ForEach(x =>
                {
                    if (SvList.Where(m => m.Bg == x.Bg && m.Sg == x.Sg).Count() == 0)
                    {
                        NewList.Add(x);
                    }
                });
                string PublicKey = IOHelper.Read("PublicKey.rsa");
                if (!string.IsNullOrEmpty(PublicKey))
                {
                    var Dis = NewList.Distinct().ToList();
                    string rsaqqinfo = RsaHelper.RSACrypto.Encrypt(PublicKey, JsonConvert.SerializeObject(Dis));
                    string pk = HTMLHelper.Post_Http(ApiUrl + "Scan/SetVersion", rsaqqinfo);

                    var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
                    MessageBox.Show(pkd.Message + ":" + pkd.Data);
                }

                ScanVersion_Load(null, null);
            }
            catch (Exception ee) { MessageBox.Show("超时,重试:" + ee.Message); }

        }
        private void AddTXT(string str)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                SkinListBoxItem slbi = new SkinListBoxItem(str);
                skinListBox1.Items.Add(slbi);
            });
        }

        private void skinButton5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                else
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            checkedListBox1_SelectedIndexChanged(null, null);
        }

        private void skinButton6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    checkedListBox2.SetItemChecked(i, false);
                }
                else
                {
                    checkedListBox2.SetItemChecked(i, true);
                }
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Hw_ScanVersion> SvListSg = SvList.Where(x => x.Sg != null).ToList();
            //checkedListBox2.Items.Clear();
            List<string> lcf = new List<string>();
            List<Hw_ScanVersion> NewList = new List<Hw_ScanVersion>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                int? Bg = ((Hw_ScanVersion)item).Bg;
                var SgSelect = SvListSg.Where(x => x.Bg == Bg).ToList();
                NewList.AddRange(SgSelect);
                //SgSelect.ForEach(x =>
                //{
                //    this.checkedListBox2.Items.Add(x.Bg + "_" + x.Sg);
                //    //lcf.Add(x.Bg + "_" + x.Sg);
                //});
                //var rs = SgSelect.Select(x => new ).ToList();
                //lcf.AddRange(rs);

                //NewList.AddRange(SvListSg.Where(x => x.Bg == Bg));
            }
            this.checkedListBox2.DataSource = NewList;
            this.checkedListBox2.DisplayMember = "Gg";
            this.checkedListBox2.ValueMember = "Id";
        }

        private async void skinButton7_Click(object sender, EventArgs e)
        {
            int vS = int.Parse(skinTextBox6.Text);
            int vE = int.Parse(skinTextBox7.Text);
            List<Hw_ScanVersion> SvListSg = SvList.Where(x => x.Sg != null).ToList();
            List<VersionUrl> NewVersions = new List<VersionUrl>();
            foreach (var item in checkedListBox2.CheckedItems)
            {
                int? Bg = ((Hw_ScanVersion)item).Bg;
                int? Sg = ((Hw_ScanVersion)item).Sg;
                if (SvListSg.Where(m => m.Sg == Sg && m.Bg == Bg).Count() > 0)
                {
                    for (int i = vS; i <= vE; i++)
                    {
                        foreach (var fitem in checkedListBox3.CheckedItems)
                        {
                            NewVersions.Add(new VersionUrl
                            {
                                BG = Bg.ToString(),
                                sg = Sg.ToString(),
                                v = i.ToString(),
                                f = fitem.ToString().TrimStart('f')
                            });
                        }
                    }
                }
            }


            await Scan(NewVersions);

        }

        private void skinListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string re = IOHelper.Read(skinListBox1.SelectedItem.ToString());
                skinToolTip1.ToolTipTitle = skinListBox1.SelectedItem.ToString();

                skinToolTip1.SetToolTip(this.skinListBox1, re);
            }
            catch { }
        }

        private void skinButton8_Click(object sender, EventArgs e)
        {
             

            List<VersionUrl> NewVersions = new List<VersionUrl>();

            foreach (var item in checkedListBox2.CheckedItems)
            {
                Hw_ScanVersion sv = (Hw_ScanVersion)item;
                VersionUrl vu = new VersionUrl
                {
                    Name = "G" + sv.Bg + "/g" + sv.Sg,
                    BG = sv.Bg.ToString(),
                    sg = sv.Sg.ToString(),
                    v = sv.V.ToString(),
                    f = sv.F.ToString()
                };





                 
                NewVersions.Add(vu);
            }


            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Title = "另存为";
                saveFileDialog1.InitialDirectory = @"\";
                saveFileDialog1.Filter = "文本(*.txt)|*.txt";
                saveFileDialog1.AddExtension = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string savestr = JsonConvert.SerializeObject(NewVersions);

                    FileStream fs = null;
                    StreamWriter sw = null;
                    //Stream sr = null;
                    try
                    {
                        fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
                        fs.Close();


                        sw = new StreamWriter(saveFileDialog1.FileName);
                        sw.Write(savestr);
                        sw.Flush();
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (fs != null)
                            fs.Close();
                        sw.Close();
                        //sr.Close();
                    }
                }
            }
        }

        private void skinButton9_Click(object sender, EventArgs e)
        {
            LookupVersion lv = new LookupVersion();
            lv.Show();
            this.Hide();
        }
    }
}
