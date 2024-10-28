using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public int Password { get; set; }
    public int AdminPassword { get; set; }
    public string AdminName { get; set; }
    public string AdminEmail { get; set; }



    public User()
    {

    }
    public User(string name, int age, string email, int password)
    {
        Name = name;
        Age = age;
        Email = email;
        Password = password;
    }

    //public override string ToString()
    //{
    //    return $"{Name}\t{Age}\t{Email}";
    //}
}
