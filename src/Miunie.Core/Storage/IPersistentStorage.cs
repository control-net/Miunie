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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Miunie.Core.Storage
{
    public interface IPersistentStorage
    {
        void Store<T>(T item);

        void Update<T>(T item);

        void Remove<T>(Expression<Func<T, bool>> predicate);

        IEnumerable<T> RestoreMany<T>(Expression<Func<T, bool>> predicate);

        IEnumerable<T> RestoreAll<T>();

        T RestoreSingle<T>(Expression<Func<T, bool>> predicate);

        bool Exists<T>(Expression<Func<T, bool>> predicate);
    }
}
