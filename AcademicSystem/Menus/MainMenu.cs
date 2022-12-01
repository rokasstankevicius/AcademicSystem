namespace AcademicSystem.Menus;

public class MainMenu : Menu
{
    private bool _loop = true;
    private static Dictionary<string, Action> _actions = new Dictionary<string, System.Action>();
    
    public override void Open(int iD)
    {
        _actions["1"] = OpenLoginMenu;
        _actions["0"] = LoopFalse;
        while (_loop)
        {
            Console.WriteLine("[Welcome]");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Select an option:");
            string selection = Console.ReadLine() ?? string.Empty;
            if (selection == "1" || selection == "0")
            {
                Console.Clear();
                _actions[selection]();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please select one of the options given.");
                Console.ReadKey();
                Console.Clear();
            }
            
            /*switch (selection)
            {
                case 1:
                    Console.Clear();
                    //LoginMenu();
                    new LoginMenu().Open();
                    break;
                case 2:
                    loop = false;
                    break;
            }
            */
        }
    }

    private void LoopFalse()
    {
        _loop = false;
    }

    private void OpenLoginMenu()
    {
        new LoginMenu().Open(0);
    }
}