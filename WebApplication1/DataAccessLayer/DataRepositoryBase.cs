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
        where T : ObjectBase, new()
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

                MapProperties(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        private static void MapProperties(T entity, T existingEntity)
        {
            List<PropertyInfo> properties = typeof (T).GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                if (!property.PropertyType.IsAbstract)
                {
                    property.SetValue(existingEntity, property.GetValue(entity));
                }
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

    }
}
