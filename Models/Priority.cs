    using System.ComponentModel.DataAnnotations.Schema;

    [Table("priority")]

    public class Priority
{
    [Column("priorityid")]
    public string PriorityID { get; set; }
    [Column("level")]
    public int Level { get; set; }
    [Column("description")]
    public string Description { get; set; }
}