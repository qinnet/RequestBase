using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YueQian.ShortUrl.RequestBase.DataModel;
using Qcy.Mongo.Helper;

namespace YueQian.ShortUrl.RequestBase.Web.Models
{
    public class IndexViewModel //: EditorViewModelBase
    {

        public string PageTitle { get; set; }
        public string PageKeyword { get; set; }
        public string PageDescription { get; set; }

        private List<EditorModule> EditorModuleCollection { get; set; }

        public IndexViewModel()
        {
            PageTitle = "我只想事情是它本来的样子 - 求源";

            BuidEditorModule("焦点图片", "焦点图片", 680, 226, showSubject: true, showPicture: true, ShowDescription: true);
            BuidEditorModule("单张焦点", "单张焦点", 309, 226, showPicture: true);
            BuidEditorModule("头条新闻", "头条新闻", 1000, 135);

            BuidEditorModule("热点新闻", "热点新闻", 290, 250, showPicture: true);
            BuidEditorModule("最新资讯1", "最新资讯1", 358, 87, showSubject: true);
            BuidEditorModule("最新资讯2", "最新资讯2", 358, 87, showSubject: true);
            BuidEditorModule("最新资讯3", "最新资讯3", 358, 87, showSubject: true);
            BuidEditorModule("聚合头条", "聚合头条", 298, 264);

            BuidEditorModule("独家策划1", "独家策划1", 360, 170);
            BuidEditorModule("独家策划2", "独家策划2", 360, 170);
            BuidEditorModule("娱乐房产", "娱乐房产", 242, 315, showPicture: true);
            BuidEditorModule("楼市微言", "楼市微言", 360, 138);
            BuidEditorModule("楼市快报", "楼市快报", 360, 138);

            BuidEditorModule("热门新闻排行榜", "热门新闻排行榜", 350, 320);
            BuidEditorModule("高端访问1", "高端访问图文链接", 347, 171, showPicture: true);
            BuidEditorModule("高端访问2", "高端访问文字链接", 347, 134, showSubject: true);
            BuidEditorModule("市场调查", "市场调查", 250, 138);
            BuidEditorModule("政策法规", "政策法规", 250, 138);
            BuidEditorModule("楼市现场", "楼市现场", 290, 260, showPicture: true, ShowDescription: true);
            BuidEditorModule("小编探盘", "小编探盘", 290, 230, showPicture: true);
            BuidEditorModule("家居3601", "家居360.1", 353, 146, showSubject: true);
            BuidEditorModule("家居3602", "家居360.2", 353, 146, showSubject: true);
            BuidEditorModule("美家分享", "美家分享", 290, 230, showPicture: true);

            BuidEditorModule("社会民生1", "社会民生1", 360, 170);
            BuidEditorModule("社会民生2", "社会民生2", 360, 170);
            BuidEditorModule("图片新闻", "图片新闻", 242, 315, showPicture: true);
            BuidEditorModule("热点要闻", "热点要闻", 360, 138);
            BuidEditorModule("常州新闻", "常州新闻", 360, 138);

            //InitEditorModuleCollection();
        }

        public EditorModule GetModule(string moduleName)
        {
            // if (EditorModuleCollection == null) InitEditorModuleCollection();
            return EditorModuleCollection.FirstOrDefault(s => s.ModuleName == moduleName);
        }

        private void BuidEditorModule(string moduleName, string moduleDescription, int width, int height, bool showSubject = false, bool showPicture = false, bool ShowDescription = false)
        {
            EditorModule.Create(moduleName, moduleDescription, width, height, showSubject, showPicture, ShowDescription);
            if (EditorModuleCollection == null) EditorModuleCollection = new List<EditorModule>();
            var module = EditorModule.Get(moduleName); ;
            EditorModuleCollection.Add(module.Result);
        }

        //private async void InitEditorModuleCollection()
        //{
        //    IEnumerable<EditorModule> source = await MongoHelper.Find<EditorModule>(s => s.ModuleName != "");
        //    EditorModuleCollection = new List<EditorModule>();
        //    EditorModuleCollection = source.ToList();
        //}
    }

}