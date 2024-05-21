using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public class RoleDto
{
    public string RoleID { get; set; }
    public string Name { get; set; }
    public string UserID { get; set; }
    
    public RoleDto(Role role)
    {
        RoleID = role.RoleID;
        Name = role.Name;
        UserID = role.UserID;
    }
}
