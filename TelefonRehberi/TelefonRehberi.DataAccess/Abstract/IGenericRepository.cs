using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TelefonRehberi.DataAccess.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {

        Task<List<TEntity>> GetAll();

        Task<TEntity> Get(int id);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Delete(int id);
    }
}
