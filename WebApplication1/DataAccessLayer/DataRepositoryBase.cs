using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Data.Contracts;
using Data.Models;

namespace DataAccessLayer
{
    public class DataRepositoryBase<TEntity> : IDataRepository<TEntity>
        where TEntity : EntityBase, new()
    {
        public TEntity Add(TEntity entity)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                try
                {
                    TEntity addedEntity = entityContext.Set<TEntity>().Add(entity);
                    entityContext.SaveChanges();
                    return addedEntity;
                }
                catch (RetryLimitExceededException)
                {
                    // TODO: custom exception needed
                    throw new Exception();
                }
            }
        }

        public void Remove(TEntity entity)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                entityContext.Entry<TEntity>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                TEntity entity = GetEntity(entityContext, id);
                entityContext.Entry<TEntity>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                TEntity existingEntity = GetEntity(entityContext, entity.Id);

                MapProperties(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        private static void MapProperties(TEntity entity, TEntity existingEntity)
        {
            List<PropertyInfo> properties = typeof (TEntity).GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                if (!property.GetGetMethod().IsVirtual)
                {
                    property.SetValue(existingEntity, property.GetValue(entity));
                }
            }
        }

        public IEnumerable<TEntity> Get()
        {
            using (var entityContext = new BiryukovTestDbContext())
                return (from e in entityContext.Set<TEntity>()
                    select e).ToArray().ToList();
        }

        public TEntity Get(int id)
        {
            using (var entityContext = new BiryukovTestDbContext())
                return GetEntity(entityContext, id);
        }

        private TEntity GetEntity(BiryukovTestDbContext entityContext, int id)
        {
            var entity = (from e in entityContext.Set<TEntity>()
                          where e.Id == id
                          select e).FirstOrDefault();

            return entity;
        }

    }
}
