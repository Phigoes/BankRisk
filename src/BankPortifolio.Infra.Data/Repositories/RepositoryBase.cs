﻿using BankPortifolio.Domain.Entities;
using BankPortifolio.Domain.Interfaces.Repository;
using BankPortifolio.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BankPortifolio.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly Context context;

        public RepositoryBase(Context context) : base()
        {
            this.context = context;
        }

        public void Update(TEntity entity)
        {
            context.InitTransaction();
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SendChanges();
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                context.InitTransaction();
                context.Set<TEntity>().Remove(entity);
                context.SendChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            context.InitTransaction();
            context.Set<TEntity>().Remove(entity);
            context.SendChanges();
        }

        public int Insert(TEntity entity)
        {
            context.InitTransaction();
            var id = context.Set<TEntity>().Add(entity).Entity.Id;
            context.SendChanges();
            return id;
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }
    }
}
