using AcademicSystem.Functions;
using MySql.Data.MySqlClient;

namespace AcademicSystem.Menus;

public class AdminMenu : Menu
{
    private bool _loop = true;
    private static Dictionary<string, Action> Actions = new Dictionary<string, System.Action>();


    public override void Open(int iD)
    {
        //--------------------------------------------------------------------------------------------------------------
        string query = String.Format("select * from admin where AdminID = '{0}'", iD);
        var cmd = new MySqlCommand(query, Conn());
        var reader = cmd.ExecuteReader();
        reader.Read();
        string fName = (string)reader["FirstName"];
        string lName = (string)reader["LastName"];
        cmd.Connection.Close();
        //--------------------------------------------------------------------------------------------------------------
        Actions["1"] = new CreateStudentGroup().Run;
        Actions["2"] = new DeleteStudentGroup().Run;
        Actions["3"] = new CreateSubject().Run;
        Actions["4"] = new DeleteSubject().Run;
        Actions["5"] = new CreateStudent().Run;
        Actions["6"] = new DeleteStudent().Run;
        Actions["7"] = new CreateTeacher().Run;
        Actions["8"] = new DeleteTeacher().Run;
        Actions["9"] = new CreateModule().Run;
        Actions["10"] = new DeleteModule().Run;
        while (_loop)
        {
            Console.WriteLine("[Welcome administrator " + fName + " " + lName + "]");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create a new student group");
            Console.WriteLine("2. Delete a student group");
            Console.WriteLine("3. Create a new subject");
            Console.WriteLine("4. Delete a subject");
            Console.WriteLine("5. Create a new student");
            Console.WriteLine("6. Delete a student");
            Console.WriteLine("7. Create a new teacher");
            Console.WriteLine("8. Delete a teacher");
            Console.WriteLine("9. Create a new module");
            Console.WriteLine("10. Delete a module");
            Console.WriteLine("0. Exit");
            Console.WriteLine("NOTE: leaving the input empty and pressing enter will cancel the option.");
            Console.WriteLine("Select an option:");
            string selection = Console.ReadLine() ?? string.Empty;
            if (selection != String.Empty)
            {
                int optionMenu = Convert.ToInt32(selection);
                if (optionMenu <= 10 && optionMenu > 0)
                {
                    Console.Clear();
                    Actions[selection]();
                }
                else if (selection == "0")
                {
                    Console.Clear();
                    _loop = false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please select one of the options given.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Please select one of the options given.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
