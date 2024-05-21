using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.Dtos;

public class UserDto
{
    public string UserID { get; set; } = string.Empty;
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RoleID { get; set; }
    public RoleDto Role { get; set; }
    public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();

#pragma warning disable CS8618 
    public UserDto(User user)
#pragma warning restore CS8618 
    {
        UserID = user.UserID;
        Username = user.Username;
        Email = user.Email;
        PasswordHash = user.PasswordHash;
        RoleID = user.RoleID;
#pragma warning disable CS8601 
        Role = user.Role != null ? new RoleDto(user.Role) : null;
#pragma warning restore CS8601 
        if (user.Tasks != null)
        {
            Tasks = user.Tasks.Select(task => new TaskDto
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate
            }).ToList();
        }
    }
}
