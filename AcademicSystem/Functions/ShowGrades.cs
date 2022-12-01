using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class ShowGrades : Connection
{
    public void Run(int iD)
    {
        bool hasGrades = false;
        string query = "select * from `subject evaluation`";
        var cmd = new MySqlCommand(query, Conn());
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            if (iD == (int)reader["StudentID"])
            {
                hasGrades = true;
            }
        }
        cmd.Connection.Close();
        if (hasGrades == true)
        {
            bool loop = true;
            while(loop == true){
                string query1 = String.Format("SELECT * FROM `subject evaluation` where StudentID = '{0}'", iD);;
                var cmd1 = new MySqlCommand(query1, Conn());
                var reader1 = cmd1.ExecuteReader();
                Console.Clear();
                Console.WriteLine("List of evaluations:");
                int i = 1;
                //int[] list = new int[Convert.ToInt32(reader2)];
                reader1.Read();
                    if (iD == (int)reader1["StudentID"])
                    {
                        //list[i - 1] = (int)reader1["SubjectEvaluationID"];
                        //Console.WriteLine((int)reader1["StudentID"]);
                        string queryFind1 = String.Format("Select * from `subject evaluation` where StudentID = '{0}'", (int)reader1["StudentID"]);
                        var cmdFind1 = new MySqlCommand(queryFind1, Conn());
                        var readerFind1 = cmdFind1.ExecuteReader();
                        while (readerFind1.Read())
                        {
                            Console.Write(i + ". ");
                            //Console.WriteLine((int)reader1["StudentID"]);
                            string queryFind2 = String.Format("Select * from `teaching subject` where TeachingSubjectID = '{0}'", (int)readerFind1["TeachingSubjectID"]);
                            var cmdFind2 = new MySqlCommand(queryFind2, Conn());
                            var readerFind2 = cmdFind2.ExecuteReader();
                            readerFind2.Read();
                            //Console.WriteLine((int)readerFind1["TeachingSubjectID"]);
                            //Console.Write("Subject: " + (string)readerFind2["SubjectName"] + ", ");
                            //Console.WriteLine((int)readerFind2["TeacherID"]);
                            string queryFind4 = String.Format("Select * from `teacher` where TeacherID= '{0}'", (int)readerFind2["TeacherID"]);
                            var cmdFind4 = new MySqlCommand(queryFind4, Conn());
                            var readerFind4 = cmdFind4.ExecuteReader();
                            readerFind4.Read();
                            Console.Write("Teacher: " + (string)readerFind4["FirstName"] + " " + (string)readerFind4["LastName"] + ", ");
                            //Console.WriteLine((int)readerFind2["SubjectID"]);
                            string queryFind5 = String.Format("Select * from `subject` where SubjectID = '{0}'", (int)readerFind2["SubjectID"]);
                            var cmdFind5 = new MySqlCommand(queryFind5, Conn());
                            var readerFind5 = cmdFind5.ExecuteReader();
                            readerFind5.Read();
                            Console.Write("Subject: " + (string)readerFind5["SubjectName"] + ", ");
                            Console.WriteLine("Grade: " + (int)readerFind1["Grade"]);
                            cmdFind4.Connection.Close();
                            cmdFind5.Connection.Close();
                            cmdFind2.Connection.Close();
                            //Console.WriteLine("Grade: " + (int)readerFind1["Grade"]);
                            i++;
                        }
                        cmdFind1.Connection.Close();
                    }
                    cmd1.Connection.Close();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                Console.Clear();
                loop = false;
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You have no evaluations.");
            Console.ReadKey();
            Console.Clear();
        }
        ConnClose();
    }
}