using AcademicSystem.Functions;
using MySql.Data.MySqlClient;

namespace AcademicSystem.Menus;

public class TeacherMenu : Menu
{
    
    private bool _loop = true;
    
    public override void Open(int iD)
    {
        //--------------------------------------------------------------------------------------------------------------
        string query = String.Format("select * from teacher where TeacherID = '{0}'", iD);
        var cmd = new MySqlCommand(query, Conn());
        var reader = cmd.ExecuteReader();
        reader.Read();
        string fName = (string)reader["FirstName"];
        string lName = (string)reader["LastName"];
        cmd.Connection.Close();
        //--------------------------------------------------------------------------------------------------------------
        //Actions["2"] = new DeleteStudentGroup().Run;
        while (_loop)
        {
            Console.WriteLine("[Welcome teacher " + fName + " " + lName + "]");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Evaluate a student");
            Console.WriteLine("2. Update student evaluation");
            Console.WriteLine("0. Exit");
            Console.WriteLine("NOTE: leaving the input empty and pressing enter will cancel the option.");
            Console.WriteLine("Select an option:");
            string selection = Console.ReadLine() ?? string.Empty;
            if (selection != String.Empty)
            {
                int optionMenu = Convert.ToInt32(selection);
                if (optionMenu == 1)
                {
                    Console.Clear();
                    new EvaluateStudent().Run(iD);
                }
                else if (optionMenu == 2)
                {
                    Console.Clear();
                    new UpdateStudentEvaluation().Run(iD);
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