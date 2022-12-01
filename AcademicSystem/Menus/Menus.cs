namespace AcademicSystem.Menus;

public class Menus
{
    public Menus()
    {
        MainMenu mainMenu = new MainMenu();
        LoginMenu loginMenu = new LoginMenu();
        AdminMenu adminMenu = new AdminMenu();
        TeacherMenu teacherMenu = new TeacherMenu();
        StudentMenu studentMenu = new StudentMenu();
        Menu[] unused = { mainMenu, loginMenu, adminMenu, teacherMenu, studentMenu  };
    }

}