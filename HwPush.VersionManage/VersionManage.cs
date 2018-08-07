using HwPush.HwBase;
using HwPush.HwModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HwPush.VersionManage
{
    public partial class VersionManage : Form
    {
        public VersionManage()
        {
            InitializeComponent();
        }
        List<HwModelInfo> HMIList = new List<HwModelInfo>();
        List<VersionDetail> VDIList = new List<VersionDetail>();
        List<MobileModel> MBList = new List<MobileModel>();
        private void VersionManage_Load(object sender, EventArgs e)
        {
            //var VersionStr = PublicClass.GetYouDaoShare().Replace("&nbsp;", " ");
            //HMIList = JsonConvert.DeserializeObject<List<HwModelInfo>>(VersionStr);
            //HMIList = HMIList.OrderBy(m => m.SortId).ToList();
            //dataGridView1.DataSource = HMIList.ToList();
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;//获取当前行  
            //MessageBox.Show(rowIndex.ToString());

            string s = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(); //获取当前行xh字段的值 



            var HMLv1 = HMIList.Where(m => m.VersionName == s).FirstOrDefault();
            if (HMLv1 != null)
            {
                VDIList = HMLv1.VersionDetail;
                if (VDIList != null)
                {
                    dataGridView2.DataSource = VDIList.OrderBy(m => m.SortId).ToList();
                }
                else {
                    VDIList = new List<VersionDetail>();
                    dataGridView2.DataSource = new List<VersionDetail>(); }
                MBList = HMLv1.MobileModel;
                if (MBList != null)
                {
                    dataGridView3.DataSource = MBList.OrderBy(m => m.SortId).ToList();
                }
                else { MBList = new List<MobileModel>(); dataGridView3.DataSource = new List<MobileModel>(); }
            }
            else
            {
                dataGridView2.DataSource = new List<VersionDetail>();
                dataGridView3.DataSource = new List<MobileModel>();
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;//获取当前行  
            //MessageBox.Show(rowIndex.ToString());
            string s = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(); //获取当前行xh字段的值 
            List<HwModelInfo> HMIref = Datev1ToInfo();
            List<VersionDetail> vdref = Datev2ToInfo();
            HMIList = HMIref;
            VDIList = vdref;
            HMIList.Where(m => m.VersionName == s).FirstOrDefault().VersionDetail = VDIList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Title = "另存为";
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.Filter = "文本(*.txt)|*.txt";
                saveFileDialog1.AddExtension = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string savestr = JsonConvert.SerializeObject(HMIList);

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


        private List<HwModelInfo> Datev1ToInfo()
        {
            object obv1 = dataGridView1.DataSource;
            List<HwModelInfo> HMI = (List<HwModelInfo>)dataGridView1.DataSource;
            return HMI;
        }
        private List<VersionDetail> Datev2ToInfo()
        {
            object obv2 = dataGridView2.DataSource;
            List<VersionDetail> VD = (List<VersionDetail>)dataGridView2.DataSource;
            return VD;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var VersionStr = PublicClass.GetYouDaoShare("http://note.youdao.com/yws/api/group/23191275/note/108644959?method=get-content&shareToken=89CEBD7471604EADA6598AA4D97046F8").Replace("&nbsp;", " ");
            HMIList = JsonConvert.DeserializeObject<List<HwModelInfo>>(VersionStr);
            HMIList = HMIList.OrderBy(m => m.SortId).ToList();
            dataGridView1.DataSource = HMIList.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string key = "小烈哥威武~!@#$%^&*()_+QQ:119564557." + PublicClass.GetpublicKey();
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Title = "另存为";
                saveFileDialog1.InitialDirectory = @"c:\\";
                saveFileDialog1.Filter = "文本(*.txt)|*.txt";
                saveFileDialog1.AddExtension = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string savestr = JsonConvert.SerializeObject(HMIList);

                    FileStream fs = null;
                    StreamWriter sw = null;
                    //Stream sr = null;
                    try
                    {
                        fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite);
                        fs.Close();
                        sw = new StreamWriter(saveFileDialog1.FileName);
                        sw.Write(DESEncrypt.Encrypt(savestr, key));//加密版本库
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

        private void button4_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
                openFileDialog1.Filter = "文本(*.txt)|*.txt";
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FilterIndex = 1;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {


                    // string savestr = JsonConvert.SerializeObject(HMIList);

                    FileStream fs = null;

                    //Stream sr = null;
                    try
                    {
                        fs = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                        int fsLen = (int)fs.Length;
                        byte[] heByte = new byte[fsLen];
                        int r = fs.Read(heByte, 0, heByte.Length);
                        string myStr = System.Text.Encoding.UTF8.GetString(heByte);


                        HMIList = JsonConvert.DeserializeObject<List<HwModelInfo>>(myStr);
                        HMIList = HMIList.OrderBy(m => m.SortId).ToList();
                        dataGridView1.DataSource = HMIList.ToList();


                        fs.Close();

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        if (fs != null)
                            fs.Close();

                        //sr.Close();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HwModelInfo hminew = new HwModelInfo
            {
                VersionName = "新机型",
                PhoneModel = "NewPhone",
                Operator = "all",
                VersionDetail = new List<VersionDetail>(),
                IsValid = "0",
                SortId = 999

            };
            HMIList.Add(hminew);
            dataGridView1.DataSource = HMIList.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();//获取当前行[0]字段的值  
            HMIList = HMIList.Where(m => m.VersionName != s).ToList();
            dataGridView1.DataSource = HMIList.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            VersionDetail vd = new VersionDetail
            {

                IsValid = "0",
                VersionType = "1",
                oldversion = "",
                version = "",
                description = "新版本",
                createTime = "",
                Name = "新升级包",
                BG = "0",
                sg = "0",
                v = "0",
                f = "0",
                SortId = 999
            };


            VDIList.Add(vd);
            string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();//获取当前行[0]字段的值  
            HMIList.Where(m => m.VersionName == s).FirstOrDefault().VersionDetail = VDIList;
            dataGridView1.DataSource = HMIList.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();//获取当前行[0]字段的值  
            string s2 = dataGridView2.CurrentRow.Cells[0].Value.ToString();//获取当前行[0]字段的值  
            VDIList = VDIList.Where(m => m.Name != s2).ToList();
            HMIList.Where(m => m.VersionName == s).FirstOrDefault().VersionDetail = VDIList;
            dataGridView1.DataSource = HMIList.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            MobileModel mb = new MobileModel
            {
                PhoneModel = "",
                Operator = "",
                IsValid = "0",

                Name = "新机型",

                SortId = 999
            };


            MBList.Add(mb);
            string s = dataGridView1.CurrentRow.Cells[0].Value.ToString();//获取当前行[0]字段的值  
            HMIList.Where(m => m.VersionName == s).FirstOrDefault().MobileModel = MBList;
            dataGridView1.DataSource = HMIList.ToList();
        }
    }
}
