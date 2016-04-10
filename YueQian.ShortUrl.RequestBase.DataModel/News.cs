using Qcy.Mongo.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueQian.ShortUrl.RequestBase.DataModel
{
    public class News : Article
    {
        public string CategoryId { get; set; }
        /// <summary>
        /// 关联话题
        /// </summary>
        public string TopicId { get; set; }

        /// <summary>
        /// 新闻来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 源地址
        /// </summary>
        public string SourceUrl { get; set; }

    }
}
