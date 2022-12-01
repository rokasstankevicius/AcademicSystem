using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class EvaluateStudent : Connection
{
    public void Run(int iD)
    {
        bool hasModule = false;
        string query = "select * from `teaching subject`";
        var cmd = new MySqlCommand(query, Conn());
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            if (iD == (int)reader["TeacherID"])
            {
                hasModule = true;
            }
        }
        cmd.Connection.Close();
        if (hasModule == true)
        {
            //==========================================================================================================
            bool loop1 = true;
            while (loop1)
            {
                string query1 = "select * from `teaching subject`";
                string query2 = String.Format("SELECT COUNT(*) FROM `teaching subject` where TeacherID = '{0}'", iD);
                var cmd1 = new MySqlCommand(query1, Conn());
                var cmd2 = new MySqlCommand(query2, Conn());
                var reader1 = cmd1.ExecuteReader();
                var reader2 = cmd2.ExecuteScalar();
                Console.Clear();
                Console.WriteLine("List of modules:");

                int[] list = new int[Convert.ToInt32(reader2)];
                //for (int i = 1; reader1.Read(); i++)
                int j = 1;
                while (reader1.Read())
                {
                    if (iD == (int)reader1["TeacherID"])
                    {
                        list[j - 1] = (int)reader1["TeachingSubjectID"];
                        Console.Write(j + ". ");
                        string queryFind1 = String.Format(
                            "Select * from `teaching subject` where TeachingSubjectID = '{0}'",
                            (int)reader1["TeachingSubjectID"]);
                        var cmdFind1 = new MySqlCommand(queryFind1, Conn());
                        var readerFind1 = cmdFind1.ExecuteReader();
                        while (readerFind1.Read())
                        {
                            string queryFind2 = String.Format("Select * from `subject` where SubjectID = '{0}'",
                                (int)readerFind1["SubjectID"]);
                            var cmdFind2 = new MySqlCommand(queryFind2, Conn());
                            var readerFind2 = cmdFind2.ExecuteReader();
                            while (readerFind2.Read())
                            {
                                Console.Write("Subject: " + (string)readerFind2["SubjectName"] + ", ");
                                string queryFind4 = String.Format("Select * from `group` where GroupID = '{0}'",
                                    (int)readerFind1["GroupID"]);
                                var cmdFind4 = new MySqlCommand(queryFind4, Conn());
                                var readerFind4 = cmdFind4.ExecuteReader();
                                while (readerFind4.Read())
                                {
                                    Console.WriteLine("Group: " + (string)readerFind4["GroupName"]);
                                }
                            }
                        }
                        cmdFind1.Connection.Close();
                        j++;
                    }
                }
                Console.WriteLine("0. Exit");
                Console.WriteLine("Please select a module:");
                string selection1 = Console.ReadLine() ?? string.Empty;
                if (selection1 != String.Empty)
                {
                    int selection1Int = Convert.ToInt32(selection1);
                    if (selection1Int <= Convert.ToInt32(reader2) && selection1Int > 0)
                    {
                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        cmd2.Connection.Close();
                        bool modulesGroupHasStudents = false;
                        int studenGroupID = 0;
                        string queryG = "select * from `teaching subject`";
                        var cmdG = new MySqlCommand(queryG, Conn());
                        var readerG = cmdG.ExecuteReader();
                        
                        while (readerG.Read())
                        {
                            if (list[selection1Int - 1] == (int)readerG["TeachingSubjectID"])
                            {
                                string queryG1 = "select * from `student`";
                                var cmdG1 = new MySqlCommand(queryG1, Conn());
                                var readerG1 = cmdG1.ExecuteReader();
                                while (readerG1.Read())
                                {
                                    if ((int)readerG["GroupID"] == (int)readerG1["GroupID"])
                                    {
                                        modulesGroupHasStudents = true;
                                        studenGroupID = (int)readerG["GroupID"];
                                    }
                                }
                            }
                        }
                        cmdG.Connection.Close();

                        
                        if (modulesGroupHasStudents == true)
                        {
                            bool loop2 = true;
                            while (loop2)
                            {
                                string query3 = "select * from `student`";
                                string query4 = String.Format("SELECT COUNT(*) FROM `student` where GroupID = '{0}'", studenGroupID);
                                //string query4 = String.Format("SELECT COUNT(*) FROM `subject evaluation` where StudentID = '{0}'", studenGroupID);
                                var cmd3 = new MySqlCommand(query3, Conn());
                                var cmd4 = new MySqlCommand(query4, Conn());
                                var reader3 = cmd3.ExecuteReader();
                                var reader4 = cmd4.ExecuteScalar();
                                Console.Clear();
                                Console.WriteLine("List of students:");

                                int[] list2 = new int[Convert.ToInt32(reader4)];
                                //Console.WriteLine("size: "+Convert.ToInt32(reader4));
                                //Console.WriteLine(studenGroupID);
                                //for (int i = 1; reader3.Read(); i++)
                                int i = 1;
                                while(reader3.Read())
                                {
                                    //list2[i - 1] = (int)reader3["StudentID"];
                                    //Console.Write((int)reader3["GroupID"]);
                                    //Console.WriteLine(studenGroupID);
                                    //int temp = (int)reader3["GroupID"];
                                    //if(temp == studenGroupID) Console.WriteLine("if dont work anymore");
                                    
                                    if (studenGroupID==(int)reader3["GroupID"])
                                    {
                                        //Console.WriteLine("added");
                                        list2[i - 1] = (int)reader3["StudentID"];
                                        Console.Write(i + ". " + (string)reader3["FirstName"] + " " + (string)reader3["LastName"] + " ");
                                        Console.WriteLine();
                                        i++;
                                    }
                                }
                                cmd3.Connection.Close();
                                
                                Console.WriteLine("0. Back");
                                Console.WriteLine("Please select a student:");
                                string selection2 = Console.ReadLine() ?? string.Empty;
                                if (selection2 != String.Empty)
                                {
                                    int selection2Int = Convert.ToInt32(selection2);
                                    if (selection2Int <= Convert.ToInt32(reader4) && selection2Int > 0)
                                    {
                                        //*************************************************************************************************
                                        cmd4.Connection.Close();
                                        bool studentsHasEvaluation = false;
                                        string queryStudentEvaluation = "select * from `subject evaluation`";
                                        var cmdStudentEvaluation = new MySqlCommand(queryStudentEvaluation, Conn());
                                        var readerStudentEvaluation = cmdStudentEvaluation.ExecuteReader();
                                        //Console.WriteLine(studentsHasEvaluation);
                                        while (readerStudentEvaluation.Read())
                                        {
                                                //Console.WriteLine((int)readerStudentEvaluation["StudentID"] + " " + (int)readerStudentEvaluation["TeachingSubjectID"]);
                                            if (list2[selection2Int - 1] == (int)readerStudentEvaluation["StudentID"] && list[selection1Int-1] == (int)readerStudentEvaluation["TeachingSubjectID"])
                                            {
                                                
                                                studentsHasEvaluation = true;
                                            }
                                        }
                                        cmdStudentEvaluation.Connection.Close();
                                        //Console.WriteLine(studentsHasEvaluation);
                                        if (studentsHasEvaluation == false)
                                        {
                                            bool loop3 = true;
                                            while (loop3 == true)
                                            {
                                                Console.Clear();
                                                Console.WriteLine("NOTE1: input must be between 1 and 10 for it to be entered.");
                                                Console.WriteLine("NOTE2: inputting 0 will close the menu.");
                                                Console.WriteLine("Please type in the students evaluation:");
                                                string grade = Console.ReadLine() ?? string.Empty;
                                                if (grade != String.Empty)
                                                {
                                                    int gradeInt = Convert.ToInt32(grade);
                                                    if (gradeInt <= 10 && gradeInt > 0)
                                                    {
                                                        loop3 = false;
                                                        string queryEvaluation = string.Format("insert into `subject evaluation` (`StudentID`, `TeachingSubjectID`, `Grade`) VALUE ('{0}','{1}','{2}')", list2[selection2Int-1],list[selection1Int-1],gradeInt);
                                                        var cmdEvaluation = new MySqlCommand(queryEvaluation, Conn());
                                                        cmdEvaluation.ExecuteNonQuery();
                                                        Console.Clear();
                                                        Console.WriteLine("Student evaluated.");
                                                        cmdEvaluation.Connection.Close();
                                                    }
                                                    else if (gradeInt == 0)
                                                    {
                                                        Console.Clear();
                                                        loop3 = false;
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
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("This student has already been evaluated.");
                                            Console.ReadKey();
                                            Console.Clear();
                                        }
                                        
                                        
                                        
                                        
                                        
                                        
                                        //Need to add the actual evaluation part 
                                        //And a checker is the student is already evaluated in this module
                                        
                                        
                                        
                                        //*************************************************************************************************
                                    }
                                    else if (selection2Int == 0)
                                        
                                    {
                                        Console.Clear();
                                        loop2 = false;
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
                                    Console.WriteLine("No input given.");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("This module's group has no students.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    }
                    else if (selection1Int == 0)
                    {
                        Console.Clear();
                        loop1 = false;
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
                    Console.WriteLine("No input given.");
                    Console.ReadKey();
                    Console.Clear();
                }
                cmd1.Connection.Close();
            }
            //==========================================================================================================
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You have no modules.");
            Console.ReadKey();
            Console.Clear();
        }
        ConnClose();
    }
}