using HwPush.Base;
using HwPush.HwBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using HwPush.Model;

namespace HwPush.Push
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //string[] a = RsaHelper.GenerateKeys();
            //string a0 = a[0];
            //string a1 = a[1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PublicKey = IOHelper.Read("PublicKey.rsa");
            if (string.IsNullOrEmpty(PublicKey))
            {
                string aa = HTMLHelper.Post_Http("http://localhost:8878/api/Key/GetPublicKey");
                var bb = JsonConvert.DeserializeObject<AjaxResCode>(aa);
                PublicKey = bb.Data.ToString();
                IOHelper.CreateFile("PublicKey.rsa");
                IOHelper.Write("PublicKey.rsa", PublicKey);
            }


            string PrivateKey = IOHelper.Read("119564557.rsa");
            if (string.IsNullOrEmpty(PrivateKey))
            {
                string rsaqq = RsaHelper.RSAEncrypt(PublicKey, "119564557");

                var pk = HTMLHelper.Post_Http("http://localhost:8878/api/Key/GetPrivateKey");
                var pkd = JsonConvert.DeserializeObject<AjaxResCode>(pk);
                PrivateKey = pkd.Data.ToString();

                IOHelper.CreateFile("119564557.rsa");
                IOHelper.Write("119564557.rsa", PrivateKey);
            }
        }
    }
}
