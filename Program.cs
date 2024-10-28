// See https://aka.ms/new-console-template for more information

User users = new User();
Question questions = new Question();
Admin admin = new Admin();
List<Admin> admins = new List<Admin>();
bool running = true;

while (running)
{
    Console.WriteLine("Welcome to Quiz Driller App \n press any key to continue \n");
    Console.ReadKey();
    Console.WriteLine();
    Console.WriteLine("1. Register");
    Console.WriteLine("2.login");
    Console.WriteLine("3. Exit");
    Console.WriteLine("choose an option");
    string opt = Console.ReadLine();
    switch (opt)
    {
        case "1":
            UserManager.CreateUser(users);
            break;
        case "2":
            Console.WriteLine("enter your Email");
            string emails = Console.ReadLine();
            Console.WriteLine("enter password");
            int passwords = int.Parse(Console.ReadLine());
            var loggedInUser = UserManager.LoginUser(emails, passwords);
            bool loggedIn = true;
            if (loggedInUser != null)
            {
                if (loggedInUser.AdminEmail == emails)
                {
                    while (loggedIn)
                    {
                        Console.WriteLine("1: ADD QUESTION");
                        Console.WriteLine("2: Update question");
                        Console.WriteLine("3: Check question");
                        Console.WriteLine("4: Delete question");
                        Console.WriteLine("5. Check users in your application");
                        Console.WriteLine("6: Delete User");
                        Console.WriteLine("7: Exit");
                        Console.Write("Choose an option: ");
                        string choices = Console.ReadLine();
                        switch (choices)
                        {
                            case "1":
                                QuestionManager.CreateQuestion(questions);
                                break;
                            case "2":
                                QuestionManager.UpdateQuestions(questions);
                                break;
                            case "3":
                                QuestionManager.GetAllQuestions();
                                break;
                            case "4":
                                QuestionManager.DeleteQuestions();
                                break;
                            case "5":
                                UserManager.GetAllUser();
                                break;
                            case "6":
                                UserManager.DeleteUser();
                                break;
                            case "7":
                                loggedIn = false;
                                break;
                        }
                    }
                }
                else if (loggedInUser.Email == emails)
                {
                    while (loggedIn)
                    {
                        Console.WriteLine("1. Start quiz");
                        Console.WriteLine("2. Update your information");
                        Console.WriteLine("3. Check your information");
                        Console.WriteLine("4. Exit");
                        Console.Write("Choose an option: ");
                        string loginChoice = Console.ReadLine();
                        switch (loginChoice)
                        {
                            case "1":
                                QuestionManager.StartQuiz();
                                break;
                            case "2":
                                UserManager.UpdateUser(users);
                                break;
                            case "3":
                                Console.WriteLine("enter your id");
                                int iden = int.Parse(Console.ReadLine());
                                UserManager.GetUser(iden);
                                break;
                            case "4":
                                loggedIn = false;
                                break;
                        }
                    }
                }
            }

            break;
        case "5":
            running = false;
            break;
    }
}
