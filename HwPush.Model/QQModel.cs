using System;
using System.Collections.Generic;

namespace HwPush.HwModel
{
    [Serializable]
    public class QQModel
    {
        public int adm_max { get; set; }
        public int adm_num { get; set; }
        public int count { get; set; }
        public int ec { get; set; }
        public int max_count { get; set; }
        public List<mems> mems { get; set; }
        public int search_count { get; set; }
        public int svr_time { get; set; }
        public int vecsize { get; set; }
        public int QunNumber { get; set; }
    }


    public class mems
    {
        public string card { get; set; }
        public int flag { get; set; }
        public int g { get; set; }
        public int join_time { get; set; }
        public int last_speak_time { get; set; }
        public lv lv { get; set; }
        public string nick { get; set; }
        public int qage { get; set; }
        public int role { get; set; }
        public string tags { get; set; }
        public Int64 uin { get; set; }
    }
    public class lv
    {
        public int level { get; set; }
        public int point { get; set; }
    }



    public class myinfo
    {
        //{"retcode":0,"result":{"p2c":"44_3","face":615,"birthday":{"month":8,"year":1988,"day":15},"phone":"0755","gender_id":1,"allow":1,"extflag":12980480,"college":"******","lbs_addr_detail":{"street_no":"Unknown","village":"民乐新村","street":"Unknown","name":"中国,广东省,深圳市,宝安区","province":"广东省","town":"Unknown","code":"440306","district":"宝安区","nation":"中国","city":"深圳市"},"cft_flag":0,"h_zone":"23","reg_type":0,"city":"深圳","h_city":"7","city_id":"3","personal":"******","shengxiao":5,"province":"广东","gender":"male","longitude":114.046836,"s_flag":0,"occupation":"计算机/互联网/IT","zone_id":"5","province_id":"44","country_id":"1","constel":7,"blood":1,"homepage":"******","country":"中国","flag":323519046,"h_country":"1","nick":"烈影","email":"w_lwy@qq.com","gps_flag":0,"h_province":"43","latitude":22.607367,"mobile":"135********"}}
        public int retcode { get; set; }
        public result result { get; set; }


    }
    public class lbs_addr_detail
    {
        public string street_no { get; set; }
        public string village { get; set; }
        public string street { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string town { get; set; }
        public string code { get; set; }
        public string district { get; set; }
        public string nation { get; set; }
        public string city { get; set; }
    }
    public class birthday
    {
        public int month { get; set; }
        public int year { get; set; }
        public int day { get; set; }
    }
    public class result
    {
        public string p2c { get; set; }
        public int face { get; set; }
        public birthday birthday { get; set; }
        public string phone { get; set; }
        public int gender_id { get; set; }
        public int allow { get; set; }
        public int extflag { get; set; }
        public string college { get; set; }
        public lbs_addr_detail lbs_addr_detail { get; set; }
        public int cft_flag { get; set; }
        public string h_zone { get; set; }
        public int reg_type { get; set; }
        public string city { get; set; }
        public string h_city { get; set; }
        public string city_id { get; set; }
        public string personal { get; set; }
        public int shengxiao { get; set; }
        public string province { get; set; }
        public string gender { get; set; }
        public double longitude { get; set; }
        public int s_flag { get; set; }
        public string occupation { get; set; }
        public string zone_id { get; set; }
        public string province_id { get; set; }
        public string country_id { get; set; }
        public int constel { get; set; }
        public int blood { get; set; }
        public string homepage { get; set; }
        public string country { get; set; }
        public int flag { get; set; }
        public string h_country { get; set; }
        public long qqnumber { get; set; }
        public string nick { get; set; }
        public string email { get; set; }
        public int gps_flag { get; set; }
        public string h_province { get; set; }
        public double latitude { get; set; }
        public string mobile { get; set; }
    }
    public class Hw_Users
    {
        public int Id { get; set; }
        public long QQNumber { get; set; }
        public string UserName { get; set; }
        public string PublicKey { get; set; }
        public string PublicKeyMd5 { get; set; }
        public string PrivateKey { get; set; }
        public string PrivateKeyMd5 { get; set; }
        public Nullable<long> IMEI { get; set; }
        public string QunIds { get; set; }
        public string Roles { get; set; }
        public string QQInfo { get; set; }
        public int Gold { get; set; }
    }
}