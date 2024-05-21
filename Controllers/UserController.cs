// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Todolist.Interfaces;
using Todolist.Data;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
// using Todolist.Models;

namespace Todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        
        private readonly IUserRepository _userRepository;
        private readonly ApiDbContext context;
        public UserController(IUserRepository userRepository, ApiDbContext context)
        {
            _userRepository = userRepository;
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [Produces("application/json")] // Add produced media type by endpoint
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetUserById(string id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(string id, [FromBody] AppUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _userRepository.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")] // Protejăm acțiunea DeleteUser, astfel încât doar utilizatorii cu rolul "admin" să aibă acces
        public IActionResult DeleteUser(string id)
        {
            // Verificați dacă utilizatorul autentificat are rolul "admin"
            if (!User.IsInRole("Admin"))
            {
                // Dacă utilizatorul nu are rolul "admin", returnați un cod de stare 403 (Forbidden)
                return Unauthorized();
            }
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(id);
            return NoContent();
        }
    }

}