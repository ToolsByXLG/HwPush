using CCWin;
using CCWin.SkinControl;
using HwPush.HwBase;
using HwPush.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HwPush.CheckVersion
{
    public partial class LookupVersionALL : CCSkinMain
    {
        public LookupVersionALL()
        {
            InitializeComponent();
        }

        private async void skinButton1_Click(object sender, EventArgs e)
        {
            IOHelper.CreateDir("Log");
            IOHelper.CreateDir("FileList");
            IOHelper.CreateDir("Url");
            IOHelper.CreateDir("NotFound");


            int istartG = int.Parse(skinTextBox3.Text);
            int isendG = int.Parse(skinTextBox4.Text);

            int istartv = 0;
            int isendv = 0;


            int xcs = int.Parse(skinTextBox5.Text);

            //var kk1 = checkedListBox1.CheckedItems.OfType<char>();
            List<string> lcf = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                lcf.Add(item.ToString().Replace("f", ""));
            }
            List<string> lcg = new List<string>();
            foreach (var item in checkedListBox2.CheckedItems)
            {
                lcg.Add(item.ToString().Replace("g", ""));
            }
            if (checkedListBox2.CheckedItems.Count == 0)
            {
                int istartsg = int.Parse(skinTextBox6.Text);
                int isendsg = int.Parse(skinTextBox7.Text);

                for (int sGI = istartsg; sGI <= isendsg; sGI++)
                {
                    lcg.Add(sGI.ToString());
                }
            }

            List<VersionUrl> NewVersions = new List<VersionUrl>();


            if (skinRadioButton1.Checked)
            {
                for (int GI = istartG; GI <= isendG; GI++)
                {

                    VersionUrl vu = new VersionUrl
                    {
                        BG = GI.ToString()
                    };
                    NewVersions.Add(vu);

                }
            }
            else
            if (skinRadioButton2.Checked)
            {
                for (int GI = istartG; GI <= isendG; GI++)
                {
                    lcg.ForEach(mg =>
                    {


                        VersionUrl vu = new VersionUrl
                        {

                            BG = GI.ToString(),
                            sg = mg
                        };
                        NewVersions.Add(vu);
                    });


                }
            }
            else
            if (skinRadioButton3.Checked)
            {
                for (int GI = istartG; GI <= isendG; GI++)
                {
                    lcg.ForEach(mg =>
                    {

                        VersionUrl vu = new VersionUrl
                        {

                            BG = GI.ToString(),
                            sg = mg
                        };
                        NewVersions.Add(vu);

                    });
                }
                istartv = int.Parse(skinTextBox1.Text);
                isendv = int.Parse(skinTextBox2.Text);
            }

            if (skinRadioButton4.Checked)
            {
                for (int GI = istartG; GI <= isendG; GI++)
                {
                    lcg.ForEach(mg =>
                    {
                        lcf.ForEach(mf =>
                        {
                            VersionUrl vu = new VersionUrl
                            {

                                BG = GI.ToString(),
                                sg = mg,
                                f = mf
                            };
                            NewVersions.Add(vu);
                        });
                    });
                }
                istartv = int.Parse(skinTextBox1.Text);
                isendv = int.Parse(skinTextBox2.Text);


            }



            List<VersionUrl> AllVersions = new List<VersionUrl>();
            for (int i = istartv; i <= isendv; i++)
            {
                foreach (var m in NewVersions)
                {
                    VersionUrl n = new VersionUrl
                    {
                        Name = m.Name,
                        BG = m.BG,
                        sg = m.sg,
                        v = i.ToString(),
                        f = m.f,
                        Operator = m.Operator
                    };



                    AllVersions.Add(n);

                }
            }

            AllVersions = AllVersions.OrderBy(x => x.v).OrderBy(x => x.BG).OrderBy(x => x.sg).OrderBy(x => x.f).ToList();
            skinProgressBar1.Maximum = (isendv - istartv + 1) * NewVersions.Count;//设置最大长度值
            skinProgressBar1.Value = 0;//设置当前值
            skinProgressBar1.Step = 1;//设置没次增长多少







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
                                    GetFileList(res.RequestMessage.RequestUri.AbsoluteUri.Replace("changelog.xml", "filelist.xml"), filenameList);

                                }
                                //else if (res.StatusCode == HttpStatusCode.MovedPermanently)
                                //{
                                //    string Result = "";
                                //    Result = res.RequestMessage.RequestUri.Segments[6];


                                //    string[] refSegments = res.RequestMessage.RequestUri.Segments;

                                //    string Resultrul = "";
                                //    if (refSegments.Length > 6)
                                //    {
                                //        for (int i = 6; i < refSegments.Length; i++)
                                //        {
                                //            Resultrul += refSegments[i];
                                //        }
                                //        string filename = "Log\\301List" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                                //        IOHelper.CreateFile(filename);
                                //        IOHelper.WriteLine(filename, Resultrul);

                                //        AddTXT(Resultrul);
                                //    }
                                //}
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

                                        AddTXT(Resultrul);
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

        //    Parallel.ForEach(AllVersions.AsEnumerable(), async m =>
        // {


        // GC.Collect();
        //try
        //{

        //    //if (!string.IsNullOrWhiteSpace(Operator) && !string.IsNullOrWhiteSpace(G))
        //    //{  
        //    HttpClientHandler hch = new HttpClientHandler()
        //    {
        //        //Proxy = new WebProxy("127.0.0.1", 8877),
        //        Proxy = new WebProxy(),
        //        UseProxy = true,
        //        AllowAutoRedirect = false
        //    };
        //    HttpClient hc = new HttpClient(hch);


        //    HttpRequestMessage hr = new HttpRequestMessage();


        //    string url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "";
        //    if (!string.IsNullOrWhiteSpace(m.sg))
        //    {
        //        url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "/g" + m.sg + "";
        //        if (!string.IsNullOrWhiteSpace(m.v) && m.v != "0")
        //        {
        //            url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "/g" + m.sg + "/v" + m.v + "";
        //            if (!string.IsNullOrWhiteSpace(m.f))
        //            {
        //                url = "http://update.hicloud.com:8180/TDS/data/files/p3/s15/G" + m.BG + "/g" + m.sg + "/v" + m.v + "/f" + m.f + "/full/changelog.xml";
        //            }
        //        }
        //    }
        //    hr.RequestUri = new Uri(url);


        //    HttpResponseMessage res = new HttpResponseMessage();
        //    //await Task.Delay(1);
        //    try
        //    {

        //        res = await hc.SendAsync(hr);
        //    }
        //    catch (Exception ex)
        //    {
        //        //await Task.Delay(100);
        //        IOHelper.CreateDir("Exception");
        //        this.BeginInvoke((MethodInvoker)delegate
        //        {
        //            IOHelper.CreateFile("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        //            IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", DateTime.Now.ToString());
        //            IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ex.ToString());
        //        });
        //    }



        //    if (res.StatusCode == HttpStatusCode.OK)
        //    {
        //        string Result = res.Content.ReadAsStringAsync().Result;

        //        var vname = "未知";
        //        try
        //        {
        //            vname = Result.Substring(Result.IndexOf("TCPU\" version=\"") + 15, Result.IndexOf("\"", Result.IndexOf("TCPU\" version=\"") + 15) - Result.IndexOf("TCPU\" version=\"") - 15);
        //        }
        //        catch { }



        //        string rurl = res.RequestMessage.RequestUri.AbsoluteUri;

        //        string filename = "";
        //        try
        //        {
        //            filename = "Log\\" + vname + "_" + rurl.Substring(rurl.IndexOf("/G") + 1, rurl.IndexOf("/cn") - rurl.IndexOf("/G") - 1).Replace("/full", "").Replace("/", "_") + ".txt";
        //        }
        //        catch
        //        {
        //            filename = "Log\\" + vname + "_" + rurl.Substring(rurl.IndexOf("/G") + 1, rurl.IndexOf("/full") - rurl.IndexOf("/G") - 1).Replace("/full", "").Replace("/", "_") + ".txt";
        //        }

        //        if (IOHelper.Exists(filename))
        //        {
        //            IOHelper.DeleteFile(filename);
        //        }
        //        IOHelper.CreateFile(filename);
        //        IOHelper.WriteLine(filename, res.RequestMessage.RequestUri.AbsoluteUri + "\n\r" + Result);
        //        AddTXT(filename);

        //    }
        //    else if (res.StatusCode == HttpStatusCode.MovedPermanently)
        //    {
        //        string Result = "";
        //        Result = res.RequestMessage.RequestUri.Segments[6];


        //        string[] refSegments = res.RequestMessage.RequestUri.Segments;

        //        string Resultrul = "";
        //        if (refSegments.Length > 6)
        //        {
        //            for (int i = 6; i < refSegments.Length; i++)
        //            {
        //                Resultrul += refSegments[i];
        //            }
        //            string filename = "Log\\302List" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //            IOHelper.CreateFile(filename);
        //            IOHelper.WriteLine(filename, Resultrul);

        //            AddTXT(Resultrul);
        //        }
        //    }
        //    await Task.Delay(1);
        //}
        //catch (Exception ex)
        //{
        //    //await Task.Delay(100);

        //    IOHelper.CreateDir("Exception");
        //    this.BeginInvoke((MethodInvoker)delegate
        //    {
        //        IOHelper.CreateFile("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        //        IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", DateTime.Now.ToString());
        //        IOHelper.WriteLine("Exception\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", ex.ToString());
        //    });
        //}

        //this.BeginInvoke((MethodInvoker)delegate
        //{
        //    try
        //    {

        //        skinProgressBar1.Value += skinProgressBar1.Step;
        //    }
        //    catch { }
        //});



        // });
        //        });



        private void AddTXT(string str)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                SkinListBoxItem slbi = new SkinListBoxItem(str);
                skinListBox1.Items.Add(slbi);
            });
        }
        List<VersionUrl> Versions = new List<VersionUrl>();
        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void skinListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string re = IOHelper.Read(skinListBox1.SelectedItem.ToString());
                toolTip1.ToolTipTitle = skinListBox1.SelectedItem.ToString();

                toolTip1.SetToolTip(this.skinListBox1, re);
            }
            catch { }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {

        }

        private void LookupVersionALL_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }
        private async void GetFileList(string url, string filenameList)
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
    }
}