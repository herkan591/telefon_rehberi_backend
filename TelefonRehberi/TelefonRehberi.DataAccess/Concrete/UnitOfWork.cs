using System;
using System.Collections.Generic;
using System.Text;
using TelefonRehberi.DataAccess.Abstract;
using TelefonRehberi.Entities;

namespace TelefonRehberi.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {

        public IGenericRepository<Person> PersonRepository { get; set; }

        public IGenericRepository<User> UserRepository { get; set; }
        public MyDbContext DbContext { get ; set ; }

        public UnitOfWork()
        {
            this.DbContext = new MyDbContext();
            PersonRepository = new GenericRepository<Person>(this.DbContext);
            UserRepository = new GenericRepository<User>(this.DbContext);


        }
    }
}
