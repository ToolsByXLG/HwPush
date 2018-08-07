using System;
using System.Collections.Generic;

namespace HwPush.CheckVersion.Models
{
    public partial class Hw_Users
    {
        public int Id { get; set; }
        public Nullable<int> QQNumber { get; set; }
        public string UserName { get; set; }
        public string PublicKey { get; set; }
        public string PublicKeyMd5 { get; set; }
        public string PrivateKey { get; set; }
        public string PrivateKeyMd5 { get; set; }
        public string IMEIS { get; set; }
        public string QunIds { get; set; }
        public string Roles { get; set; }
        public string QQInfo { get; set; }
    }
}
