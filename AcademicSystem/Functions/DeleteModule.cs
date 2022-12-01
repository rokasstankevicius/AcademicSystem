using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class DeleteModule : Connection
{
    public void Run()
    {
        string query1 = "select * from `teaching subject`";
        string query2 = "SELECT COUNT(*) FROM `teaching subject`";
        var cmd1 = new MySqlCommand(query1, Conn());
        var cmd2 = new MySqlCommand(query2, Conn());
        var reader1 = cmd1.ExecuteReader();
        var reader2 = cmd2.ExecuteScalar();

        Console.WriteLine("List of modules:");

        int[] list = new int[Convert.ToInt32(reader2)];
        for (int i = 1; reader1.Read(); i++)
        {
            list[i - 1] = (int)reader1["TeachingSubjectID"];
            Console.Write(i + ". ");
            string queryFind1 = String.Format("Select * from `teaching subject` where TeachingSubjectID = '{0}'", (int)reader1["TeachingSubjectID"]);
            var cmdFind1 = new MySqlCommand(queryFind1, Conn());
            var readerFind1 = cmdFind1.ExecuteReader();
            while (readerFind1.Read())
            {
                string queryFind2 = String.Format("Select * from `subject` where SubjectID = '{0}'", (int)readerFind1["SubjectID"]);
                var cmdFind2 = new MySqlCommand(queryFind2, Conn());
                var readerFind2 = cmdFind2.ExecuteReader();
                while (readerFind2.Read())
                {
                    Console.Write("Subject: " + (string)readerFind2["SubjectName"] + " ");
                    string queryFind3 = String.Format("Select * from `teacher` where TeacherID = '{0}'", (int)readerFind1["TeacherID"]);
                    var cmdFind3 = new MySqlCommand(queryFind3, Conn());
                    var readerFind3 = cmdFind3.ExecuteReader();
                    while (readerFind3.Read())
                    {
                        Console.Write("Teacher: " + (string)readerFind3["FirstName"] + " " + (string)readerFind3["LastName"] + " ");
                        string queryFind4 = String.Format("Select * from `group` where GroupID = '{0}'", (int)readerFind1["GroupID"]);
                        var cmdFind4 = new MySqlCommand(queryFind4, Conn());
                        var readerFind4 = cmdFind4.ExecuteReader();
                        while (readerFind4.Read())
                        {
                            Console.WriteLine("Group: " + (string)readerFind4["GroupName"]);
                        }
                        cmdFind4.Connection.Close();
                    }
                    cmdFind3.Connection.Close();
                }
                cmdFind2.Connection.Close();
            }
            cmdFind1.Connection.Close();
            
        }
        cmd1.Connection.Close();
        Console.WriteLine("Please select a module to delete:");
        string selection = Console.ReadLine() ?? string.Empty;
        
        if (selection != String.Empty)
        {
            int selectionInt = Convert.ToInt32(selection);

            bool moduleHasEvaluation = false;
            string query3 = "select * from `subject evaluation`";
            var cmd3 = new MySqlCommand(query3, Conn());
            var reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                if (list[selectionInt - 1] == (int)reader3["TeachingSubjectID"])
                {
                    moduleHasEvaluation = true;
                }
            }
            cmd3.Connection.Close();
            //----------------------------------------------------------------------------------------------------------
            if (moduleHasEvaluation == false)
            {

                if (selectionInt - 1 <= Convert.ToInt32(reader2) && selectionInt - 1 >= 0)
                {
                    cmd2.Connection.Close();
                    string query4 = string.Format("delete from `teaching subject` where TeachingSubjectID ='{0}'",
                        list[selectionInt - 1]);
                    var cmd4 = new MySqlCommand(query4, Conn());
                    cmd4.ExecuteNonQuery();
                    Console.Clear();
                    Console.WriteLine("Module deleted.");
                    Console.ReadKey();
                    Console.Clear();
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
                bool loopForMassDeletion = true;
                while (loopForMassDeletion)
                {
                    Console.Clear();
                    Console.WriteLine("This module has evaluations.");
                    Console.WriteLine("Are you sure you want to delete this subject?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    string selectionForMassDeletion = Console.ReadLine() ?? string.Empty;
                    if (selectionForMassDeletion == "1")
                    {
                        string querryForMassDeletion3 = string.Format("delete from `subject evaluation` where TeachingSubjectID ='{0}'", list[selectionInt - 1]);
                        var cmdForMassDeletion3 = new MySqlCommand(querryForMassDeletion3, Conn());
                        cmdForMassDeletion3.ExecuteNonQuery();
                        cmdForMassDeletion3.Connection.Close();
                        string querryForMassDeletion4 = string.Format("delete from `teaching subject` where TeachingSubjectID ='{0}'",
                            list[selectionInt - 1]);
                        var cmdForMassDeletion4 = new MySqlCommand(querryForMassDeletion4, Conn());
                        cmdForMassDeletion4.ExecuteNonQuery();
                        cmdForMassDeletion4.Connection.Close();
                        loopForMassDeletion = false;
                        Console.Clear();
                        Console.WriteLine("Module deleted.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else if (selectionForMassDeletion == "2")
                    {
                        loopForMassDeletion = false;
                        Console.Clear();
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
        else
        {
            Console.Clear();
            Console.WriteLine("No input given.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}