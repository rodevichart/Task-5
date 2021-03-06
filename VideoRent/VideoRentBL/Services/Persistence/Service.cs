﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoMapper;
using VideoRentBL.Services.Core;
using VideoRentDAL.Core;
using VideoRentDAL.Core.Repositories;

namespace VideoRentBL.Services.Persistence
{
    public abstract class Service<T, TDto> : IService<TDto>
        where T : class
        where TDto : class
    {
        protected readonly IRepository<T> Repository;
        protected readonly IUnitOfWork UnitOfWork;
        private static readonly Mutex Mutex = new Mutex();

        protected Service(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        public TDto Get(int id)
        {
            Mutex.WaitOne();
            var result = Mapper.Map<T, TDto>(Repository.Get(id));
            Mutex.ReleaseMutex();
            return result;
        }

        public IEnumerable<TDto> GetAll()
        {
            Mutex.WaitOne();
            var result = Repository.GetAll().Select(Mapper.Map<T, TDto>);
            Mutex.ReleaseMutex();
            return result;
        }



        public IEnumerable<TDto> Find(Expression<Func<TDto, bool>> predicate)
        {            
            var expression = Mapper.Map<Expression<Func<T, bool>>>(predicate);
            Mutex.WaitOne();
            var result = Repository.Find(expression).Select(Mapper.Map<T, TDto>);
            Mutex.ReleaseMutex();
            return result;
        }

        public TDto SingleOrDefault(Expression<Func<TDto, bool>> predicate)
        {
            var expression = Mapper.Map<Expression<Func<T, bool>>>(predicate);
            Mutex.WaitOne();
            var result = Mapper.Map<T, TDto>(Repository.SingleOrDefault(expression));
            Mutex.ReleaseMutex();
            return result;           
        }


        public virtual void Add(TDto dtoObj)
        {
            var dto = Mapper.Map<TDto, T>(dtoObj);
            Mutex.WaitOne();
            Repository.Add(dto);
            UnitOfWork.Complete();
            Mutex.ReleaseMutex();
        }

        public void AddRange(IList<TDto> dtoObjs)
        {
            var dto = Mapper.Map<IList<TDto>, IList<T>>(dtoObjs);
            Mutex.WaitOne();
            Repository.AddRange(dto);
            UnitOfWork.Complete();
            Mutex.ReleaseMutex();

        }


        public void Remove(int id)
        {
           // var dto = Mapper.Map<TDto, T>(dtoObj);
            Mutex.WaitOne();
            Repository.Remove(id);
            UnitOfWork.Complete();
            Mutex.ReleaseMutex();

        }

        public void RemoveRange(IEnumerable<TDto> dtoObjs)
        {
            var dto = Mapper.Map<IEnumerable<TDto>, IEnumerable<T>>(dtoObjs);
            Mutex.WaitOne();
            Repository.RemoveRange(dto);
            UnitOfWork.Complete();
            Mutex.ReleaseMutex();
        }

        public void Update(TDto dtoObj)
        {
            var t = Mapper.Map<TDto, T>(dtoObj);
            Repository.Update(t);
            UnitOfWork.Complete();
        }
    }
}