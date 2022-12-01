using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class DeleteStudentGroup : Connection
{
    public void Run()
    {
        string query1 = "select * from `group`";
        string query2 = "SELECT COUNT(*) FROM `group`";
        var cmd1 = new MySqlCommand(query1, Conn());
        var cmd2 = new MySqlCommand(query2, Conn());
        var reader1 = cmd1.ExecuteReader();
        var reader2 = cmd2.ExecuteScalar();

        Console.WriteLine("List of groups:");

        int[] list = new int[Convert.ToInt32(reader2)];
        for (int i = 1; reader1.Read(); i++)
        {
            list[i - 1] = (int)reader1["GroupID"];
            Console.WriteLine(i + ". " + (string)reader1["GroupName"]);
        }
        cmd1.Connection.Close();
        Console.WriteLine("Please select a group to delete:");
        string selection = Console.ReadLine() ?? string.Empty;
        if (selection != String.Empty)
        {
            int selectionInt = Convert.ToInt32(selection);
            bool groupHasStudents = false;
            string query3 = "select * from `student`";
            var cmd3 = new MySqlCommand(query3, Conn());
            var reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                if (list[selectionInt - 1] == (int)reader3["GroupID"])
                {
                    groupHasStudents = true;
                }
            }
            cmd3.Connection.Close();
            //----------------------------------------------------------------------------------------------------------
            if (groupHasStudents == false)
            {
                if (selectionInt - 1 <= Convert.ToInt32(reader2) && selectionInt - 1 >= 0)
                {
                    cmd2.Connection.Close();
                    string query4 = string.Format("delete from `group` where GroupID ='{0}'", list[selectionInt - 1]);
                    var cmd4 = new MySqlCommand(query4, Conn());
                    cmd4.ExecuteNonQuery();
                    cmd4.Connection.Close();
                    Console.Clear();
                    Console.WriteLine("Group deleted.");
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
                    Console.WriteLine(
                        "This group is in use, deleting it will delete students that belong to this group,");
                    Console.WriteLine(
                        "the student evaluations that belong to the students in the group and modules that");
                    Console.WriteLine("are for this group.");
                    Console.WriteLine("Are you sure you want to delete this group?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    string selectionForMassDeletion = Console.ReadLine() ?? string.Empty;
                    if (selectionForMassDeletion == "1")
                    {
                        string querryForMassDeletionFind = string.Format("Select * from `student` where GroupID ='{0}'",
                            list[selectionInt - 1]);
                        var cmdForMassDeletionFind = new MySqlCommand(querryForMassDeletionFind, Conn());
                        var readerForMassDeletionFind = cmdForMassDeletionFind.ExecuteReader();
                        while (readerForMassDeletionFind.Read())
                        {
                            string querryForMassDeletion1 =
                                string.Format("delete from `subject evaluation` where StudentID='{0}'",
                                    (int)readerForMassDeletionFind["StudentID"]);
                            var cmdForMassDeletion1 = new MySqlCommand(querryForMassDeletion1, Conn());
                            cmdForMassDeletion1.ExecuteNonQuery();
                        }
                        cmdForMassDeletionFind.Connection.Close();
                        //



                        string queryStudents1 = "select * from `student`";
                        string queryStudents2 = "SELECT COUNT(*) FROM `student`";
                        var cmdStudents1 = new MySqlCommand(queryStudents1, Conn());
                        var cmdStudents2 = new MySqlCommand(queryStudents2, Conn());
                        var readerStudents1 = cmdStudents1.ExecuteReader();
                        var readerStudents2 = cmdStudents2.ExecuteScalar();


                        int[] listStudents = new int[Convert.ToInt32(readerStudents2)];

                        for (int i = 0; readerStudents1.Read(); i++)
                        {
                            if (list[selectionInt - 1] == (int)readerStudents1["GroupID"])
                            {
                                listStudents[i] = (int)readerStudents1["StudentID"];
                            }
                        }
                        cmdStudents1.Connection.Close();

                        string querryForMassDeletion2 = string.Format("delete from `student` where GroupID ='{0}'",
                            list[selectionInt - 1]);
                        var cmdForMassDeletion2 = new MySqlCommand(querryForMassDeletion2, Conn());
                        cmdForMassDeletion2.ExecuteNonQuery();
                        cmdForMassDeletion2.Connection.Close();
                        for (int i = 1; i <= Convert.ToInt32(readerStudents2); i++)
                        {
                            string querryForMassDeletionUser = string.Format("delete from `login` where LoginID ='{0}'",
                                listStudents[i - 1]);
                            var cmdForMassDeletionUser = new MySqlCommand(querryForMassDeletionUser, Conn());
                            cmdForMassDeletionUser.ExecuteNonQuery();
                            cmdForMassDeletionUser.Connection.Close();
                        }
                        cmdStudents2.Connection.Close();


                        string querryForMassDeletion3 =
                            string.Format("delete from `teaching subject` where GroupID ='{0}'",
                                list[selectionInt - 1]);
                        var cmdForMassDeletion3 = new MySqlCommand(querryForMassDeletion3, Conn());
                        cmdForMassDeletion3.ExecuteNonQuery();
                        cmdForMassDeletion3.Connection.Close();
                        string querryForMassDeletion4 = string.Format("delete from `group` where GroupID ='{0}'",
                            list[selectionInt - 1]);
                        var cmdForMassDeletion4 = new MySqlCommand(querryForMassDeletion4, Conn());
                        cmdForMassDeletion4.ExecuteNonQuery();
                        cmdForMassDeletion4.Connection.Close();
                        loopForMassDeletion = false;
                        Console.Clear();
                        Console.WriteLine("Group deleted.");
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
            //----------------------------------------------------------------------------------------------------------
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