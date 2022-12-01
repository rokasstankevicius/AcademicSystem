using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class CreateStudent : Connection
{
    public void Run()
    {
        bool studentFound = false;
        Console.WriteLine("Please type in the firstname of the students:");
        string fName = Console.ReadLine() ?? string.Empty;
        if (fName != String.Empty)
        {
            Console.WriteLine("Please type in the lastname of the students:");
            string lName = Console.ReadLine() ?? string.Empty;
            if (lName != String.Empty)
            {
                //------------------------------------------------------------------------------------------------------
                string query1 = "select * from student";
                var cmd1 = new MySqlCommand(query1, Conn());
                var reader1 = cmd1.ExecuteReader();

                while (reader1.Read() && studentFound == false)
                {
                    if (fName == (string)reader1["FirstName"] && lName == (string)reader1["LastName"])
                    {
                        studentFound = true;
                    }
                }
                cmd1.Connection.Close();
                if (studentFound == false)
                {
                    //------------------------------------------------------------------------------------------------------

                    //------------------------------------------------------------------------------------------------------
                    bool b = true; // for the loop
                    while (b)
                    {
                        //------------------------------------------------------------------------------------------------------
                        string query2 = "select * from `group`";
                        string query3 = "SELECT COUNT(*) FROM `group`";
                        var cmd2 = new MySqlCommand(query2, Conn());
                        var cmd3 = new MySqlCommand(query3, Conn());
                        var reader2 = cmd2.ExecuteReader();
                        var reader3 = cmd3.ExecuteScalar();

                        Console.WriteLine("List of groups:");

                        int[] list = new int[Convert.ToInt32(reader3)];
                        for (int i = 1; reader2.Read(); i++)
                        {
                            list[i - 1] = (int)reader2["GroupID"];
                            Console.WriteLine(i + ". " + (string)reader2["GroupName"]);
                        }
                        cmd2.Connection.Close();
                        Console.WriteLine("0. Create a new student group");
                        Console.WriteLine("Please select the students group:");
                        string selection = Console.ReadLine() ?? string.Empty;
                        //------------------------------------------------------------------------------------------------------
                        if (selection != String.Empty)
                        {
                            int selectionInt = Convert.ToInt32(selection);
                            if (selectionInt - 1 < Convert.ToInt32(reader3) && selectionInt - 1 >= 0)
                            {
                                cmd3.Connection.Close();
                                
                                string query4 =
                                    string.Format("insert into login(Username, Password) VALUE ('{0}','{1}')", fName,
                                        lName);
                                var cmd4 = new MySqlCommand(query4, Conn());
                                cmd4.ExecuteNonQuery();
                                int newStudentId = (int)cmd4.LastInsertedId;
                                cmd4.Connection.Close();
                                //-------------------------------------------------------------------------------------------
                                string query5 =
                                    string.Format(
                                        "insert into student(StudentID, FirstName, LastName, GroupID) VALUE ('{0}','{1}','{2}','{3}')",
                                        newStudentId, fName, lName, list[selectionInt - 1]);
                                var cmd5 = new MySqlCommand(query5, Conn());
                                cmd5.ExecuteNonQuery();
                                cmd5.Connection.Close();
                                //-------------------------------------------------------------------------------------------
                                b = false;
                                Console.Clear();
                                Console.WriteLine("Student created.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else if (selectionInt == 0)
                            {
                                new CreateStudentGroup().Run();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("The given selection does not match the list.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("No input given.");
                            b = false;
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }

                    //------------------------------------------------------------------------------------------------------
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("A student with the same first and last name already exists.");
                    Console.WriteLine("(System limitation: cannot create users with the same full name because their name is used for logging into the system)");
                    Console.ReadKey();
                    Console.Clear();
                }
                //------------------------------------------------------------------------------------------------------
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