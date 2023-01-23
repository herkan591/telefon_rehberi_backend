using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberi.Entities;

namespace TelefonRehberi.Business.Abstract
{
    public interface IPersonService
    {

        Task<List<Person>> GetAll();

        Task<Person> Get(int id);

        Task<Person> Create(Person Kisi);

        Task<Person> Update(Person Kisi);

        Task Delete(int id);





    }
}
