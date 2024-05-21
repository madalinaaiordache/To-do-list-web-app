   using System.ComponentModel.DataAnnotations.Schema;


    [Table("task")]
    public class Task
{
    [Column("taskid")]
    public string TaskID { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("duedate")]
    public DateTime DueDate { get; set; }
    // [ForeignKey("AppUser")]
    public string AppUserId { get; set; }
    // [Column("user")]
    // public User User { get; set; }
    [Column("category")]
    public Category Category { get; set; } 
    [Column("categoryid")]
    public string CategoryID { get; set; }
    [Column("priority")]
    public Priority Priority { get; set; }
    [Column("priorityid")]
    public string PriorityID { get; set; }
   
}