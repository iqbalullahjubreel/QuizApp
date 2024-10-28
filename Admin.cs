using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Admin : UserManager
{
    private static List<Admin> admins = new List<Admin>();

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Admin()
    {

    }
    public Admin(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}