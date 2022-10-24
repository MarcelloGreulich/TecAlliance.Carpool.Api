using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Model;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Business.Services
{
    public class CarpoolBusinessServices
    {
        //Global
        UserDataServices userDataServices;
        UserBusinessServices userBusinessServices;
        CarpoolDataServices carpoolDataServices;
        //Construktor
        public CarpoolBusinessServices()
        {
            userDataServices = new UserDataServices();
            userBusinessServices = new UserBusinessServices();
            carpoolDataServices = new CarpoolDataServices();
        }
        /// <summary>
        /// Gets user by Id, Converts it to UserInfoDto
        /// Creates an Carpool  
        /// hands the carpool over to carpoolDataServices
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carpoolDto"></param>
        /// <param name="isDriver"></param>
        public void PostCarpool(int id, CarpoolDto carpoolDto, bool isDriver)
        {
            UserDto userDto = userBusinessServices.GetUserdtoById(id);
            UserInoDto userInfoDto = ConvertIntoUserInfoDto(userDto, isDriver);
            List<CarpoolModel> carpool = CreateCarpool(carpoolDto, userInfoDto);
            carpoolDataServices.PostCarpool(carpool);
        }
        /// <summary>
        /// Gets Carpool from user 
        /// Gets curent Carpool list
        /// Converts List from user
        /// add both Lists together
        /// hand the List over to carpoolDataServices
        /// </summary>
        /// <param name="carpool"></param>
        public void AddCarpool(CarpoolDtoWithUserInformation carpool)
        {
            List<CarpoolModel> carpoolModelList = new List<CarpoolModel>();
            List<CarpoolModel> list = carpoolDataServices.SaveCarpools();
            CarpoolModel carpoolModel = ConvertToCarpoolModel(carpool);
            list.Add(carpoolModel);
            carpoolDataServices.PostCarpool(list);
        }
        /// <summary>
        /// get UserDto and IsDriver bool
        /// Converts UserDto to User InoDto
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="idDriver"></param>
        /// <returns>userInodto</returns>
        public UserInoDto ConvertIntoUserInfoDto(UserDto userDto, bool idDriver)
        {
            UserInoDto userInoDto = new UserInoDto();
            userInoDto.Id = userDto.Id;
            userInoDto.Name = userDto.Name;
            userInoDto.IsDriver = idDriver;
            return userInoDto;
        }
        /// <summary>
        /// Get UserInoDto
        /// Converts it to UserInfo
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>userIndo</returns>
        public UserInfo ConvertIntoUserInfo(UserInoDto userDto)
        {
            UserInfo userInoDto = new UserInfo();
            userInoDto.Id = userDto.Id;
            userInoDto.Name = userDto.Name;
            userInoDto.IsDriver = userDto.IsDriver;
            return userInoDto;
        }
        /// <summary>
        /// Get List of current Carpools
        /// Get carpool from user
        /// Checks if user is driver
        /// if user is driver create a new carpool
        /// if user is not a Driver it add user to Carpool
        /// Creates List of CarpoolModels
        /// </summary>
        /// <param name="carpoolDto"></param>
        /// <param name="userDto"></param>
        /// <returns>list of CarpoolModels</returns>
        public List<CarpoolModel> CreateCarpool(CarpoolDto carpoolDto, UserInoDto userDto)
        {
            List<CarpoolModel> carpoollList = new List<CarpoolModel>();
            if (userDto.IsDriver)
            {
                CarpoolModel carpool = new CarpoolModel();
                List<CarpoolModel> carpoolModels = carpoolDataServices.SaveCarpools();
                if (carpoolModels != null)
                {
                    foreach (var item in carpoolModels)
                    {
                        carpoollList.Add(item);
                    }
                }
                carpool.CarpoolId = carpoolDataServices.GetId();
                carpool = convertAllInfo(carpoolDto, userDto, carpool);
                carpoollList.Add(carpool);
            }
            else
            {
                CarpoolModel carpool = new CarpoolModel();

                foreach (var item in carpoolDataServices.SaveCarpools())
                {
                    //&& carpoolDto.DepartureTime.ToLongTimeString() == item.DepartureTime.ToShortTimeString() 
                    if (carpoolDto.StartPoint == item.StartPoint && carpoolDto.EndPoint == item.EndPoint && item.FreeSeat > 0)
                    {
                        item.FreeSeat--;
                        item.Passengers.Add(ConvertIntoUserInfo(userDto));
                        carpoollList.Add(item);
                    }
                    else
                    {
                        carpoollList.Add(item);
                    }
                }
            }
            return carpoollList;
        }
        /// <summary>
        /// gets carpoolDto
        /// gets userDto
        /// gets CarpoolModel
        /// Adds all info to CarpoolModel
        /// </summary>
        /// <param name="carpoolDto"></param>
        /// <param name="userDto"></param>
        /// <param name="carpool"></param>
        /// <returns>Carpoolmodel</returns>
        public CarpoolModel convertAllInfo(CarpoolDto carpoolDto, UserInoDto userDto, CarpoolModel carpool)
        {
            carpool.CarDesignation = carpoolDto.CarDesignation;
            carpool.FreeSeat = carpoolDto.FreeSeat;
            carpool.StartPoint = carpoolDto.StartPoint;
            carpool.EndPoint = carpoolDto.EndPoint;
            carpool.DepartureTime = carpoolDto.DepartureTime;
            carpool.Drivers = ConvertIntoUserInfo(userDto);
            List<CarpoolModel> list = carpoolDataServices.SaveCarpools();
            if (list != null)
            {
                foreach (var item in list)
                {
                    List<UserInfo> userInfoList = new List<UserInfo>();
                    UserInfo userInfotest = new UserInfo();
                    List<UserInfo> passanger = new List<UserInfo>();
                    UserInfo passangertest = new UserInfo();
                    if (item.CarpoolId == carpool.CarpoolId)
                    {
                        carpool.Drivers = item.Drivers;
                        userInfotest = ConvertIntoUserInfo(userDto);
                        passanger = item.Passengers;
                        if (passanger != null)
                        {
                            foreach (var pesItem in passanger)
                            {
                                passangertest.Id = pesItem.Id;
                                passangertest.Name = pesItem.Name;
                                passangertest.IsDriver = pesItem.IsDriver;
                                userInfoList.Add(passangertest);
                            }
                        }

                        userInfoList.Add(userInfotest);
                    }
                    carpool.Passengers = userInfoList;
                }
            }
            return carpool;
        }
        /// <summary>
        /// Converts userDto into driver
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public UserInoDto ConvertIntoUserInfoDtoDrivcer(UserInfo userDto)
        {
            UserInoDto userInoDto = new UserInoDto();
            userInoDto.Id = userDto.Id;
            userInoDto.Name = userDto.Name;
            userInoDto.IsDriver = userDto.IsDriver;
            return userInoDto;
        }
        /// <summary>
        /// Gets all carpool from carpoolDataServices and converts the into carpoolDto With User Information List
        /// </summary>
        /// <returns>carpoolDtoWithUserInformationList</returns>
        public List<CarpoolDtoWithUserInformation> GetAllCarpools()
        {
            List<CarpoolDtoWithUserInformation> carpoolDtoWithUserInformationList = new List<CarpoolDtoWithUserInformation>();
            List<CarpoolModel> carpools = carpoolDataServices.SaveCarpools();
            foreach (var carpool in carpools)
            {
                CarpoolDtoWithUserInformation carpoolDtoWithUserInformation = new CarpoolDtoWithUserInformation();
                UserInoDto driver = new UserInoDto();
                List<UserInoDto> carpoolUserInoDtoTempList = new List<UserInoDto>();
                carpoolDtoWithUserInformation.CarpoolId = carpool.CarpoolId;
                carpoolDtoWithUserInformation.CarDesignation = carpool.CarDesignation;
                carpoolDtoWithUserInformation.FreeSeat = carpool.FreeSeat;
                carpoolDtoWithUserInformation.StartPoint = carpool.StartPoint;
                carpoolDtoWithUserInformation.EndPoint = carpool.EndPoint;
                carpoolDtoWithUserInformation.DepartureTime = carpool.DepartureTime;
                driver.Id = carpool.Drivers.Id;
                UserDto driverName = new UserDto();
                driver.Name = carpool.Drivers.Name;
                driverName = userBusinessServices.GetUserdtoById(driver.Id);
                driver.Name = driverName.Name;
                carpoolDtoWithUserInformation.Drivers = driver;
                if (carpool.Passengers != null)
                {
                    foreach (var item in carpool.Passengers)
                    {
                        UserInoDto user = new UserInoDto();
                        user.Id = item.Id;
                        UserDto userDto = new UserDto();
                        userDto = userBusinessServices.GetUserdtoById(user.Id);
                        user.Name = userDto.Name;
                        user.IsDriver = item.IsDriver;
                        carpoolUserInoDtoTempList.Add(user);
                        carpoolDtoWithUserInformation.Passengers = carpoolUserInoDtoTempList;
                    }
                }


                carpoolDtoWithUserInformationList.Add(carpoolDtoWithUserInformation);
            }
            return carpoolDtoWithUserInformationList;
        }
        /// <summary>
        /// Gets CarpoolId from user
        /// Gets all carpool from carpoolDataServices
        /// Returns Carpool with same Carpool Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CarpoolDtoWithUserInformation GetCarpoolById(int id)
        {
            CarpoolDtoWithUserInformation carpoolDtoWithUserInformation = new CarpoolDtoWithUserInformation();
            List<CarpoolModel> carpools = carpoolDataServices.SaveCarpools();
            foreach (var carpool in carpools)
            {
                if (carpool.CarpoolId == id)
                {
                    UserInoDto driver = new UserInoDto();
                    List<UserInoDto> carpoolUserInoDtoTempList = new List<UserInoDto>();
                    carpoolDtoWithUserInformation.CarpoolId = carpool.CarpoolId;
                    carpoolDtoWithUserInformation.CarDesignation = carpool.CarDesignation;
                    carpoolDtoWithUserInformation.FreeSeat = carpool.FreeSeat;
                    carpoolDtoWithUserInformation.StartPoint = carpool.StartPoint;
                    carpoolDtoWithUserInformation.EndPoint = carpool.EndPoint;
                    carpoolDtoWithUserInformation.DepartureTime = carpool.DepartureTime;
                    driver.Id = carpool.Drivers.Id;
                    UserDto driverName = new UserDto();
                    driver.Name = carpool.Drivers.Name;
                    driverName = userBusinessServices.GetUserdtoById(driver.Id);
                    driver.Name = driverName.Name;
                    carpoolDtoWithUserInformation.Drivers = driver;
                    if (carpool.Passengers != null)
                    {
                        foreach (var item in carpool.Passengers)
                        {
                            UserInoDto user = new UserInoDto();
                            user.Id = item.Id;
                            UserDto userDto = new UserDto();
                            userDto = userBusinessServices.GetUserdtoById(user.Id);
                            user.Name = userDto.Name;
                            user.IsDriver = item.IsDriver;
                            carpoolUserInoDtoTempList.Add(user);
                            carpoolDtoWithUserInformation.Passengers = carpoolUserInoDtoTempList;
                        }
                    }
                }
                
            }
            return carpoolDtoWithUserInformation;
        }
        /// <summary>
        /// hand over to carpoolDataServices
        /// </summary>
        public void DeleteAllCarpools()
        {
            carpoolDataServices.DeleteAllCarpools();
        }
        /// <summary>
        /// gets CarpoolDtoWithUserInformation an Converts it to CarpoolModel
        /// </summary>
        /// <param name="carpool"></param>
        /// <returns></returns>
        public CarpoolModel ConvertToCarpoolModel(CarpoolDtoWithUserInformation carpool)
        {
            List<UserInfo> carpoolUserInoDtoTempList = new List<UserInfo>();
            CarpoolModel carpoolModel = new CarpoolModel();
            carpoolModel.CarpoolId = carpoolDataServices.GetId();
            carpoolModel.CarDesignation = carpool.CarDesignation;
            carpoolModel.FreeSeat = carpool.FreeSeat;
            carpoolModel.StartPoint = carpool.StartPoint;
            carpoolModel.EndPoint = carpool.EndPoint;
            carpoolModel.DepartureTime = carpool.DepartureTime;
            UserInfo driver = new UserInfo();
            UserDto driverName = new UserDto();
            driver.Name = carpool.Drivers.Name;
            driverName = userBusinessServices.GetUserdtoById(driver.Id);
            driver.Name = driverName.Name;
            carpoolModel.Drivers = driver;
            if (carpool.Passengers != null)
            {
                foreach (var item in carpool.Passengers)
                {
                    UserInfo user = new UserInfo();
                    user.Id = item.Id;
                    UserDto userDto = new UserDto();
                    userDto = userBusinessServices.GetUserdtoById(user.Id);
                    user.Name = userDto.Name;
                    user.IsDriver = item.IsDriver;
                    carpoolUserInoDtoTempList.Add(user);
                    carpoolModel.Passengers = carpoolUserInoDtoTempList;
                }
            }
        
            return carpoolModel;
        }
        /// <summary>
        /// Gtets carpoolId from user
        /// Gets CarpoolWithUserInformation by id and Converts it to CarpoolModel
        /// hand CarpoolModel over to carpoolDataServices
        /// </summary>
        /// <param name="carpoolId"></param>
        public void DeleteCarpoolsId(int carpoolId)
        {
            CarpoolModel carpool = ConvertToCarpoolModel(GetCarpoolById(carpoolId));
            carpoolDataServices.DeleteCarpoolsById(carpool);       
        }
        /// <summary>
        /// Gets carpoolId and userId from user
        /// Gets CarpoolWithUserInformation by id and Converts it to CarpoolModel
        /// hand CarpoolModel and userId over to carpoolDataServices
        /// </summary>
        /// <param name="carpoolId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CarpoolModel LeaveCarpool(int carpoolId, int userId)
        {
            CarpoolModel carpool = ConvertToCarpoolModel(GetCarpoolById(carpoolId));
            return carpoolDataServices.LeaveCarpool(carpool, userId);
        }
    }
}
