using LiteDB;
using Miunie.Core.Infrastructure;
using Miunie.Core.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Miunie.LiteDbStorage
{
    public class PersistentStorage : IPersistentStorage
    {
        private readonly string _dbFileName;

        public PersistentStorage(IFileSystem fileSystem)
        {
            _dbFileName = Path.Combine(fileSystem.DataStoragePath, "Miunie.db");
        }

        public IEnumerable<T> RestoreMany<T>(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                return collection.Find(predicate);
            }
        }

        public IEnumerable<T> RestoreAll<T>()
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                return collection.FindAll();
            }
        }

        public T RestoreSingle<T>(Expression<Func<T, bool>> predicate)
            => RestoreMany(predicate).FirstOrDefault();

        public bool Exists<T>(Expression<Func<T, bool>> predicate)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                return collection.Exists(predicate);
            }
        }

        public void Store<T>(T item)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                collection.Insert(item);
            }
        }

        public void Update<T>(T item)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                collection.Update(item);
            }
        }
    }
}
