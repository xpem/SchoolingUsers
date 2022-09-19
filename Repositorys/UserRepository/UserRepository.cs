using Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contexts;

namespace Repositories.UserRepository
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        #region property
        private readonly UserContext _applicationDbContext;
        private DbSet<T> entities;
        #endregion

        #region Constructor
        public UserRepository(UserContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            entities = _applicationDbContext.Set<T>();
        }
        #endregion


        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _applicationDbContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return entities.ToList();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Update(entity);
            _applicationDbContext.SaveChanges();
        }
    }
}
