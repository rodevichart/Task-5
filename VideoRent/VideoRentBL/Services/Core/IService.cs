﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VideoRentBL.Services.Core
{
    public interface IService<TDto> where TDto : class
    {
        TDto Get(int id);
        IEnumerable<TDto> GetAll();
        IEnumerable<TDto> Find(Expression<Func<TDto, bool>> predicate);
        TDto SingleOrDefault(Expression<Func<TDto, bool>> predicate);
        void Add(TDto dtoObj);
        void AddRange(IList<TDto> dtoObjs);
        void Remove(int id);
        void RemoveRange(IEnumerable<TDto> dtoObjs);
        void Update(TDto dtoObj);
    }
}