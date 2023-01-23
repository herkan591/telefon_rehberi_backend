using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelefonRehberi.Business.Abstract;
using TelefonRehberi.Business.ValidationRules;
using TelefonRehberi.DataAccess.Abstract;
using TelefonRehberi.Entities;

namespace TelefonRehberi.Business.Concrete
{
    public class PersonManager : IPersonService
    {

        IUnitOfWork _unitOfWork;
        PersonValidator validator = new PersonValidator();


        public PersonManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }



        public async Task<Person> Create(Person person)
        {

            validator.ValidateAndThrow(person);

            return await _unitOfWork.PersonRepository.Create(person);


        }

        public async Task Delete(int id)
        {

            await _unitOfWork.PersonRepository.Delete(id);
        }

        public async Task<List<Person>> GetAll()
        {
            return await _unitOfWork.PersonRepository.GetAll();
        }

        public async Task<Person> Get(int id)
        {

            return await _unitOfWork.PersonRepository.Get(id);

        }

        public async Task<Person> Update(Person person)
        {
            validator.ValidateAndThrow(person);
            return await _unitOfWork.PersonRepository.Update(person);
        }
    }
}
