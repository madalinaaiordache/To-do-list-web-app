using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618 

    public class User
{
    [Column("userid")] //Trying to map table column to variable 
    public string UserID { get; set; }
    [Column("username")]
    public string Username { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("passwordhash")]
    public string PasswordHash { get; set; }
    [Column("roleid")]
    public string RoleID { get; set; }
    [Column("role")]
    public Role Role { get; set; }
    [Column("tasks")]
    public List<Task> Tasks { get; set; } = [];
}
#pragma warning restore CS8618 

/*
Pentru a configura relațiile între aceste entități în baza de date, putem urmări următoarele scheme:

One-to-Many Relationship:

Relația dintre User și Task: Un utilizator poate avea mai multe sarcini, dar o sarcină aparține unui singur utilizator.
Relația dintre Role și User: Un rol poate fi asociat cu mai mulți utilizatori, dar un utilizator are un singur rol.

Many-to-Many Relationship:

Relația dintre Task și Category: O sarcină poate fi asociată cu mai multe categorii și, în același timp, o categorie poate fi asociată cu mai multe sarcini.
Relația dintre Task și Priority: O sarcină poate avea o prioritate, dar o prioritate poate fi asociată cu mai multe sarcini.
*/