using MySql.Data.MySqlClient;

namespace AcademicSystem;

public class Login : Connection
{
    public bool userFound = false;
    public int foundUsersID = 0;
    public bool admin = false;
    public bool teacher = false;
    public bool student = false;
    public Login(string? username, string? password)
    {
        string query = "select * from login";
        var cmd = new MySqlCommand(query, Conn());
        var reader = cmd.ExecuteReader();
        
        while (reader.Read() && userFound == false)
        {
            if (username == (string)reader["Username"] && password == (string)reader["Password"])
            {
                userFound = true;
                foundUsersID = (int)reader["LoginID"];
                UserPermissions();
                //Console.WriteLine(userFound + " " + foundUsersID);
            }
            
        }
        ConnClose();
    }

    void UserPermissions()
    {
        bool found = false;
        string query1 = "select * from admin";
        string query2 = "select * from teacher";
        string query3 = "select * from student";
        var cmd1 = new MySqlCommand(query1, Conn());
        var cmd2 = new MySqlCommand(query2, Conn());
        var cmd3 = new MySqlCommand(query3, Conn());
        var reader1 = cmd1.ExecuteReader();
        var reader2 = cmd2.ExecuteReader();
        var reader3 = cmd3.ExecuteReader();
        /*while (reader1.Read() || reader2.Read() || reader3.Read() || found == false)
        {
            if (foundUsersID == (int)reader1["AdminID"])
            {
                found = true;
                admin = true;
            }
            else if (foundUsersID == (int)reader2["TeacherID"])
            {
                found = true;
                teacher = true;
            }
            else if (foundUsersID == (int)reader3["StudentID"])
            {
                found = true;
                student = true;
            }
        }*/
        while (reader1.Read() && found == false)
        {
            if (foundUsersID == (int)reader1["AdminID"])
            {
                found = true;
                admin = true;
            }
        }
        //Console.WriteLine(userFound);
        while (reader2.Read() && found == false)
        {
            if (foundUsersID == (int)reader2["TeacherID"])
            {
                found = true;
                teacher = true;
            }
        }
        //Console.WriteLine(userFound);
        while (reader3.Read() && found == false){
            if (foundUsersID == (int)reader3["StudentID"])
            {
                found = true;
                student = true;
            }
        }
        //Console.WriteLine(userFound);
        //Console.WriteLine(student);
        //Console.ReadKey();
    }
}