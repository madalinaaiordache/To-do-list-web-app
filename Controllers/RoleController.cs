// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Todolist.Interfaces;
using Todolist.Data;
// using Todolist.Models;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        
        private readonly IRoleRepository _roleRepository;
        private readonly ApiDbContext context;
        public RoleController(IRoleRepository roleRepository, ApiDbContext context)
        {
            _roleRepository = roleRepository;
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Role>))]
        [Produces("application/json")] // Adăugăm atributul Produces pentru a specifica tipul media JSON
        public IActionResult GetRoles()
        {
            var roles = _roleRepository.GetRoles();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(roles);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Role))]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetRoleById(string id)
        {
            try
            {
                var role = _roleRepository.GetRoleById(id);
                if (role == null)
                {
                    return NotFound();
                }
                return Ok(role);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }

        // [HttpPost]
        // [ProducesResponseType(201)]
        // [ProducesResponseType(400)]
        // public IActionResult AddRole([FromBody] Role role)
        // {
        //     try
        //     {
        //         if (!ModelState.IsValid)
        //         {
        //             return BadRequest(ModelState);
        //         }
        //         _roleRepository.AddRole(role);
        //         return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleID }, role);
        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(500, new { error = "An internal server error occurred." });
        //     }
        // }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult AddRole([FromBody] RoleDto roleDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Create a new Role object using the RoleDto data
                Role role = new Role(roleDto.Name, roleDto.UserID);

                _roleRepository.AddRole(role);

                // Optionally, you can map the newly added role back to a RoleDto
                RoleDto addedRoleDto = new RoleDto(role);

                return CreatedAtAction(nameof(GetRoleById), new { id = addedRoleDto.RoleID }, addedRoleDto);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }



        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRole(string id, [FromBody] Role role)
        {
            try
            {
                if (id != role.RoleID)
                {
                    return BadRequest();
                }
                _roleRepository.UpdateRole(role);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRole(string id)
        {
            try
            {
                var existingRole = _roleRepository.GetRoleById(id);
                if (existingRole == null)
                {
                    return NotFound();
                }
                _roleRepository.DeleteRole(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }
    }
}