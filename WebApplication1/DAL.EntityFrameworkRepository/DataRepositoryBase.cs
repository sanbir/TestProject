using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using DAL.Contracts;
using Shared.Exceptions;
using Shared.Models;

namespace DAL.EntityFrameworkRepository
{
    public class DataRepositoryBase<TEntity> : IDataRepository<TEntity>
        where TEntity : EntityBase, new()
    {
        public TEntity Add(TEntity entity)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                using (var entityContext = new BiryukovTestDbContext())
                {
                    TEntity addedEntity = entityContext.Set<TEntity>().Add(entity);
                    entityContext.SaveChanges();
                    return addedEntity;
                }
            });
        }

        public void Remove(TEntity entity)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                using (var entityContext = new BiryukovTestDbContext())
                {
                    entityContext.Entry<TEntity>(entity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                }
            });
        }

        public void Remove(int id)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                using (var entityContext = new BiryukovTestDbContext())
                {
                    TEntity entity = GetEntity(entityContext, id);
                    entityContext.Entry<TEntity>(entity).State = EntityState.Deleted;
                    entityContext.SaveChanges();
                }
            });
        }

        public TEntity Update(TEntity entity)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                using (var entityContext = new BiryukovTestDbContext())
                {
                    TEntity existingEntity = GetEntity(entityContext, entity.Id);

                    MapProperties(entity, existingEntity);

                    entityContext.SaveChanges();
                    return existingEntity;
                }
            });
        }

        private static void MapProperties(TEntity entity, TEntity existingEntity)
        {
            List<PropertyInfo> properties = typeof(TEntity).GetProperties().ToList();
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
            return ExecuteExceptionHandledOperation(() =>
            {
                using (var entityContext = new BiryukovTestDbContext())
                    return (from e in entityContext.Set<TEntity>()
                        select e).ToArray().ToList();
            });
        }

        public TEntity Get(int id)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                using (var entityContext = new BiryukovTestDbContext())
                    return GetEntity(entityContext, id);
            });
        }

        private TEntity GetEntity(BiryukovTestDbContext entityContext, int id)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                var entity = (from e in entityContext.Set<TEntity>()
                              where e.Id == id
                              select e).FirstOrDefault();

                return entity;
            });
        }

        protected T ExecuteExceptionHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (Exception ex)
            {
                // TODO: Logger.Error(ex);
                throw new DataAccessException();
            }
        }

        protected void ExecuteExceptionHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (Exception ex)
            {
                // TODO: Logger.Error(ex);
                throw new DataAccessException();
            }
        }

    }
}
