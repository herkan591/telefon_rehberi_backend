using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberi.DataAccess.Abstract;

namespace TelefonRehberi.DataAccess.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        MyDbContext _context;

        public GenericRepository(MyDbContext context)
        {
            _context = context;

            
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            EntityEntry<TEntity> created=_context.Add<TEntity>(entity);
            await _context.SaveChangesAsync();

            return created.Entity;
        }

        public async Task Delete(int id)
        {

            TEntity deleted = await Get(id);
            _context.Remove<TEntity>(deleted);

            await _context.SaveChangesAsync();

        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Update(TEntity entity)
        {

            _context.ChangeTracker.Clear();

            EntityEntry<TEntity> updated=  _context.Update<TEntity>(entity);
            await _context.SaveChangesAsync();
            return updated.Entity;
        }

    }
}
