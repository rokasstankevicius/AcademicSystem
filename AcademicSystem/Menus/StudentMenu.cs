using AcademicSystem.Functions;
using MySql.Data.MySqlClient;

namespace AcademicSystem.Menus;

public class StudentMenu : Menu
{
    private bool _loop = true;

    public override void Open(int iD)
    {
        //--------------------------------------------------------------------------------------------------------------
        string query = String.Format("select * from student where StudentID = '{0}'", iD);
        var cmd = new MySqlCommand(query, Conn());
        var reader = cmd.ExecuteReader();
        reader.Read();
        string fName = (string)reader["FirstName"];
        string lName = (string)reader["LastName"];
        int gId = (int)reader["GroupID"];
        cmd.Connection.Close();
        string queryG = String.Format("select * from `group` where GroupID = '{0}'", gId);
        var cmdG = new MySqlCommand(queryG, Conn());
        var readerG = cmdG.ExecuteReader();
        readerG.Read();
        string gName = (string)readerG["GroupName"];
        cmdG.Connection.Close();
        //--------------------------------------------------------------------------------------------------------------
        //Actions["2"] = new DeleteStudentGroup().Run;
        while (_loop)
        {
            Console.WriteLine("[Welcome student " + fName + " " + lName + " " + gName + "]");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Show evaluations");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Select an option:");
            string selection = Console.ReadLine() ?? string.Empty;
            if (selection != String.Empty)
            {
                int optionMenu = Convert.ToInt32(selection);
                if (optionMenu == 1)
                {
                    Console.Clear();
                    new ShowGrades().Run(iD);
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