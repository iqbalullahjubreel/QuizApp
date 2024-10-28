using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



public class UserManager
{
    private static List<User> users = new List<User>();

    private static string ConnectionStringWithoutDB = "server=localhost;User=root;password=oladapo";
    private static string ConnectionString = "server=localhost;User=root;database=UserDataBase;password=oladapo";
    public static void CreateUserDB()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionStringWithoutDB))
        {
            connection.Open();
            string query = "Create Database if not exists UserDataBase";

            MySqlCommand command = new MySqlCommand(query, connection);
            var execute =
            command.ExecuteNonQuery();


            if (execute > 0)
            {
                Console.WriteLine("Database Created Successfully.");
            }
            else
            {
                Console.WriteLine("Unable To Create Database.");
            }
        }
    }

    public static void CreateUserTable()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string query = "create table if not exists UserDataBase.User(id int primary Key not null auto_increment, Name varchar(255) Not Null, Age int(23), Passwords int(23), Email varchar(200) not null unique, AdminName varchar(255) Not Null, AdminPasswords int(23), AdminEmail varchar(200) not null unique);";

            MySqlCommand command = new MySqlCommand(query, connection);
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

    public static void CreateUser(User user)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your age");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your email Address");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your password");
            int password = int.Parse(Console.ReadLine());
            var registerUser = new User(name, age, email, password);
            users.Add(registerUser);


            MySqlCommand insert = new MySqlCommand($"insert into User(name,age,passwords, email,AdminName,AdminEmail ,AdminPasswords) values('{user.Name = name}','{user.Age = age} ','{user.Password = password}','{user.Email = email}','{user.AdminEmail = "inamcojubreel@gmail.com"}','{user.AdminName = "inamurrahman jubreel"}','{user.AdminPassword = 11234}');", connection);
            //

            //MySqlCommand = new MySqlCommand (insert, connection);
            var execute = insert.ExecuteNonQuery();

            if (execute == 0)
            {
                Console.WriteLine("User Created Successfully.");
            }
            else
            {
                Console.WriteLine("Unable To Create User.");
            }
        }
    }
    public static void UpdateUser(User user)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            Console.WriteLine("Enter your new Name");
            string newname = Console.ReadLine();
            Console.WriteLine("Enter your new Email address");
            string newEmail = Console.ReadLine();
            Console.WriteLine("Enter your password ");
            int newPassword = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your id");
            int id = int.Parse(Console.ReadLine());

            user.Name = newname;
            user.Email = newEmail;
            user.Password = newPassword;

            string Query = $"Update UserDataBase.User SET name = '{user.Name}',email ='{user.Email}',passwords ='{user.Password}' where id = {id}";
            MySqlCommand command = new MySqlCommand(Query, connection);
            var execute = command.ExecuteNonQuery();
            if (execute > 0)
            {
                Console.WriteLine("User table updated sucessfully");
            }
            else
            {
                Console.WriteLine("unable update User table");
            }
        }
    }
    public static void DeleteUser()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            Console.WriteLine("Enter the id of the user you want to delete");
            int id = int.Parse(Console.ReadLine());
            string query = $"DELETE from UserDataBase.User  where id = {id};";
            MySqlCommand command = new MySqlCommand(query, connection);
            var execute = command.ExecuteNonQuery();
            if (execute > 0)
            {
                Console.WriteLine("deleted Sucessfully");
            }
            else
            {
                Console.WriteLine("Unable to update");
            }
        }

    }

    public static User LoginUser(string email, int password)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string selectquery = $"SELECT id, name, email,AdminName ,AdminEmail FROM UserDataBase.User WHERE email = '{email}' AND passwords = {password} OR adminname = 'inamcojubreel@gmail.com' AND adminpasswords = 11234 ";
            using (MySqlCommand command = new MySqlCommand(selectquery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        User use = new User();
                        use.Id = reader.GetInt32(0);
                        use.Name = reader.GetString(1);
                        use.Email = reader.GetString(2);
                        use.AdminEmail = reader.GetString(3);
                        use.AdminName = reader.GetString(4);

                        if (use.Email == email)
                        {
                            Console.WriteLine($"User sucessfully logged in {use.Name}");
                        }
                        else if (use.AdminEmail == email)
                        {
                            Console.WriteLine($"User sucessfully logged in {use.AdminName}");
                        }
                        return use;
                    }
                }
            }
        }
        return null;
    }

    public static void GetAllUser()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string selectquery = "SELECT id, name, age, email FROM UserDataBase.User";
            using (MySqlCommand command = new MySqlCommand(selectquery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("User in database:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"NAME {reader["name"]}, Age: {reader["age"]}, EMAIL: {reader["email"]}\n ");
                    }
                }
            }
        }
    }
    public static User GetUser(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string selectquery = $"SELECT id, name, age, email FROM User WHERE id = {id}";

            using (MySqlCommand command = new MySqlCommand(selectquery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("User:");
                    if (!reader.Read())
                    {
                        Console.WriteLine("User not found");
                    }
                    else
                    {
                        Console.WriteLine($"ID {reader["name"]}, AGE: {reader["age"]}, EMAIL: {reader["email"]} ");
                    }
                }
            }
        }
        return null;
    }
}