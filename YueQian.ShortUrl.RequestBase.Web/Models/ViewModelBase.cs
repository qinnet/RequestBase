using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YueQian.ShortUrl.RequestBase.Web.Models
{
    public class ViewModelBase
    {
        public string PageTitle { get; set; }
        public string PageKeyword { get; set; }
        public string PageDescription { get; set; }
    }
}