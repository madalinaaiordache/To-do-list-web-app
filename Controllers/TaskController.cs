// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Todolist.Interfaces;
using Todolist.Data;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using Todolist.Dtos;
using System.Security.Claims;
using AutoMapper;
using Todolist.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Todolist.Dtos.Account;
using System.IdentityModel.Tokens.Jwt;


namespace Todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiDbContext context;
        private readonly IMapper mapper;
        public TaskController(UserManager<AppUser> userManager, ITaskRepository taskRepository, ApiDbContext context, IMapper mapper, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            this.context = context;
            this.mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Task>))]
        [Produces("application/json")] // Add produced media type by endpoint
        public async Task<IActionResult> GetTasks()
        {
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
                var tasks = _taskRepository.GetTasks().Where( task => task.AppUserId == user.Id);
                return Ok(tasks);
            }
            else
            {
                // User not found
                return BadRequest("User not found.");
            }
        }



        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Task>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> GetTaskById(string id)
        {
            try
            {
                var userEmail = User.Claims.Any() ? User.Claims.ElementAt(0).Value : string.Empty;
                Console.WriteLine("User.email: " + userEmail);
                
                if (string.IsNullOrEmpty(userEmail))
                {
                    // User is not authenticated or authorized
                    Console.WriteLine("User not authenticated or authorized.");
                    return BadRequest("User is not authenticated or authorized.");
                }

                // Retrieve the user by email
                var user =  await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    // User not found
                    return BadRequest("User not found.");
                }

                var task = _taskRepository.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }

                // Check if the task is assigned to the authenticated user
                if (task.AppUserId != user.Id)
                {
                    // Task doesn't belong to the authenticated user
                    return BadRequest("Access denied."); 
                }

                return Ok(task);
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // var userEmail = User.Claims.ElementAt(0).Value;
                var userEmail = User.Claims.Any() ? User.Claims.ElementAt(0).Value : string.Empty;             

                Console.WriteLine("Use.email: " + userEmail);
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
                    var userId = user.Id;

                    var task = new Task
                    {
                        TaskID = Guid.NewGuid().ToString(),
                        Title = taskDto.Title,
                        Description = taskDto.Description,
                        DueDate = taskDto.DueDate,
                        PriorityID = taskDto.PriorityID,
                        CategoryID = taskDto.CategoryID,
                        AppUserId = userId // Set the user ID
                    };

                    //Check if the provided CategoryID corresponds to an existing category
                    var categoryExists =  _categoryRepository.GetCategoryById(taskDto.CategoryID);
                    if (categoryExists != null)
                    {
                        // Category exists
                        Console.WriteLine("Category exists: " + categoryExists.Name);
                    }
                    else
                    {
                        // Category does not exist
                        ModelState.AddModelError("CategoryID", "The specified category does not exist.");
                        return BadRequest(ModelState);
                    }

                    // Check if the task is assigned to the authenticated user
                    
                    // task.Category = category;
                    _taskRepository.AddTask(task);
                    return Created();
                }
                else
                {
                    // User not found
                    return BadRequest("User not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // Return a meaningful error response
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTask(string id, [FromBody] TaskDto taskDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userEmail = User.Claims.Any() ? User.Claims.ElementAt(0).Value : string.Empty;
                if (string.IsNullOrEmpty(userEmail))
                {
                    // User is not authenticated or authorized
                    Console.WriteLine("User not authenticated or authorized.");
                    return BadRequest("User is not authenticated or authorized.");
                }

                // Retrieve the user by email
                var user =  await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    // User not found
                    return BadRequest("User not found.");
                }

                var existingTask = _taskRepository.GetTaskById(id);
                if (existingTask == null)
                {
                    return NotFound();
                }

                existingTask.Title = taskDto.Title;
                existingTask.Description =   taskDto.Description;
                existingTask.DueDate = taskDto.DueDate;
                existingTask.CategoryID = taskDto.CategoryID;
                existingTask.PriorityID = taskDto.PriorityID;


                if (existingTask.AppUserId != user.Id)
                    {
                        // Task doesn't belong to the authenticated user
                        return BadRequest("Access denied."); 
                    }
                _taskRepository.UpdateTask(existingTask);

                return NoContent();
            }
            catch (ArgumentException)
            {
                // Log the exception
                // Return a meaningful error response
                return NotFound();
            }
            catch (Exception)
            {
                // Log the exception
                // Return a meaningful error response
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {   

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userEmail = User.Claims.Any() ? User.Claims.ElementAt(0).Value : string.Empty;
                if (string.IsNullOrEmpty(userEmail))
                {
                    // User is not authenticated or authorized
                    Console.WriteLine("User not authenticated or authorized.");
                    return BadRequest("User is not authenticated or authorized.");
                }

                // Retrieve the user by email
                var user =  await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    // User not found
                    return BadRequest("User not found.");
                }


                var existingTask = _taskRepository.GetTaskById(id);
                if (existingTask == null)
                {
                    return NotFound();
                }

                if (existingTask.AppUserId != user.Id)
                {
                    // Task doesn't belong to the authenticated user
                    return BadRequest("Access denied."); 
                }
                _taskRepository.DeleteTask(id);
                return NoContent();
            }
            catch (Exception)
            {
                // Log the exception
                // Return a meaningful error response
                return StatusCode(500, new { error = "An internal server error occurred." });
            }
        }    
    }

}