using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HwPush.HwModel
{



    /// <summary>
    /// 手机型号信息
    /// </summary>
    public class HwModelInfo
    {
        /// <summary>
        /// 版本名称 
        /// </summary>
        public string VersionName { get; set; }

        /// <summary>
        /// 手机型号 (废弃) 
        /// </summary>
        public string PhoneModel { get; set; }

        /// <summary>
        /// 运营商 all cmcc (废弃)
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 升级版本明细
        /// </summary>
        public List<VersionDetail> VersionDetail { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public string IsValid { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }


        /// <summary>
        /// 手机型号 
        /// </summary>
        public List<MobileModel> MobileModel { get; set; }
    }

    public class MobileModel
    {
        /// <summary>
        /// 手机型号名称
        /// </summary>
        public string Name { get; set; } 
        /// <summary>
        /// 手机型号
        /// </summary>
        public string PhoneModel { get; set; }

        /// <summary>
        /// 运营商 all cmcc
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string IsValid { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }
    }


        /// <summary>
        /// 升级版本明细
        /// </summary>
        public class VersionDetail
    {
        /// <summary>
        /// 升级包名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string IsValid { get; set; }
        /// <summary>
        /// 版本类型 1正式版 2内测版 3降级版
        /// </summary>
        public string VersionType { get; set; }


        /// <summary>
        /// 升级前版本
        /// </summary>
        public string oldversion { get; set; }
        /// <summary>
        /// 升级版本
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 版本描述 172-321
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string createTime { get; set; }



        /// <summary>
        /// 大G 900
        /// </summary>
        public string BG { get; set; }
        /// <summary>
        /// 小g 77
        /// </summary>
        public string sg { get; set; }
        /// <summary>
        /// v 48702
        /// </summary>
        public string v { get; set; }
        /// <summary>
        /// f 1-2
        /// </summary>
        public string f { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int SortId { get; set; }

    }












    public class Mate8List
    {
        public List<Mate8Model> Mate8Model { get; set; }
    }
    public class Mate8Model
    {
        public string ModelName { get; set; }
        public string ModelNumber { get; set; }//884
        public List<UpdateList> UpdateList { get; set; }
    }
    public class UpdateList
    {
        public string oldversion { get; set; }
        public string version { get; set; }
        public string versionID { get; set; }
        public string description { get; set; }
        public string createTime { get; set; }
    }

    public class Mate8Version
    {
        public string status { get; set; }
        public List<VersionInfo> components { get; set; }
    }
    public class VersionInfo
    {
        public string name { get; set; }
        public string version { get; set; }
        public string versionID { get; set; }
        public string description { get; set; }
        public string createTime { get; set; }
        public string url { get; set; }
    }
    public class Mate8RequestBody
    {
        public rules rules { get; set; }
    }
    public class rules
    {
        public string FingerPrint { get; set; }
        public string DeviceName { get; set; }
        public string FirmWare { get; set; }
        public string IMEI { get; set; }
        public string IMSI { get; set; }
        public string Language { get; set; }
        public string OS { get; set; }
        public string HotaVersion { get; set; }
        public string saleinfo { get; set; }
        public string C_version { get; set; }
        public string D_version { get; set; }
        public string devicetoken { get; set; }
        public string PackageType { get; set; }
        public string ControlFlag { get; set; }
        public string extra_info { get; set; }
    }
    public class Mate8RequestXML
    {
        public rule rule { get; set; }
    }
    public class rule
    {
        public string FingerPrint { get; set; }
        public string DeviceName { get; set; }
        public string FirmWare { get; set; }
        public string IMEI { get; set; }
        public string IMSI { get; set; }
        public string Language { get; set; }
        public string OS { get; set; }
        public string HotaVersion { get; set; }
        public string saleinfo { get; set; }
        public string C_version { get; set; }
        public string D_version { get; set; }
        public string devicetoken { get; set; }
        public string PackageType { get; set; }
        public string ControlFlag { get; set; }
        public string extra_info { get; set; }
    }


    public class RequestXML
    {
        public string name { get; set; }
        public string rule_Text { get; set; }
    }


    public class HwConfig
    {
        public string configureID { get; set; }
        public string isStrictMatch { get; set; }
        public List<condPara> condParaList { get; set; }
    }

    public class condPara
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public  class Hw_UserVersionLibrary
    {

        public int id { get; set; }
        public int UId { get; set; }
        public string DeviceName { get; set; }
        public string D_version { get; set; }
        public string version { get; set; }
        public string FirmWare { get; set; }
        public string Json { get; set; }
        public Nullable<int> IsResource { get; set; }
        public Nullable<long> IMEI { get; set; }
    }
}