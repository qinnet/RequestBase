using MongoDB.Driver;
using YueQian.ShortUrl.RequestBase.DataModel;
using YueQian.ShortUrl.RequestBase.Web.Models;
using Qcy.Mongo.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YueQian.ShortUrl.RequestBase.Web.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        #region /*(-_-)*/话题/*(-_-)*/
        public async Task<JsonResult> Topics(int curPage = 1, int pageSize = 20, string keyword = "")
        {
            var source = await MongoHelper.Find<Topic>(s => s.Title.Contains(keyword));

            var list = source.OrderByDescending(s => s.CreationDate).Skip(pageSize * (curPage - 1)).Take(pageSize).ToList();

            return Json(new { success = true, totalRows = source.Count(), curPage = curPage, data = list });
        }

        /// <summary>
        /// 搜索话题，返回下拉列表选项
        /// </summary>
        public async Task<JsonResult> TopicsSearch(int curPage = 1, int pageSize = 20, string q = "", string Id = "", int length = 0)
        {

            if (string.IsNullOrEmpty(Id))
            {
                var source = await MongoHelper.Find<Topic>(s => s.Title.Contains(q));
                if (length == 1)
                {
                    if (source != null)
                        return Json(source.FirstOrDefault().Id,JsonRequestBehavior.AllowGet);
                    return null;
                }
                var list = source.OrderByDescending(s => s.CreationDate).Skip(pageSize * (curPage - 1)).Take(pageSize)
                                 .Select(s => s.Title).ToList();

                return Json(string.Join("\n", list), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = await MongoHelper.FindOne<Topic>(s => s.Id == Id);
                if (model != null) return Json(new { id = model.Id, text = model.Title }, JsonRequestBehavior.AllowGet);
                return null;
            }

        }

        #endregion

        #region /*(-_-)*/资讯/*(-_-)*/
        public async Task<JsonResult> News(int curPage = 1, int pageSize = 20, string keyword = "")
        {
            var source = await MongoHelper.Find<News>(s => s.Title.Contains(keyword));

            var list = source.OrderByDescending(s => s.CreationDate).Skip(pageSize * (curPage - 1)).Take(pageSize).ToList();

            return Json(new { success = true, totalRows = source.Count(), curPage = curPage, data = list });
        }
        #endregion

        #region /*(-_-)*/基础页面/*(-_-)*/

        /// <summary>
        /// 列表
        /// </summary>
        public ActionResult List(string dt = "News")
        {
            return View(dt);
        }

        /// <summary>
        /// 详情页
        /// </summary>
        public async Task<ActionResult> Details(string id, DataModelType dt = DataModelType.News)
        {
            switch (dt)
            {
                default:
                case DataModelType.News:
                    return await Details<News>(id);
                case DataModelType.Topic:
                    return await Details<Topic>(id);
            }
        }
        /// <summary>
        /// 详情页获取数据
        /// </summary>
        private async Task<ActionResult> Details<T>(string id) where T : EntityBase
        {
            var model = await MongoHelper.FindOne<T>(id);
            return View(string.Format("{0}Info", typeof(T).Name), model);
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        [HttpPost, ValidateInput(false)]
        public async Task<JsonResult> Update(string Id, DataModelType dt = DataModelType.News)
        {

            switch (dt)
            {
                default:
                case DataModelType.News:
                    var news = await MongoHelper.FindOne<News>(Id);
                    if (news == null) news = new News();
                    UpdateModel(news);
                    news.CreationDate = DateTime.Now;
                    return Json(new { result = MongoHelper.Save(news).Status });
                case DataModelType.Topic:
                    var topic = await MongoHelper.FindOne<Topic>(Id);
                    if (topic == null) topic = new Topic();
                    UpdateModel(topic);
                    topic.CreationDate = DateTime.Now;
                    return Json(new { result = MongoHelper.Save(topic).Status });
            }
        }

        /// <summary>
        /// 删除一个实体
        /// </summary>
        [HttpPost]
        public async Task<JsonResult> Delete(string id, DataModelType dt = DataModelType.News)
        {
            switch (dt)
            {
                default:
                case DataModelType.News:
                    return await Delete<News>(id);
                case DataModelType.Topic:
                    return await Delete<Topic>(id);
            }
        }
        private async Task<JsonResult> Delete<T>(string id) where T : EntityBase
        {
            var filter = Builders<T>.Filter.Eq(s => s.Id, id);
            await MongoHelper.Delete(filter);
            return Json(new { result = true });
        }
        #endregion

    }

    public enum DataModelType
    {
        News, Topic
    }
}