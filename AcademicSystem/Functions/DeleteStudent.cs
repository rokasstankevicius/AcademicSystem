using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class DeleteStudent : Connection
{
    public void Run()
    {
        string query1 = "select * from `student`";
        string query2 = "SELECT COUNT(*) FROM `student`";
        var cmd1 = new MySqlCommand(query1, Conn());
        var cmd2 = new MySqlCommand(query2, Conn());
        var reader1 = cmd1.ExecuteReader();
        var reader2 = cmd2.ExecuteScalar();

        Console.WriteLine("List of students:");

        int[] list = new int[Convert.ToInt32(reader2)];
        for (int i = 1; reader1.Read(); i++)
        {
            list[i - 1] = (int)reader1["StudentID"];
            string queryGroup = string.Format("SELECT `GroupName` FROM `group` WHERE GroupID = '{0}'",
                (int)reader1["GroupID"]);
            var cmdGroup = new MySqlCommand(queryGroup, Conn());
            var readerGroup = cmdGroup.ExecuteReader();
            Console.Write(i + ". " + (string)reader1["FirstName"] + " " + (string)reader1["LastName"] + " ");
            while (readerGroup.Read())
            {
                Console.WriteLine((string)readerGroup["GroupName"]);
            }
        }
        cmd1.Connection.Close();


        Console.WriteLine("WARNING: deleting a student will also delete their evaluations.");
        Console.WriteLine("Please select a student to delete:");
        string selection = Console.ReadLine() ?? string.Empty;
        if (selection != String.Empty)
        {
            int selectionInt = Convert.ToInt32(selection);
            if (selectionInt - 1 <= Convert.ToInt32(reader2) && selectionInt - 1 >= 0)
            {
                cmd2.Connection.Close();
                string query3 = string.Format("delete from `student` where StudentID ='{0}'", list[selectionInt - 1]);
                var cmd3 = new MySqlCommand(query3, Conn());
                cmd3.ExecuteNonQuery();
                cmd3.Connection.Close();
                string query4 = string.Format("delete from `login` where LoginID ='{0}'", list[selectionInt - 1]);
                var cmd4 = new MySqlCommand(query4, Conn());
                cmd4.ExecuteNonQuery();
                cmd4.Connection.Close();
                string query5 = string.Format("delete from `subject evaluation` where StudentID ='{0}'",
                    list[selectionInt - 1]);
                var cmd5 = new MySqlCommand(query5, Conn());
                cmd5.ExecuteNonQuery();
                cmd5.Connection.Close();
                Console.Clear();
                Console.WriteLine("Student deleted.");
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
            Console.Clear();
            Console.WriteLine("No input given.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}