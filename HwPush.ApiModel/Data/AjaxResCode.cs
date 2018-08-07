using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HwPush.ApiModel.Data
{
    /// <summary>
    /// ajax请求返回类
    /// </summary>
    [Serializable]
    [DataContract]
    public class AjaxResCode
    {
        private int _resultcode = 0;
        [DataMember]
        public int ResultCode
        {
            get { return _resultcode; }
            set { _resultcode = value; }
        }

        private string _message = "";
        [DataMember]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private object _data = "";
        [DataMember]
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
    [Serializable]
    [DataContract]
    public class SoapResultList : AjaxResCode
    {
        private int _totalrecord = 0;
        [DataMember]
        public int TotalCount
        {
            get { return _totalrecord; }
            set { _totalrecord = value; }
        }

        private int _pageindex = 1;
        [DataMember]
        public int PageIndex
        {
            get { return _pageindex; }
            set { _pageindex = value; }
        }

        private int _pagesize = 0;
        [DataMember]
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }

        private int _totalpage = 0;
        [DataMember]
        public int TotalPage
        {
            get
            {
                if (PageSize > 0 && TotalCount > 0)
                {
                    _totalpage = Convert.ToInt16(Math.Ceiling((double)TotalCount / (double)PageSize));
                }
                return _totalpage;
            }
            set { _totalpage = value; }
        }

    }

}
