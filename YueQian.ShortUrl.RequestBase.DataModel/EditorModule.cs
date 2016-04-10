using Qcy.Mongo.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YueQian.ShortUrl.RequestBase.DataModel
{
    public class EditorModule : EntityBase
    {
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public bool ShowDescription { get; set; }
        public bool ShowSubject { get; set; }
        public bool ShowPicture { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public IList<EditorLink> Links { get; set; }

        public EditorModule() { }

        public static async Task<EditorModule> Get(string moduleName)
        {
            return await MongoHelper.FindOne<EditorModule>(s => s.ModuleName == moduleName);
        }


        public static async void Create(string moduleName, string ModuleDescription, int width, int height, bool ShowSubject = false, bool ShowPicture = false, bool ShowDescription = false)
        {
            EditorModule model = await MongoHelper.FindOne<EditorModule>(s => s.ModuleName == moduleName);
            if (model == null)
            {
                model = new EditorModule { ModuleName = moduleName };
            }

            model.ModuleDescription = ModuleDescription;
            model.ShowSubject = ShowSubject;
            model.ShowPicture = ShowPicture;
            model.ShowDescription = ShowDescription;
            model.Width = width;
            model.Height = height;
            model.CreationDate = DateTime.Now;
            await MongoHelper.Save(model);
        }

        public static async void InitEditorModuleCollection(List<EditorModule> EditorModuleCollection)
        {
            if (EditorModuleCollection == null) EditorModuleCollection = new List<EditorModule>();

            IEnumerable<EditorModule> source = await MongoHelper.Find<EditorModule>(s => s.ModuleName != "");

            EditorModuleCollection = source.ToList();
        }
    }
}
