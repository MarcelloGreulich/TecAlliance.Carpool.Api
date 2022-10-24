using System.Reflection;
using System.Text;
using TecAlliance.Carpool.Data.Model;
using static System.Net.Mime.MediaTypeNames;

namespace TecAlliance.Carpool.Data.Services
{
    public class CarpoolDataServices
    {
        //Global
        string? DirectoryPath;

        public CarpoolDataServices()
        {
            this.DirectoryPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(CarpoolDataServices)).Location);
        }


        /// <summary>
        /// Get id for Carpools
        /// </summary>
        /// <param name="GetId"></param>
        public int GetId()
        {
            int id = 0;
            List<CarpoolModel> carpools = SaveCarpools();

            if (carpools != null)
            {
                foreach (var carpool in carpools)
                {
                    id = carpool.CarpoolId + 1;
                }

            }
            else
            {
                id = 0;
            }

            return id;
        }
        /// <summary>
        /// Reads all lines From carpoolList.csv and store them in a List
        /// </summary>
        /// <returns>List of CarpoolModels</returns>
        public List<CarpoolModel> SaveCarpools()
        {
            List<CarpoolModel> list = new List<CarpoolModel>();
            int id = 0;
            string[] lines = File.ReadAllLines($"{DirectoryPath}\\Csv\\Carpools.csv");
            foreach (string line in lines)
            {
                CarpoolModel carpool = new CarpoolModel();
                if (line == string.Empty)
                {
                    return null;
                }
                else
                {
                    UserInfo driver = new UserInfo();
                    string[] box = line.Split(';');
                    carpool.CarpoolId = id;
                    carpool.CarDesignation = box[1];
                    carpool.FreeSeat = Convert.ToInt32(box[2]);
                    carpool.StartPoint = box[3];
                    carpool.EndPoint = box[4];
                    carpool.DepartureTime = Convert.ToDateTime(box[5]);
                    driver.Id = Convert.ToInt32(box[6]);
                    driver.Name = "";
                    driver.IsDriver = true;
                    carpool.Drivers = driver;
                    List<UserInfo> pasgList = new List<UserInfo>();
                    for (int i = 7; i < box.Length; i++)
                    {
                        if (box[i] != "")
                        {
                            UserInfo passangerid = new UserInfo();
                            passangerid.Id = Convert.ToInt32(box[i]);
                            passangerid.Name = "";
                            passangerid.IsDriver = false;
                            pasgList.Add(passangerid);
                        }

                    }
                    carpool.Passengers = pasgList;
                    id++;
                }
                list.Add(carpool);
            }

            return list;
        }
        /// <summary>
        /// Get a List of CarpoolModels and writes them into carpoolList.csv
        /// </summary>
        /// <param name="carpoolList"></param>
        public void PostCarpool(List<CarpoolModel> carpoolList)
        {
            //Create File Stream
            FileStream fs = new FileStream($"{DirectoryPath}\\Csv\\Carpools.csv", FileMode.Create);
            foreach (var carpool in carpoolList)
            {
                string userString = $"{carpool.CarpoolId};{carpool.CarDesignation};{carpool.FreeSeat};{carpool.StartPoint};{carpool.EndPoint};{carpool.DepartureTime};{carpool.Drivers.Id};";
                //Prepare user string for writing
                byte[] buffer = Encoding.Default.GetBytes(userString);
                //Write user in UserList.csv
                fs.Write(buffer, 0, buffer.Length);
                if (carpool.Passengers != null)
                {
                    foreach (var item in carpool.Passengers)
                    {
                        userString = $"{item.Id};";
                        buffer = Encoding.Default.GetBytes(userString);
                        fs.Write(buffer, 0, buffer.Length);
                    }
                    buffer = Encoding.Default.GetBytes("\n");
                    fs.Write(buffer);
                }
            }
            //close and dispose File stream
            fs.Close();
            fs.Dispose();
        }
        /// <summary>
        /// Creates an empty carpoolList.csv file
        /// </summary>
        public void DeleteAllCarpools()
        {
            FileStream fs = new FileStream($"{DirectoryPath}\\Csv\\Carpools.csv", FileMode.Create);
            fs.Close();
            fs.Dispose();
        }
        /// <summary>
        /// Gets CarpoolModel, Removes it and Writes a new carpoolList.csv
        /// </summary>
        /// <param name="carpool"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteCarpoolsById(CarpoolModel carpool)
        {
            List<CarpoolModel> list = SaveCarpools();
            if (list.Count == 0)
            {
                throw new Exception("Dieser user Existiert nicht");
            }
            else
            {
                list.RemoveAt(carpool.CarpoolId);
                FileStream fs = new FileStream($"{DirectoryPath}\\Csv\\Carpools.csv", FileMode.Create);
                byte[] buffer = null;
                foreach (var l in list)
                {
                    string userString = $"{l.CarpoolId};{l.CarDesignation};{l.FreeSeat};{l.StartPoint};{l.EndPoint};{l.DepartureTime};{l.Drivers.Id};";
                    buffer = Encoding.Default.GetBytes(userString);
                    fs.Write(buffer);
                    if (carpool.Passengers != null)
                    {
                        foreach (var item in carpool.Passengers)
                        {
                            string str = $"{item.Id};";
                            buffer = Encoding.Default.GetBytes(str);
                            fs.Write(buffer, 0, buffer.Length);
                        }
                    }
                    buffer = Encoding.Default.GetBytes("\n");
                    fs.Write(buffer);
                }
                fs.Close();
                fs.Dispose();

            }
        }
        /// <summary>
        /// Gets an CarpoolModel and the user id
        /// it Removes the User from the Carpool and Writes new carpoolList.csv 
        /// </summary>
        /// <param name="carpool"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CarpoolModel LeaveCarpool(CarpoolModel carpool, int userId)
        {
            List<CarpoolModel> list = SaveCarpools();
            FileStream fs = new FileStream(@$"{DirectoryPath}\Carpools.csv", FileMode.Create);
            CarpoolModel model = new CarpoolModel();
            foreach (var l in list)
            {
                string userString = $"{carpool.CarpoolId};{carpool.CarDesignation};{carpool.FreeSeat};{carpool.StartPoint};{carpool.EndPoint};{carpool.DepartureTime};{carpool.Drivers.Id};";
                //Prepare user string for writing
                byte[] buffer = Encoding.Default.GetBytes(userString);
                //Write user in UserList.csv
                fs.Write(buffer, 0, buffer.Length);

                if (l.CarpoolId == carpool.CarpoolId)
                {
                    model.CarpoolId = carpool.CarpoolId;
                    model.CarDesignation = carpool.CarDesignation;
                    model.FreeSeat = carpool.FreeSeat;
                    model.StartPoint = carpool.StartPoint;
                    model.EndPoint = carpool.EndPoint;
                    model.DepartureTime = carpool.DepartureTime;
                    model.Drivers = carpool.Drivers;
                    List<UserInfo> info = new List<UserInfo>();
                    if (carpool.Passengers != null)
                    {
                        foreach (var item in carpool.Passengers)
                        {
                            if (item.Id != userId)
                            {
                                info.Add(item);
                                string srtin = $"{item.Id};";
                                buffer = Encoding.Default.GetBytes(srtin);
                                fs.Write(buffer, 0, buffer.Length);
                            }
                        }
                        buffer = Encoding.Default.GetBytes("\n");
                        fs.Write(buffer);
                    }
                    model.Passengers = info;
                }
                else
                {

                    if (carpool.Passengers != null)
                    {
                        foreach (var item in carpool.Passengers)
                        {
                            string srtin = $"{item.Id};";
                            buffer = Encoding.Default.GetBytes(srtin);
                            fs.Write(buffer, 0, buffer.Length);

                        }
                        buffer = Encoding.Default.GetBytes("\n");
                        fs.Write(buffer);
                    }
                }
            }
            fs.Close();
            fs.Dispose();
            return model;
        }
    }
}
