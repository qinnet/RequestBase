using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YueQian.ShortUrl.RequestBase.DataModel;
using Qcy.Mongo.Helper;

namespace YueQian.ShortUrl.RequestBase.Web.Models
{
    public class IndexViewModel : ViewModelBase
    {

        public IEnumerable<Topic> List { get; set; }

        private List<EditorModule> EditorModuleCollection { get; set; }

        public IndexViewModel()
        {
            PageTitle = "我只想事情是它本来的样子 - 求源";
        }
    }

}