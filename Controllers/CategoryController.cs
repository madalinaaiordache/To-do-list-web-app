// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Todolist.Interfaces;
using Todolist.Data;
using Todolist.Dtos;
// using Todolist.Models;

using Microsoft.AspNetCore.Mvc.Formatters;
using AutoMapper;
using System.Data.Entity.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApiDbContext context;
        private readonly IMapper mapper;
        public CategoryController(ICategoryRepository categoryRepository, ITaskRepository taskRepository, ApiDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _taskRepository = taskRepository;
            this.context = context;
            this.mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [Produces("application/json")] // Adăugăm atributul Produces pentru a specifica tipul media JSON
        public async Task<IActionResult> GetCategories()
        {

            // var userEmail = User.Claims.ElementAt(0).Value;
            var userEmail = User.Claims.Any() ? User.Claims.ElementAt(0).Value : string.Empty;
            if (string.IsNullOrEmpty(userEmail))
            {
                // User is not authenticated or authorized
                Console.WriteLine("User not authenticated or authorized.");
                return BadRequest("User is not authenticated or authorized.");
            }

            // Retrieve the user by email
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null)
            {   
                var categories = _categoryRepository.GetCategories();
                var tasks = _taskRepository.GetTasks();
            
                foreach (var category in categories)
                {
                    category.Tasks = tasks.Where(task => task.CategoryID == category.CategoryID && task.AppUserId == user.Id).ToList();
                }

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(categories);
            }
            else
            {
                // User not found
                return BadRequest("User not found.");
            }


        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            try
            {
                
                // var userEmail = User.Claims.ElementAt(0).Value;
                var userEmail = User.Claims.Any() ? User.Claims.ElementAt(0).Value : string.Empty;

                if (string.IsNullOrEmpty(userEmail))
                {
                    // User is not authenticated or authorized
                    Console.WriteLine("User not authenticated or authorized.");
                    return BadRequest("User is not authenticated or authorized.");
                }

                // Retrieve the user by email
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user != null)
                {   
                    var category = _categoryRepository.GetCategoryById(id);
                    category.Tasks = _taskRepository.GetTasks().Where(task => task.CategoryID == category.CategoryID && task.AppUserId == user.Id).ToList();
                    if (category == null)
                    {
                        return NotFound();
                    }
                    return Ok(category);
                }
                else
                {
                    // User not found
                    return BadRequest("User not found.");
                }
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
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDto categoryDto)
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

               _categoryRepository.AddCategory(new Category
                    {
                        CategoryID = Guid.NewGuid().ToString(),
                        Name = categoryDto.Name,
                    }
                );
                return NoContent();
   
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryDto categoryDto)
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

                var existingCategory = _categoryRepository.GetCategoryById(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                // Update properties of the existing category entity
                existingCategory.Name = categoryDto.Name;
                // Update other properties as needed

                _categoryRepository.UpdateCategory(existingCategory);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCategory(string id)
        {
            try
            {   
                if (!User.IsInRole("Admin"))
                {
                    // Dacă utilizatorul nu are rolul "admin", returnați un cod de stare 403 (Forbidden)
                    return Unauthorized();
                }
                var existingCategory = _categoryRepository.GetCategoryById(id);
                if (existingCategory == null)
                {
                    return NotFound();
                }
                _categoryRepository.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }
    }

    internal class Logger
    {
        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole(); // Add console logging
            // Add other logging providers as needed
        });

        private static readonly ILogger<Logger> _logger = _loggerFactory.CreateLogger<Logger>();

        internal static void LogError(DbUpdateException ex, string message)
        {
            _logger.LogError(ex, message);
        }

        internal static void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, message);
        }
    }
}