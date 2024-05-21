    using System.ComponentModel.DataAnnotations.Schema;

    [Table("category")]
    public class Category
{
    
    [Column("categoryid")]
    public string CategoryID { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("tasks")]
    public List<Task> Tasks { get; set; } = new List<Task>();
}