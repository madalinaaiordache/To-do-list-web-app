using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable CS8618 

namespace Todolist.Dtos
{
    public class TaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime DueDate { get; set; }
        // [Required]
        // public Category Category { get; set; } 
        [Required]
        // public Priority Priority { get; set; } 
        public string CategoryID { get; set; } 
        [Required]
        public string PriorityID { get; set; } 
    }
}


#pragma warning restore CS8618 
