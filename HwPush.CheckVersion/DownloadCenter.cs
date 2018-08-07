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
    public partial class DownloadCenter : CCSkinMain
    {
        public DownloadCenter()
        {
            InitializeComponent();
        }

        private async void skinButton1_Click(object sender, EventArgs e)
        {
            IOHelper.CreateDir("Log");
            IOHelper.CreateDir("FileList");
            IOHelper.CreateDir("Url");
            IOHelper.CreateDir("NotFound");


            int istartG = int.Parse(skinTextBox1.Text);
            int isendG = int.Parse(skinTextBox2.Text);
            int xcs = int.Parse(skinTextBox2.Text);




            List<int> AllVersions = new List<int>();
            for (int i = istartG; i <= isendG; i++)
            {
               


                    AllVersions.Add(i);

                
            }
             
            skinProgressBar1.Maximum = AllVersions.Count;//设置最大长度值
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
                                    //AllowAutoRedirect = false
                                };
                                HttpClient hc = new HttpClient(hch);


                                HttpRequestMessage hr = new HttpRequestMessage();



                                string url = "http://download-c.huawei.com/download/downloadCenter?downloadId="+m;

                                hr.RequestUri = new Uri(url);
                                hr.Method = new HttpMethod("HEAD");


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

                                    string FileName = res.Content.Headers.ContentDisposition.FileName;
                                    try
                                    {
                                        FileName = System.Web.HttpUtility.UrlDecode(FileName, System.Text.Encoding.UTF8);
                                    }
                                    catch { }
                                    long? ContentLength = res.Content.Headers.ContentLength;
                                    string AbsoluteUri = res.RequestMessage.RequestUri.AbsoluteUri;



                                    //filenameLog = "Log\\" + V + ".xml";
                                    //filenameUrl = "Url\\" + vname + "_" + rurl.Substring(rurl.IndexOf("/G") + 1, rurl.IndexOf("/full") - rurl.IndexOf("/G") - 1).Replace("/full", "").Replace("/", "_") + ".txt";
                                    string filenameList = "FileList\\" + FileName.Replace("\"", "") + ".txt";

                                    if (IOHelper.Exists(filenameList))
                                    {
                                        IOHelper.DeleteFile(filenameList);
                                    }

                                    IOHelper.CreateFile(filenameList);

                                    IOHelper.WriteLine(filenameList, FileName + "\n\r" + AbsoluteUri + "\n\r" + ContentLength);
                                    AddTXT(filenameList);

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