using YueQian.ShortUrl.RequestBase.DataModel;
using Qcy.Mongo.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YueQian.ShortUrl.RequestBase.Web.Models
{
    public class ListItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
    public class TopicViewModel
    {
        public Topic TopicInfo { get; set; }

        public IEnumerable<ListItem> Nav { get; set; }
        public IEnumerable<News> Collection { get; set; }

        public TopicViewModel()
        {
            Nav = new List<ListItem>();
            Collection = new List<News>();
        }

    }
    public class DetailViewModel
    {
        public Topic TopicInfo { get; set; }
        public News NewsInfo { get; set; }
    }
}