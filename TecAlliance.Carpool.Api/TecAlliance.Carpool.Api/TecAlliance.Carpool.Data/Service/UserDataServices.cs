using System.Reflection;
using System.Text;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Services
{
    public class UserDataServices
    {
        //Global
        string? DirectoryPath;

        public UserDataServices()
        {
            this.DirectoryPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(CarpoolDataServices)).Location);
        }

        //Daten Schreiben und ausgeben 
        public List<User> SaveUser()
        {
            List<User> list = new List<User>();
            string[] lines = File.ReadAllLines($"{DirectoryPath}\\Csv\\UserList.csv");
            foreach (string line in lines)
            {
                User user = new User();
                if (line == string.Empty)
                {
                    return list;
                }
                else
                {
                    string[] box = line.Split(';');

                    user.Id = Convert.ToInt32(box[0]);
                    user.Name = box[1];
                    user.Nachname = box[2];
                    user.Anmeldename = box[3];
                    user.Passwort = box[4];
                    user.Gender = box[5];
                    user.Alter = Convert.ToInt32(box[6]);
                    list.Add(user);
                }
            }

            return list;
        }

        public void AddUser(User user)
        {
            //Create File Stream
            FileStream fs = new FileStream($"{DirectoryPath}\\Csv\\UserList.csv", FileMode.Append);
            //Convert user to string
            string userString = $"{user.Id};{user.Name};{user.Nachname};{user.Anmeldename};{user.Passwort};{user.Gender};{user.Alter}; \n";
            //Prepare user string for writing
            byte[] buffer = Encoding.Default.GetBytes(userString);
            //Write user in UserList.csv
            fs.Write(buffer, 0, buffer.Length);
            //close and dispose File stream
            fs.Close();
            fs.Dispose();
        }

        public User RemoveUserById(User user, List<User> list)
        {
            if (list.Count == 0)
            {
                throw new Exception("Dieser user Existiert nicht");
            }
            else
            {
                list.RemoveAt(user.Id);
                FileStream fs = new FileStream($"{DirectoryPath}\\Csv\\UserList.csv", FileMode.Create);
                foreach (var l in list)
                {
                    string userString = $"{l.Id};{l.Name};{l.Nachname};{l.Anmeldename};{l.Passwort};{l.Gender};{l.Alter}; \n";
                    byte[] buffer = Encoding.Default.GetBytes(userString);
                    fs.Write(buffer);
                }
                fs.Close();
                fs.Dispose();
            }
            return user;
        }
        public void RemoveAllUser()
        { 
            FileStream fs = new FileStream($"{DirectoryPath}\\Csv\\UserList.csv", FileMode.Create);
            fs.Close();
            fs.Dispose();
        }
        
        public User ReplaceUserById(int id,User user ,List<User> list)
        {
            list[id] = user;
            FileStream fs = new FileStream($"{DirectoryPath}\\Csv\\UserList.csv", FileMode.Create);
            //Convert user to string
            foreach (var listUser in list)
            {
                string userString = $"{listUser.Id};{listUser.Name};{listUser.Nachname};{listUser.Anmeldename};{listUser.Passwort};{listUser.Gender};{listUser.Alter}; \n";
                //Prepare user string for writing
                byte[] buffer = Encoding.Default.GetBytes(userString);
                //Write user in UserList.csv
                fs.Write(buffer, 0, buffer.Length);
            }

            //close and dispose File stream
            fs.Close();
            fs.Dispose();
            return user;
        }
    }
}
