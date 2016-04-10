using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Qcy.Mongo.Helper
{
    public class MongoHelper
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        static MongoHelper()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase("RequestBase");
        }

        private static IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = typeof(T).Name;
            return _database.GetCollection<T>(collectionName);
        }

        public static async Task Save<T>(T entity) where T : EntityBase
        {
            var collection = GetCollection<T>();
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
                await collection.InsertOneAsync(entity);
            }
            else
            {
                var filter = Builders<T>.Filter.Eq(s => s.Id, entity.Id);
                await collection.ReplaceOneAsync(filter, entity);
            }
        }

        public static async Task Delete<T>(FilterDefinition<T> filter) where T : EntityBase
        {
            var collection = GetCollection<T>();
            await collection.DeleteOneAsync(filter);
        }

        public static async Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> filter) where T : EntityBase
        {
            var collection = GetCollection<T>();
            return await collection.Find(filter).ToListAsync();
        }

        public static async Task<T> FindOne<T>(string Id) where T : EntityBase
        {
            var collection = GetCollection<T>();
            var filter = Builders<T>.Filter.Eq(s => s.Id, Id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public static async Task<T> FindOne<T>(Expression<Func<T, bool>> filter) where T : EntityBase
        {
            var collection = GetCollection<T>();
            return await collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
