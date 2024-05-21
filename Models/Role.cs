    using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618 

    [Table("role")]
    public class Role 

{
    public Role(string Name, string UserID) 
    {
        this.Name = Name;
        this.UserID = UserID;
    }

    [Column("roleid")]
    public string RoleID { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("users")]
    public string UserID { get; set; }
}
#pragma warning restore CS8618 
