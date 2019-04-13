using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Miunie.Core.Storage
{
    public interface IPersistentStorage
    {
        void Store<T>(T item);
        void Update<T>(T item);
        IEnumerable<T> RestoreMany<T>(Expression<Func<T, bool>> predicate);
        T RestoreSingle<T>(Expression<Func<T, bool>> predicate);
        bool Exists<T>(Expression<Func<T, bool>> predicate);
    }
}
