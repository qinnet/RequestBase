using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueQian.ShortUrl.RequestBase.DataModel
{
    public class Topic : Article
    {
        /// <summary>
        /// 起源
        /// </summary>
        public string ComeFrom { get; set; }
        
        /// <summary>
        /// 起源地址
        /// </summary>
        public string ComeFromUrl { get; set; }
    }
}
