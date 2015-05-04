using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Data.Contracts;
using Data.Models;

namespace DataAccessLayer
{
    public class DataRepositoryBase<T> : IDataRepository<T>
        where T : EntityBase, new()
    {
        public T Add(T entity)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                T addedEntity = entityContext.Set<T>().Add(entity);
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
                T existingEntity = GetEntity(entityContext, entity.Id);

                PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        public IEnumerable<T> Get()
        {
            using (var entityContext = new BiryukovTestDbContext())
                return (from e in entityContext.Set<T>()
                    select e).ToArray().ToList();
        }

        public T Get(int id)
        {
            using (var entityContext = new BiryukovTestDbContext())
                return GetEntity(entityContext, id);
        }

        private T GetEntity(BiryukovTestDbContext entityContext, int id)
        {
            var entity = (from e in entityContext.Set<T>()
                          where e.Id == id
                          select e).FirstOrDefault();

            return entity;
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
