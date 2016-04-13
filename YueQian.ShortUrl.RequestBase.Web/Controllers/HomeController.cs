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
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(bool editMode = false)
        {

            ViewBag.isAdmin = true;
            ViewBag.isShow = true && editMode;
            var model = new IndexViewModel();
            model.List = await MongoHelper.Find<Topic>(s => !string.IsNullOrEmpty(s.Title));
            return View(model);
        }

        public async Task<ActionResult> Topic(string id)
        {
            var hotTopic = await MongoHelper.FindOne<Topic>(id);
            if (hotTopic == null) return Redirect("/");
            var model = new TopicViewModel();
            model.TopicInfo = hotTopic;
            model.PageTitle = string.Format("{0} - 求源", hotTopic.Title);
            model.PageKeyword = hotTopic.Keywords;
            model.PageDescription = hotTopic.Description;

            var collection = (await MongoHelper.Find<News>(s => s.TopicId == id)).ToList();
            if (collection != null)
            {
                model.Collection = collection;
                model.Nav = collection.Select(s => new ListItem { Title = s.ShortTitle, Url = string.Format("#rb_{0}", s.Id) });
            }
            return View(model);
        }

        public async Task<ActionResult> Details(string id)
        {
            var news = await MongoHelper.FindOne<News>(id);
            if (news == null) return Redirect("/");

            var model = new DetailViewModel();
            var topic = await MongoHelper.FindOne<Topic>(s => s.Id == news.TopicId);

            model.NewsInfo = news;
            model.TopicInfo = topic;
            model.PageTitle = string.Format("{0} - 求源", news.Title);
            return View(model);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public async Task<ActionResult> Editor(string module)
        {
            EditorModule model = await EditorModule.Get(module);

            if (model == null) return Content("不存在该模块,请先创建!");
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Editor(EditorModule em, EditorLink newModel)
        {
            if (em.Links == null) em.Links = new List<EditorLink>();
            if (newModel != null && !string.IsNullOrEmpty(newModel.Title))
            {
                newModel.CreationDate = DateTime.Now;
                em.Links.Add(newModel);
            }
            em.CreationDate = DateTime.Now;
            await MongoHelper.Save(em);
            return Content("<html><head><script>alert('提交成功!');window.location.href='/?editMode=true'</script></head></html>");
        }



        static string[] AllowExs = new string[] { ".jpg", ".gif", ".png" };

        public ContentResult Upload(string id, string name = "")
        {
            if (Request.Files == null || Request.Files.Count == 0) return Content("ERROR");
            var file = Request.Files[0];
            if (file.ContentLength == 0) return Content("ERROR");
            var ex = System.IO.Path.GetExtension(file.FileName).ToLower();
            if (!AllowExs.Contains(ex)) return Content("ERROR");

            string path = Request.PhysicalApplicationPath + "Attachment\\" + id + "\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            string filename = !string.IsNullOrEmpty(name) ? name : (DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random(DateTime.Now.Millisecond).Next(9999));

            try
            {
                if (System.IO.File.Exists(path + filename + ex))
                    System.IO.File.Delete(path + filename + ex);
                file.SaveAs(path + filename + ex);
                return Content(string.Format("/Attachment/{0}/{1}{2}", id, filename, ex));
            }
            catch
            {
                return Content("ERROR");
            }
        }



    }

}