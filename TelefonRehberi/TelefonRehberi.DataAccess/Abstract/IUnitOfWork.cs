using System;
using System.Collections.Generic;
using System.Text;
using TelefonRehberi.Entities;

namespace TelefonRehberi.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        IGenericRepository<Person> PersonRepository { get; }

        IGenericRepository<User> UserRepository { get; }

        MyDbContext DbContext { get; set; }

    }
}
