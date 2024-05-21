using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace Todolist.Dtos
{
        public class PriorityDto
    {
        [Required]
        public int Level { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [JsonIgnore] // Exclude tasks property from serialization
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }

}