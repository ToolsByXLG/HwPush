using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Mate8OEM
{
    public partial class OEM : Form
    {
        public OEM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;



            var resultFile = "oeminfo";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                resultFile = openFileDialog1.FileName;




            try
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    FileStream fs = new FileStream(resultFile, FileMode.Open, FileAccess.Read);
                    FileStream fs2 = new FileStream(resultFile, FileMode.Open, FileAccess.Read);






                    BinaryReader br = new BinaryReader(fs);
                    byte[] AllByte = new byte[fs.Length];

                    fs2.Read(AllByte, 0, Convert.ToInt32(fs2.Length));

                    int type = 0;
                    if (fs2.Length != 67108864 || AllByte[393733] != 76)
                    {
                        MessageBox.Show("文件不是Mate8的OEM文件");
                        return;
                    }

                    if (radioButton1.Checked)
                    {
                        switch (AllByte[393732])
                        {
                            case 65://al
                                type = 4;//cl
                                break;
                            case 67://cl
                                type = 1;//al
                                break;
                            case 68://dl
                                type = 2;//tl
                                break;
                            case 84://tl
                                type = 3;//dl
                                break;
                        }
                    }
                    else if (radioButton2.Checked)
                    {
                        type = 1;
                    }
                    else if (radioButton3.Checked)
                    {
                        type = 2;
                    }
                    else if (radioButton4.Checked)
                    {
                        type = 3;
                    }
                    else if (radioButton5.Checked)
                    {
                        type = 4;
                    }
                   


                    AllByte = null;
                    fs2.Close();


                    int length = (int)fs.Length / 16;
                    int i = 0;
                    byte[] newByte = new byte[fs.Length];
                    while (length > 0)
                    {
                        byte[] tempByte = br.ReadBytes(16);
                        if (i == 4353)
                        {

                            if (type == 1)
                            {
                                tempByte[4] = 6;//全网all
                            }
                            if (type == 2)
                            {
                                tempByte[4] = 7;//移动cmcc
                            }
                            if (type == 3)
                            {
                                tempByte[4] = 9;//联通dualcu
                            }

                            if (type == 4)
                            {
                                tempByte[4] = 10;//电信 telecom
                            }

                        }

                        if (i == 4384)
                        {
                            if (type == 1)
                            {
                                //全网all
                                tempByte[0] = 97;
                                tempByte[1] = 108;
                                tempByte[2] = 108;

                                tempByte[3] = 47;
                                tempByte[4] = 99;
                                tempByte[5] = 110;
                                tempByte[6] = 255;
                                tempByte[7] = 255;
                                tempByte[8] = 255;
                                tempByte[9] = 255;
                                tempByte[10] = 255;
                                tempByte[11] = 255;
                                tempByte[12] = 255;
                                tempByte[13] = 255;
                                tempByte[14] = 255;
                                tempByte[15] = 255;
                            }
                            if (type == 2)
                            {
                                //移动cmcc
                                tempByte[0] = 99;
                                tempByte[1] = 109;
                                tempByte[2] = 99;
                                tempByte[3] = 99;

                                tempByte[4] = 47;
                                tempByte[5] = 99;
                                tempByte[6] = 110;
                                tempByte[7] = 255;
                                tempByte[8] = 255;
                                tempByte[9] = 255;
                                tempByte[10] = 255;
                                tempByte[11] = 255;
                                tempByte[12] = 255;
                                tempByte[13] = 255;
                                tempByte[14] = 255;
                                tempByte[15] = 255;
                            }
                            if (type == 3)
                            {
                                //联通dualcu
                                tempByte[0] = 100;
                                tempByte[1] = 117;
                                tempByte[2] = 97;
                                tempByte[3] = 108;
                                tempByte[4] = 99;
                                tempByte[5] = 117;

                                tempByte[6] = 47;
                                tempByte[7] = 99;
                                tempByte[8] = 110;
                                tempByte[9] = 255;
                                tempByte[10] = 255;
                                tempByte[11] = 255;
                                tempByte[12] = 255;
                                tempByte[13] = 255;
                                tempByte[14] = 255;
                                tempByte[15] = 255;
                            }


                            if (type == 4)
                            {
                                //电信 telecom
                                tempByte[0] = 116;
                                tempByte[1] = 101;
                                tempByte[2] = 108;
                                tempByte[3] = 101;
                                tempByte[4] = 99;
                                tempByte[5] = 111;
                                tempByte[6] = 109;


                                tempByte[7] = 47;
                                tempByte[8] = 99;
                                tempByte[9] = 110;
                                tempByte[10] = 255;
                                tempByte[11] = 255;
                                tempByte[12] = 255;
                                tempByte[13] = 255;
                                tempByte[14] = 255;
                                tempByte[15] = 255;
                            }

                        }

                        if (i == 23072)
                        {

                            if (type == 1)//al
                            {
                                tempByte[11] = 65;
                                tempByte[12] = 76;
                                tempByte[13] = 49;
                            }
                            if (type == 2)//tl
                            {
                                tempByte[11] = 84;
                                tempByte[12] = 76;
                                tempByte[13] = 48;
                            }
                            if (type == 3)//dl
                            {
                                tempByte[11] = 68;
                                tempByte[12] = 76;
                                tempByte[13] = 48;
                            }

                            if (type == 4)//cl
                            {
                                tempByte[11] = 67;
                                tempByte[12] = 76;
                                tempByte[13] = 48;
                            }

                        }
                        if (i == 24608)
                        {
                            if (type == 1)//al
                            {
                                tempByte[4] = 65;
                                tempByte[5] = 76;
                                tempByte[6] = 49;
                            }
                            if (type == 2)//tl
                            {
                                tempByte[4] = 84;
                                tempByte[5] = 76;
                                tempByte[6] = 48;
                            }
                            if (type == 3)//dl
                            {
                                tempByte[4] = 68;
                                tempByte[5] = 76;
                                tempByte[6] = 48;
                            }
                            if (type == 4)//cl
                            {
                                tempByte[4] = 67;
                                tempByte[5] = 76;
                                tempByte[6] = 48;
                            }

                        }
                        for (int bi = 0; bi < 16; bi++)
                        {
                            newByte[i * 16 + bi] = tempByte[bi];
                        }

                        i++;
                        length--;
                    }

                    string newfilename = "OEM_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (type == 1)//al
                    {
                        newfilename = "AL_" + newfilename;
                    }
                    if (type == 2)//tl
                    {
                        newfilename = "TL_" + newfilename;
                    }
                    if (type == 3)//dl
                    {
                        newfilename = "DL_" + newfilename;
                    }
                    if (type == 4)//cl
                    {
                        newfilename = "CL_" + newfilename;
                    }
                    Stream flstr = new FileStream(newfilename, FileMode.Create);
                    BinaryWriter sw = new BinaryWriter(flstr, Encoding.Default);
                    byte[] buffer = newByte;
                    sw.Write(buffer);
                    sw.Close();
                    flstr.Close();
                    fs.Close();
                    br.Close();



                    MessageBox.Show("转换成功,感谢D2NotRooted技术支持\n\r新文件为" + newfilename, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            catch (Exception ee)
            {
                MessageBox.Show("出错:" + ee.Message);
            }
        }

        private void OEM_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
            return;
        }
    }
}