namespace AcademicSystem.Menus;

public class LoginMenu : Menu
{
    private bool loop = true;
    public static Dictionary<string, Action> Actions = new Dictionary<string, System.Action>();
    public override void Open(int iD)
    {
        Actions["2"] = LoopFalse;
        loop = true;
        while (loop)
        {
            Console.WriteLine("[Login]");
            Console.WriteLine("Username:");
            var username = Console.ReadLine();

            Console.WriteLine("Password:");
            var password = Console.ReadLine();

            Console.Clear();

            Login login = new Login(username, password);
            if (login.userFound)
            {
                //Console.WriteLine(login.userFound);
                //Console.WriteLine(login.foundUsersID);
                //Console.WriteLine(login.admin);
                //Console.WriteLine(login.teacher);
                //Console.WriteLine(login.student);
                //Console.ReadKey();
                loop = false;
                if (login.admin)
                {
                    Console.Clear();
                    new AdminMenu().Open(login.foundUsersID);
                    //ConnectToAdmin connectToAdmin = new ConnectToAdmin(login.foundUsersID);

                }
                else if (login.teacher)
                {
                    Console.Clear();
                    new TeacherMenu().Open(login.foundUsersID);
                    //TeacherMenu();
                    
                }
                else if (login.student)
                {
                    Console.Clear();
                    new StudentMenu().Open(login.foundUsersID);
                    //StudentMenu();
                    
                }
            }
            else
            {
                bool loop1 = true;
                while (loop1)
                {
                    Console.WriteLine("User was not found, try again?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No (exit)");
                    string selection = Console.ReadLine() ?? string.Empty;
                    if (selection == "2")
                    {
                        Console.Clear();
                        Actions[selection]();
                        loop1 = false;
                    }
                    else if (selection == "1")
                    {
                        loop1 = false;
                    }
                    else if (selection != "1" && selection != "2")
                    {
                        Console.Clear();
                        Console.WriteLine("Please select one of the options given.");
                        Console.ReadKey();
                        Console.Clear();
                    }

                    /*switch (selection)
                    {
                        case 2:
                            loop = false;
                            break;
                    }*/

                    Console.Clear();
                }
            }
        }
    }
    private void LoopFalse()
    {
        loop = false;
    }
}