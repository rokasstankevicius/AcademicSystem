using MySql.Data.MySqlClient;

namespace AcademicSystem.Functions;

public class CreateSubject : Connection
{
    public void Run()
    {
        Console.WriteLine("Please type in the new subject:");
        string name = Console.ReadLine() ?? string.Empty;
        if (name != String.Empty)
        {
            bool alreadyExists = false;
            string query1 = "select * from `subject`";
            var cmd1 = new MySqlCommand(query1, Conn());
            var reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                if (name == (string)reader["SubjectName"]) alreadyExists = true;
            }
            cmd1.Connection.Close();
            if (alreadyExists == false)
            {
                string query2 = string.Format("insert into `subject` (`SubjectName`) VALUE ('{0}')", name);
                var cmd2 = new MySqlCommand(query2, Conn());
                cmd2.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine("A subject with this name (" + name + ") has been created");
                Console.ReadKey();
                Console.Clear();
                cmd2.Connection.Close();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("A subject with this name already exists.");
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