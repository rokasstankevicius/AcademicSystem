﻿using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class DeleteTeacher : Connection
{
    public void Run()
    {
        string query1 = "select * from `teacher`";
        string query2 = "SELECT COUNT(*) FROM `teacher`";
        var cmd1 = new MySqlCommand(query1, Conn());
        var cmd2 = new MySqlCommand(query2, Conn());
        var reader1 = cmd1.ExecuteReader();
        var reader2 = cmd2.ExecuteScalar();

        Console.WriteLine("List of teachers:");

        int[] list = new int[Convert.ToInt32(reader2)];
        for (int i = 1; reader1.Read(); i++)
        {
            list[i - 1] = (int)reader1["TeacherID"];
            Console.WriteLine(i + ". " + (string)reader1["FirstName"] + " " + (string)reader1["LastName"] + " ");
        }
        cmd1.Connection.Close();
        
        Console.WriteLine("Please select a teacher to delete:");
        string selection = Console.ReadLine() ?? string.Empty;
        if (selection != String.Empty)
        {
            int selectionInt = Convert.ToInt32(selection);
            bool teacherHasModules = false;
            string queryLook = "select * from `teaching subject`";
            var cmdLook = new MySqlCommand(queryLook, Conn());
            var readerLook = cmdLook.ExecuteReader();
            while (readerLook.Read())
            {
                if (list[selectionInt - 1] == (int)readerLook["TeacherID"])
                {
                    teacherHasModules = true;
                }
            }
            cmdLook.Connection.Close();

            if (teacherHasModules == false)
            {
                if (selectionInt - 1 <= Convert.ToInt32(reader2) && selectionInt - 1 >= 0)
                {
                    cmd2.Connection.Close();
                    string query3 = string.Format("delete from `teacher` where TeacherID ='{0}'",
                        list[selectionInt - 1]);
                    var cmd3 = new MySqlCommand(query3, Conn());
                    cmd3.ExecuteNonQuery();
                    cmd3.Connection.Close();
                    string query4 = string.Format("delete from `login` where LoginID ='{0}'", list[selectionInt - 1]);
                    var cmd4 = new MySqlCommand(query4, Conn());
                    cmd4.ExecuteNonQuery();
                    cmd4.Connection.Close();
                    Console.Clear();
                    Console.WriteLine("Teacher deleted.");
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
                    Console.WriteLine("This teacher currently has modules, deleting this teacher will delete the modules");
                    Console.WriteLine("and any evaluations given for those modules.");
                    Console.WriteLine("Are you sure you want to delete this teacher?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    string selectionForMassDeletion = Console.ReadLine() ?? string.Empty;
                    if (selectionForMassDeletion == "1")
                    {
                        string querryForMassDeletionFind = string.Format("Select * from `teaching subject` where TeacherID ='{0}'",
                            list[selectionInt - 1]);
                        var cmdForMassDeletionFind = new MySqlCommand(querryForMassDeletionFind, Conn());
                        var readerForMassDeletionFind = cmdForMassDeletionFind.ExecuteReader();
                        while (readerForMassDeletionFind.Read())
                        {
                            string querryForMassDeletion1 = string.Format("delete from `subject evaluation` where TeachingSubjectID='{0}'", (int)readerForMassDeletionFind["TeachingSubjectID"]);
                            var cmdForMassDeletion1 = new MySqlCommand(querryForMassDeletion1, Conn());
                            cmdForMassDeletion1.ExecuteNonQuery();
                            cmdForMassDeletion1.Connection.Close();
                        }

                        string querryForMassDeletion3 = string.Format("delete from `teaching subject` where TeacherID ='{0}'", list[selectionInt - 1]);
                        var cmdForMassDeletion3 = new MySqlCommand(querryForMassDeletion3, Conn());
                        cmdForMassDeletion3.ExecuteNonQuery();
                        
                        string querryForMassDeletion4 = string.Format("delete from `teacher` where TeacherID ='{0}'",
                            list[selectionInt - 1]);
                        var cmdForMassDeletion4 = new MySqlCommand(querryForMassDeletion4, Conn());
                        cmdForMassDeletion4.ExecuteNonQuery();
                        
                        string queryForMassDeletion5 = string.Format("delete from `login` where LoginID ='{0}'", list[selectionInt - 1]);
                        var cmdForMassDeletion5 = new MySqlCommand(queryForMassDeletion5, Conn());
                        cmdForMassDeletion5.ExecuteNonQuery();
                        
                        
                        cmdForMassDeletionFind.Connection.Close();
                        cmdForMassDeletion3.Connection.Close();
                        cmdForMassDeletion4.Connection.Close();
                        cmdForMassDeletion5.Connection.Close();
                        loopForMassDeletion = false;
                        Console.Clear();
                        Console.WriteLine("Teacher deleted.");
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