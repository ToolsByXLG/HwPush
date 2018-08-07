using System;
using System.Collections.Generic;
using System.Text;

namespace HwPush.Model
{
    [Serializable]
    public class YouDaoModel
    {
        public string content { get; set; }
        public string tl { get; set; }
        public int mt { get; set; }
        public string p { get; set; }
        public int domain { get; set; }
        public string su { get; set; }
        public int pv { get; set; }
        public int sz { get; set; }
        public int pr { get; set; }
        public int ct { get; set; }
        public string au { get; set; }
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
    public class VersionUrl
    {
        public string Name { get; set; }
        public string BG { get; set; }
        public string sg { get; set; }
        public string v { get; set; }
        public string f { get; set; }
        public string Operator { get; set; }
    }
    public class VersionUrlExtend : VersionUrl
    {
        public string p { get; set; }
        public string s { get; set; }
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
}