using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TelefonRehberi.Business.Abstract;
using TelefonRehberi.Entities;

namespace TelefonRehberi.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {

        public IPersonService _personService;
        private readonly ILogger<PersonsController> _logger;


        public PersonsController(IPersonService personService, ILogger<PersonsController> logger)
        {
            _personService = personService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Persons = await _personService.GetAll();

                if (Persons != null)
                {


                    return Ok(Persons);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError("ERROR : "+e.Message);
                return BadRequest(e.Message);
            }
            
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var Person = await _personService.Get(id);

                if (Person != null)
                {


                    return Ok(Person);
                }
                return NotFound();
            }
            catch(Exception e)
            {

                _logger.LogError("ERROR : " + e.Message);
                return BadRequest(e.Message);
            }
            
            

        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            try
            {
                Person createdPerson = await _personService.Create(person);
                return CreatedAtAction("GET", new { id = createdPerson.Id }, createdPerson);
            }
            catch (Exception e)
            {
                _logger.LogError("HATA : " + e.Message);
                return BadRequest(e.Message);
            }


        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            try
            {
                var updatedPerson = await _personService.Get(person.Id);

                if (updatedPerson != null)
                {

                    return Ok(await _personService.Update(person));
                }
                return NotFound();
            }
            catch(Exception e)
            {
                _logger.LogError("HATA : " + e.Message);
                return BadRequest(e.Message);
            }
            

            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedPerson = await _personService.Get(id);

                if (deletedPerson != null)
                {
                    await _personService.Delete(id);
                    return Ok();
                }
               
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError("ERROR : " + e.Message);
                return BadRequest(e.Message);
            }


        }


    }
}
