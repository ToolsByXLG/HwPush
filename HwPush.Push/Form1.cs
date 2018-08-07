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
using System.Xml.Serialization;
using HwPush.HwBase;
using System.Xml;
using HwPush.Base;

namespace HwPush.Push
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string xml = "<?xml version='1.0' encoding='utf-8'?><root><rule name='C_Version'>C00</rule><rule name='D_version'>D022</rule><rule name='DashBoard'>41.003.53.00.06</rule><rule name='DeviceName'>NXT-AL10</rule><rule name='FirmWare'>EMUI-NXT-AL10C00B022-log</rule><rule name='IMEI'>RSA:ReQIiQYwkbs/muXU59nvLSLVDkZxAU0Ae2J48NaeVCEmghvKHko/mhnKK2kHq/kwYVg1updYP1F0gMPOVB/0LlHrJBRuNGXso/yTxL6AFqBF+xvlTAcLOcDs/1ce5IxryA3Iedk2+MffSZfhyMBmFPUrXQTiD+SVqpxPIQCy9cE=</rule><rule name='Language'>zh_CN</rule><rule name='OS'>Windows 7</rule><rule name='SaleInfo'>|cn|all|N|EmotionUI_4.1|4.0 GB|64.00 GB|8_2.3GHz</rule></root>".Replace("'", "\"");

            try
            {

                //Mate8RequestBody rules = null;

                //    rules = PublicClass.GetRulesByXml(xml);


                //string json = JsonConvert.SerializeObject(rules);

                string json = "{'status':'0','components':[{'name':'NXT-AL10C00B023','version':'NXT-AL10C00B023','versionID':'52186','description':'【测试】022-023','createTime':'2016-04-29T10:00:00','url':'http://update.hicloud.com:8180/TDS/data/files/p3/s15/G1254/g272/v52186/f1/'}]}".Replace("'", "\"");

                // XmlDocument doc1 = JsonConvert.DeserializeXmlNode(json);


                Mate8Version m8v= JsonConvert.DeserializeObject<Mate8Version>(json);

                string xml2 = XmlUtil.Serializer(typeof(Mate8Version), m8v);

            }
            catch
            {

            }

        }

        protected static DataSet GetDataSetByXml(string xmlData)
        {
            try
            {
                DataSet ds = new DataSet();

                using (StringReader xmlSR = new StringReader(xmlData))
                {

                    ds.ReadXml(xmlSR, XmlReadMode.InferTypedSchema); //忽视任何内联架构，从数据推断出强类型架构并加载数据。如果无法推断，则解释成字符串数据
                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
      
    }
}
