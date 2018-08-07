using System;
using System.Collections.Generic;

namespace HwPush.Api.Models
{
    public partial class Hw_Users
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
