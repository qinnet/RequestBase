using Qcy.Mongo.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueQian.ShortUrl.RequestBase.DataModel
{
    public class EditorLink : EntityBase
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

        public string Subject { get; set; }
        public string SubjectUrl { get; set; }

        public string Picture { get; set; }

        public string ViewCount{ get; set; }
    }
}
