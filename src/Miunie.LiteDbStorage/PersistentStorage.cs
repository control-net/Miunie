// This file is part of Miunie.
//
//  Miunie is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Miunie is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with Miunie. If not, see <https://www.gnu.org/licenses/>.

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
                _ = collection.Insert(item);
            }
        }

        public void Update<T>(T item)
        {
            using (var db = new LiteDatabase(_dbFileName))
            {
                var collection = db.GetCollection<T>();
                _ = collection.Update(item);
            }
        }
    }
}
