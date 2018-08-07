
using HwPush.HwBase;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int Month = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            int WeekOfYear = 0;
            int Year = 0;

            int Day = 0;
            PublicClass.GetYearAndWeekOfYear(ref Year, ref Month, ref Day, ref WeekOfYear, DateTime.Now);
            textBox3.Text = Year.ToString();
            textBox4.Text = WeekOfYear.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string key1 = "小烈哥威武~!@#$%^&*()_+QQ:119564557." + PublicClass.GetpublicKey();
            string key = "小烈哥" + textBox4.Text + "威武" + textBox3.Text + "~!@#$%^&*()_+QQ:119564557." + PublicClass.GetpublicKey();

            string kk = DESEncrypt.Encrypt(key1, key);//加密版本库

            textBox5.Text = kk;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string key = "小烈哥威武~!@#$%^&*()_+QQ:119564557." + PublicClass.GetpublicKey();
            var VersionStr = PublicClass.GetYouDaoShare().Replace("&nbsp;", " ");

            textBox6.Text = VersionStr;
            string kk = DESEncrypt.Encrypt(VersionStr, key);//加密版本库
            VersionStr = DESEncrypt.Decrypt(kk, key);
            textBox7.Text = kk;
        }
    }
}