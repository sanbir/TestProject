using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Data.Contracts;
using Data.Models;

namespace DataAccessLayer
{
    public abstract class DataRepositoryBase<T> : IDataRepository<T>
        where T : ObjectBase, new()
    {
        protected abstract T AddEntity(BiryukovTestDbContext entityContext, T entity);

        protected abstract T UpdateEntity(BiryukovTestDbContext entityContext, T entity);

        protected abstract IEnumerable<T> GetEntities(BiryukovTestDbContext entityContext);

        protected abstract T GetEntity(BiryukovTestDbContext entityContext, int id);

        public T Add(T entity)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public void Remove(T entity)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public T Update(T entity)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                T existingEntity = UpdateEntity(entityContext, entity);

                PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        public IEnumerable<T> Get()
        {
            using (var entityContext = new BiryukovTestDbContext())
                return (GetEntities(entityContext)).ToArray().ToList();
        }

        public T Get(int id)
        {
            using (var entityContext = new BiryukovTestDbContext())
                return GetEntity(entityContext, id);
        }

        private static void PropertyMap<TSource, TDest>(TSource source, TDest destination)
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
        }
    }
}
