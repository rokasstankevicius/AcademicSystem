using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class CreateModule : Connection
{
    public void Run()
    {
        bool b = true; // for the loop
        while (b)
        {
            string query1 = "select * from `subject`";
            string query2 = "SELECT COUNT(*) FROM `subject`"; 
            var cmd1 = new MySqlCommand(query1, Conn());
            var cmd2 = new MySqlCommand(query2, Conn()); 
            var reader1 = cmd1.ExecuteReader();
            var reader2 = cmd2.ExecuteScalar();
            Console.WriteLine("List of subjects:");
            int[] list = new int[Convert.ToInt32(reader2)];
            for (int i = 1; reader1.Read(); i++) 
            { 
                list[i - 1] = (int)reader1["SubjectID"]; 
                Console.WriteLine(i + ". " + (string)reader1["SubjectName"]);
            } 
            cmd1.Connection.Close();
            Console.WriteLine("0. Create a new subject"); 
            Console.WriteLine("Please select a subject:"); 
            string selection1 = Console.ReadLine() ?? string.Empty;
            if (selection1 != String.Empty)
            { 
                int selection1Int = Convert.ToInt32(selection1);
                if (selection1Int - 1 < Convert.ToInt32(reader2) && selection1Int - 1 >= 0)
                {
                    b = false;
                    //------------------------------------------------------------------------------------------------------
                    cmd2.Connection.Close();
                    bool c = true; // for the loop
                    while (c)
                    {
                        string query3 = "select * from `teacher`";
                        string query4 = "SELECT COUNT(*) FROM `teacher`";
                        var cmd3 = new MySqlCommand(query3, Conn());
                        var cmd4 = new MySqlCommand(query4, Conn());
                        var reader3 = cmd3.ExecuteReader();
                        var reader4 = cmd4.ExecuteScalar();
                        Console.WriteLine("List of teachers:");
                        int[] list2 = new int[Convert.ToInt32(reader4)];
                        for (int i = 1; reader3.Read(); i++)
                        {
                            list2[i - 1] = (int)reader3["TeacherID"];
                            Console.WriteLine(i + ". " + (string)reader3["FirstName"] + " " + (string)reader3["LastName"]);
                        }
                        cmd3.Connection.Close();
                        Console.WriteLine("0. Create a new teacher");
                        Console.WriteLine("Please select a teacher:");
                        string selection2 = Console.ReadLine() ?? string.Empty;
                        if (selection2 != String.Empty)
                        {
                            int selection2Int = Convert.ToInt32(selection2);
                            if (selection2Int - 1 < Convert.ToInt32(reader4) && selection2Int - 1 >= 0)
                            {
                                c = false;
                                //------------------------------------------------------------------------------------------------------
                                cmd4.Connection.Close();
                                bool d = true; // for the loop
                                while (d)
                                {

                                    string query5 = "select * from `group`";
                                    string query6 = "SELECT COUNT(*) FROM `group`";
                                    var cmd5 = new MySqlCommand(query5, Conn());
                                    var cmd6 = new MySqlCommand(query6, Conn());
                                    var reader5 = cmd5.ExecuteReader();
                                    var reader6 = cmd6.ExecuteScalar();
                                    Console.WriteLine("List of groups:");
                                    int[] list3 = new int[Convert.ToInt32(reader6)];
                                    for (int i = 1; reader5.Read(); i++)
                                    {
                                        list3[i - 1] = (int)reader5["GroupID"];
                                        Console.WriteLine(i + ". " + (string)reader5["GroupName"]);
                                    }
                                    cmd5.Connection.Close();
                                    Console.WriteLine("0. Create a new group");
                                    Console.WriteLine("Please select a group:");
                                    string selection3 = Console.ReadLine() ?? string.Empty;
                                    if (selection3 != String.Empty)
                                    {
                                        int selection3Int = Convert.ToInt32(selection3);
                                        if (selection3Int - 1 < Convert.ToInt32(reader6) && selection3Int - 1 >= 0)
                                        {
                                            d = false;
                                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                            cmd6.Connection.Close();
                                            bool moduleFound = false;
                                            string query = "select * from `teaching subject`";
                                            var cmd = new MySqlCommand(query, Conn());
                                            var reader = cmd.ExecuteReader();
        
                                            while (reader.Read() && moduleFound == false)
                                            {
                                                if (list[selection1Int - 1] == (int)reader["SubjectID"] && list2[selection2Int-1] == (int)reader["TeacherID"] && list3[selection3Int-1] == (int)reader["GroupID"])
                                                {
                                                    moduleFound= true;
                                                }
            
                                            }
                                            cmd.Connection.Close();
                                            if (moduleFound == false)
                                            {
                                                string queryModule = string.Format(
                                                    "insert into `teaching subject`(SubjectID, TeacherID, GroupID) VALUE ('{0}','{1}','{2}')",
                                                    list[selection1Int - 1], list2[selection2Int - 1],
                                                    list3[selection3Int - 1]);
                                                var cmdModule = new MySqlCommand(queryModule, Conn());
                                                cmdModule.ExecuteNonQuery();
                                                Console.Clear();
                                                Console.WriteLine("Module created.");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Console.WriteLine("A module with the same parameters already exists.");
                                                Console.ReadKey();
                                                Console.Clear();
                                            }
                                            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                                        }

                                        else if (selection3Int == 0)
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
                                        d = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                }
                                //------------------------------------------------------------------------------------------------------
                            }

                            else if (selection2Int == 0)
                            {
                                new CreateTeacher().Run();

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
                            c = false;
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
                    //------------------------------------------------------------------------------------------------------
                }
                else if (selection1Int == 0) 
                { 
                    new CreateSubject().Run();
                    
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
    }
}