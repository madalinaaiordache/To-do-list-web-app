// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Todolist.Interfaces;
using Todolist.Data;
using Todolist.Dtos;
using Microsoft.AspNetCore.Mvc.Formatters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : Controller
    {
        
        private readonly IPriorityRepository _priorityRepository;
        private readonly ApiDbContext context;
        private readonly IMapper mapper;
        public PriorityController(IPriorityRepository priorityRepository, ApiDbContext context, IMapper mapper)
        {
            _priorityRepository = priorityRepository;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Priority>))]
        [Produces("application/json")] // Adăugăm atributul Produces pentru a specifica tipul media JSON
        public IActionResult GetPriorities()
        {
            var priorities = _priorityRepository.GetPriorities();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(priorities);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Priority))]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public IActionResult GetPriorityById(string id)
        {
            try
            {
                var priority = _priorityRepository.GetPriorityById(id);
                if (priority == null)
                {
                    return NotFound();
                }
                return Ok(priority);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePriorityAsync([FromBody] PriorityDto priorityDto)
        {
            try
            {   
                if (!User.IsInRole("Admin"))
                {
                    // Dacă utilizatorul nu are rolul "admin", returnați un cod de stare 403 (Forbidden)
                    return Unauthorized();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                _priorityRepository.AddPriority(new Priority
                    {
                        PriorityID = Guid.NewGuid().ToString(),
                        Level = priorityDto.Level,
                        Description = priorityDto.Description,
                    }
                );
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdatePriority(string id, [FromBody] PriorityDto priorityDto)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    // Dacă utilizatorul nu are rolul "admin", returnați un cod de stare 403 (Forbidden)
                    return Unauthorized();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingPriority = _priorityRepository.GetPriorityById(id);
                if (existingPriority == null)
                {
                    return NotFound();
                }
                // Map the DTO to the entity
                existingPriority.Description = priorityDto.Description;
                existingPriority.Level = priorityDto.Level;

                _priorityRepository.UpdatePriority(existingPriority);

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
        [Authorize(Roles = "Admin")] 
        public IActionResult DeletePriority(string id)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    // Dacă utilizatorul nu are rolul "admin", returnați un cod de stare 403 (Forbidden)
                    return Unauthorized();
                }
                var existingPriority = _priorityRepository.GetPriorityById(id);
                if (existingPriority == null)
                {
                    return NotFound();
                }
                _priorityRepository.DeletePriority(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }
    }

}