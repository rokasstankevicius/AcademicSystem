using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class CreateTeacher : Connection
{
    public void Run()
    {
        bool found = false;
        Console.WriteLine("Please type in the firstname of the teacher:");
        string fName = Console.ReadLine() ?? string.Empty;
        if (fName != String.Empty)
        {
            Console.WriteLine("Please type in the lastname of the teacher:");
            string lName = Console.ReadLine() ?? string.Empty;
            if (lName != String.Empty)
            {
                //------------------------------------------------------------------------------------------------------
                string query1 = "select * from teacher";
                var cmd1 = new MySqlCommand(query1, Conn());
                var reader1 = cmd1.ExecuteReader();

                while (reader1.Read() && found == false)
                {
                    if (fName == (string)reader1["FirstName"] && lName == (string)reader1["LastName"])
                    {
                        found = true;
                    }
                }
                cmd1.Connection.Close();
                if (found == false)
                {

                    string query4 = string.Format("insert into login(Username, Password) VALUE ('{0}','{1}')", fName, lName);
                    var cmd4 = new MySqlCommand(query4, Conn());
                    cmd4.ExecuteNonQuery();
                    int newStudentId = (int)cmd4.LastInsertedId;
                    cmd4.Connection.Close();
                    //-------------------------------------------------------------------------------------------
                    string query5 = string.Format("insert into teacher(TeacherID, FirstName, LastName) VALUE ('{0}','{1}','{2}')", newStudentId, fName, lName);
                    var cmd5 = new MySqlCommand(query5, Conn());
                    cmd5.ExecuteNonQuery();
                    cmd5.Connection.Close();
                    //-------------------------------------------------------------------------------------------
                    Console.Clear();
                    Console.WriteLine("Teacher created.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("A teacher with the same first and last name already exists.");
                    Console.WriteLine("(System limitation: cannot create users with the same full name because their name is used for logging into the system)");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No input given");
                Console.ReadKey();
                Console.Clear();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("No input given");
            Console.ReadKey();
            Console.Clear();
        }
    }
}