using Qcy.Mongo.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueQian.ShortUrl.RequestBase.DataModel
{
    public abstract class Article : EntityBase
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }

        public string CreationDataString
        {
            get
            {
                if (CreationDate != null)
                {
                    return CreationDate.ToString("yyyy/MM/dd HH:mm:ss");
                }
                return "";
            }
        }
    }
}
