using System.ComponentModel.DataAnnotations.Schema;

public class TaskCategory
{
    [Column("taskcategoryid")]
    public string TaskCategoryID { get; set; }
    [Column("taskid")]
    public string TaskID { get; set; }
    // [Column("task")]
    public Task Task { get; set; }
    [Column("categoryid")]
    public string CategoryID { get; set; }
    // [Column("category")]
    public Category Category { get; set; }
}
