using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.Data.UnitOfWork;
using HomeBudget.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeBudget.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return unitOfWork.User.GetAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            User u = unitOfWork.User.FindByID(id);
            if (u != null)
            {
                return Ok(u);
            }
            return NotFound();
        }

        [HttpGet("login")]
        //api/user/?username=johnparker&pincode=1111
        public ActionResult Get([FromQuery]string username, [FromQuery] string pinCode)
        {
            User u = unitOfWork.User.Login(u => u.Username == username && u.PINCode == pinCode);
            if (u != null)
            {
                return Ok(u);
            }
            return NotFound();
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromHeader] int userID,[FromBody] User value)
        {

            try
            {
                unitOfWork.User.Add(value);
                unitOfWork.Commit();
                HttpContext.Response.StatusCode = 201;
            } catch (Exception e)
            {
                HttpContext.Response.StatusCode = 400;
            }
            
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
