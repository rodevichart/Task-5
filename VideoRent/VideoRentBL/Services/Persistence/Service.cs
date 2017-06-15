using System;
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
    public class Service<T, TDto> : IService<T, TDto>
        where T : class
        where TDto : class
    {
        protected readonly IRepository<T> Repository;
        private readonly IUnitOfWork _unitOfWork;
        private static readonly Mutex Mutex = new Mutex();
        public Service(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            Repository = repository;
            _unitOfWork = unitOfWork;
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


        public void Add(TDto dtoObj)
        {
            var dto = Mapper.Map<TDto, T>(dtoObj);
            Mutex.WaitOne();
            Repository.Add(dto);
            _unitOfWork.Complete();
            Mutex.ReleaseMutex();


        }

        public void AddRange(IList<TDto> dtoObjs)
        {
            var dto = Mapper.Map<IList<TDto>, IList<T>>(dtoObjs);
            Mutex.WaitOne();
            Repository.AddRange(dto);
            _unitOfWork.Complete();
            Mutex.ReleaseMutex();

        }


        public void Remove(TDto dtoObj)
        {
            var dto = Mapper.Map<TDto, T>(dtoObj);
            Mutex.WaitOne();
            Repository.Remove(dto);
            _unitOfWork.Complete();
            Mutex.ReleaseMutex();

        }

        public void RemoveRange(IEnumerable<TDto> dtoObjs)
        {
            var dto = Mapper.Map<IEnumerable<TDto>, IEnumerable<T>>(dtoObjs);
            Mutex.WaitOne();
            Repository.RemoveRange(dto);
            _unitOfWork.Complete();
            Mutex.ReleaseMutex();
        }
    }
}