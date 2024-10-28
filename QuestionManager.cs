//using Adoproject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

public class QuestionManager
{
    private static List<Question> questio = new List<Question>();

    private static string ConnectionStringWithoutDB = "server=localhost;User=root;password=oladapo";
    private static string ConnectionString = "server=localhost;User=root;database=QuestionDataBase;password=oladapo";
    public static void CreateQuestionDB()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionStringWithoutDB))
        {
            connection.Open();
            string query = "Create Database if not exists QuestionDataBase";

            MySqlCommand command = new MySqlCommand(query, connection);

            var execute = command.ExecuteNonQuery();

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

    public static void CreateQuestionTable()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string query = "create table if not exists QuestionDataBase.Question(id int primary Key not null auto_increment, Type varchar(255) Not Null, Answer Varchar(255),  Questions varchar(200) not null unique);";

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
    public static void CreateQuestion(Question question)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            Console.WriteLine("how many questions do you want to Add");
            int number = int.Parse(Console.ReadLine());
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("Choose the type of question you want to add");
                string type = Console.ReadLine();
                Console.WriteLine("Enter the question");
                string questions = Console.ReadLine();
                Console.WriteLine("Enter the answer for the question");
                string answer = Console.ReadLine();
                var registerUser = new Question(type, questions, answer);
                questio.Add(registerUser);


                MySqlCommand insert = new MySqlCommand($"insert into Question(type, Answer, questions) values('{question.Type = type}', '{question.Answer = answer}','{question.Questions = questions}');", connection);

                var execute = insert.ExecuteNonQuery();

                if (execute == 0)
                {
                    Console.WriteLine("Question Created Successfully.");
                }
                else
                {
                    Console.WriteLine("Unable To Create Question.");
                }

            }
        }
    }
    public static void UpdateQuestions(Question question)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            int count = 0;

            Console.WriteLine("how many questions do you want to update");
            int number = int.Parse(Console.ReadLine());
            for (int i = 0; i < number; i++)
            {
                Console.WriteLine("enter the question type");
                string questiontype = Console.ReadLine();
                Console.WriteLine("enter the new question");
                string newquestion = Console.ReadLine();
                Console.WriteLine("enter the new answer for the question");
                string newanswer = Console.ReadLine();
                Console.WriteLine("Enter the question id");
                int id = int.Parse(Console.ReadLine());
                var registerUser = new Question(questiontype, newquestion, newanswer);
                questio.Add(registerUser);

                question.Type = questiontype;
                question.Questions = newquestion;
                question.Answer = newanswer;
                count++;

                string Query = $"Update QuestionDataBase.Question SET type = '{question.Type}',questions ='{question.Questions}',answer ='{question.Answer}' where id = {id}";
                MySqlCommand command = new MySqlCommand(Query, connection);
                var execute = command.ExecuteNonQuery();
                if (execute > 0)
                {
                    Console.WriteLine("question table updated sucessfully");
                }
                else
                {
                    Console.WriteLine("unable update question table");
                }
            }
        }
    }
    public static void DeleteQuestions()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            Console.WriteLine("Enter the question id that you want to delete");
            int id = int.Parse(Console.ReadLine());
            string query = $"DELETE from QuestionDataBase.Question  where id = {id};";
            MySqlCommand command = new MySqlCommand(query, connection);
            var execute = command.ExecuteNonQuery();
            if (execute > 0)
            {
                Console.WriteLine("deleted Sucessfully");
            }
            else
            {
                Console.WriteLine("Unable to delete");
            }
        }

    }
    public static void GetAllQuestions()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string selectquery = "SELECT type, questions, answer FROM QuestionDataBase.Question";
            using (MySqlCommand command = new MySqlCommand(selectquery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Question in database:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Question TYPE {reader["type"]}, QUESTION: {reader["questions"]}, ANSWER: {reader["answer"]} \n");
                    }
                }
            }
        }
    }
    public static void StartQuiz()
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string selectquery = $"SELECT type, questions, answer FROM QuestionDataBase.Question";

            using (MySqlCommand command = new MySqlCommand(selectquery, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Question question = new Question();
                    int count = 0;
                    int Count = 0;
                    question.Count = count;
                    int percentage = question.Count / 10 * 100;

                    Console.WriteLine("these are the courses available now please choose an option");
                    Console.WriteLine("Course 1: maths");
                    Console.WriteLine("Course 2: civic");
                    Console.WriteLine("Course 3: biology");
                    Console.WriteLine("Course 4: english");
                    Console.WriteLine("Course 5: physics");
                    string opts = Console.ReadLine();
                    while (reader.Read())
                    {
                        Question quest = new Question();
                        quest.Type = reader.GetString(0);
                        quest.Questions = reader.GetString(1);
                        quest.Answer = reader.GetString(2);
                        switch (opts)
                        {
                            case "1":
                                string topic = "maths";
                                if (topic == quest.Type)
                                {
                                    count++;
                                    Console.WriteLine($"QUESTION: {quest.Questions}");
                                    string ans = Console.ReadLine();
                                    if (ans == quest.Answer)
                                    {
                                        Console.WriteLine("correct answer");
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect answer");
                                        Count++;
                                        //Console.WriteLine($"the correct answer is {quest.Answer}");
                                    }
                                }
                                break;
                            case "2":
                                string top = "civic";
                                if (top == quest.Type)
                                {
                                    Console.WriteLine($"QUESTION: {quest.Questions}");
                                    string ans = Console.ReadLine();
                                    if (ans == quest.Answer)
                                    {
                                        Console.WriteLine("correct answer");
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect answer");
                                        Count++;
                                        //Console.WriteLine($"the correct answer is {quest.Answer}");
                                    }
                                }
                                break;
                            case "3":
                                string topics = "biology";
                                if (topics == quest.Type)
                                {
                                    Console.WriteLine($"QUESTION: {quest.Questions}");
                                    string ans = Console.ReadLine();
                                    if (ans == quest.Answer)
                                    {
                                        Console.WriteLine("correct answer");
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect answer");
                                        Count++;
                                        //Console.WriteLine($"the correct answer is {quest.Answer}");
                                    }
                                }
                                break;
                            case "4":
                                string topi = "english";
                                if (topi == quest.Type)
                                {
                                    Console.WriteLine($"QUESTION: {quest.Questions}");
                                    string ans = Console.ReadLine();
                                    if (ans == quest.Answer)
                                    {
                                        Console.WriteLine("correct answer");
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect answer");
                                        Count++;
                                        //Console.WriteLine($"the correct answer is {quest.Answer}");
                                    }
                                }
                                break;
                            case "5":
                                string to = "physics";
                                if (to == quest.Type)
                                {
                                    Console.WriteLine($"QUESTION: {quest.Questions}");
                                    string ans = Console.ReadLine();
                                    if (ans == quest.Answer)
                                    {
                                        Console.WriteLine("correct answer");
                                        count++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect answer");
                                        Count++;
                                        //Console.WriteLine($"the correct answer is {quest.Answer}");
                                    }
                                }
                                break;
                        }
                    }
                    Console.WriteLine($"The total Count of correct answers in this Section is {question.Count}");
                    Console.WriteLine($"The total Count of incorrect answers in this Section is {Count}");
                    Console.WriteLine($"Total Percentage is {percentage}%");
                    if (percentage > 60)
                    {
                        Console.WriteLine($"Congratulations you passed the cut-off mark");
                    }
                    else
                    {
                        Console.WriteLine($"You did not pass the cut-off mark");
                    }

                }
            }
        }
    }
}
