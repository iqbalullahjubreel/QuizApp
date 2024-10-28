using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

public class AdminManager
{
    static List<Admin> admins = new List<Admin>();

    private static string ConnectionStringWithoutDB = "server=localhost;User=root;password=oladapo";
    private static string ConnectionString = "server=localhost;User=root;database=AdminDataBase;password=oladapo";
    public static void CreateAdminDB()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionStringWithoutDB))
        {
            connection.Open();
            string query = "Create Database if not exists AdminDataBase";

            MySqlCommand command = new MySqlCommand(query, connection);

            var execute = command.ExecuteNonQuery();
            if (execute > 0)
            {
                Console.WriteLine("Admin Database Created Successfully.");
            }
            else
            {
                Console.WriteLine("Unable To Create Database.");
            }
        }
    }
    public static void CreateAdminTable()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string querys = "create table if not exists AdminDataBase.Admin(id int primary Key not null auto_increment, Name varchar(255) Not Null, Passwords int(23),  Email varchar(200) not null unique);";
            MySqlCommand command = new MySqlCommand(querys, connection);
            var execute = command.ExecuteNonQuery();
            if (execute == 0)
            {
                Console.WriteLine("Table Created Successfully.");
            }
            else
            {
                Console.WriteLine("Unable To Create Table.");
            }
        }
    }
    public static void CreateAdmin(Admin admin)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your email Address");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
            var registerUser = new Admin(name, email, password);
            admins.Add(registerUser);


            MySqlCommand insert = new MySqlCommand($"insert into Admin(name, email, passwords) values('{admin.Name = name}','{admin.Email = email}','{admin.Password = password}');", connection);

            var execute = insert.ExecuteNonQuery();

            if (execute == 0)
            {
                Console.WriteLine("Admin Created Successfully.");
            }
            else
            {
                Console.WriteLine("Unable To Create Admin.");
            }

        }
    }

    public static Admin LoginAdmin(string email, int password)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string selectquery = $"SELECT id, name, email FROM AdminDataBase.Admin WHERE Email = '{email}' AND passwords = '{password}' ";
            using (MySqlCommand command = new MySqlCommand(selectquery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Admin admin = new Admin();
                        admin.Id = reader.GetInt32(0);
                        admin.Name = reader.GetString(1);
                        admin.Email = reader.GetString(2);
                        Console.WriteLine($"Admin loggedin sucessfull {admin.Name}:");
                        return admin;
                    }
                }
            }
        }
        return null;
    }
}