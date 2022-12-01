using AcademicSystem.Menus;

namespace AcademicSystem
{
    static class Program
    {
        static void Main()
        {
            Menus.Menus menus = new Menus.Menus(); // for all the menus used
            new MainMenu().Open(0); // Starts the Main menu
            //Test test = new Test();
        }
    }
}