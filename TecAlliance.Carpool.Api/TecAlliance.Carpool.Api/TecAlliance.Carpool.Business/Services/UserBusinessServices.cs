using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data.Services;

namespace TecAlliance.Carpool.Business.Services
{
    //Prüfen
    public class UserBusinessServices
    {
        //Global
        UserDataServices dataServices;

        //Constructor
        public UserBusinessServices()
        {
            dataServices = new UserDataServices();
        }
        //Adds user to UserList.csv
        public void AddUser(UserDto user)
        {
            //Add converted user to DataServices
            user.Id = GetId();
            dataServices.AddUser(ConvertIntoUser(user));
        }
        //Converts UserDto to User Model from DataServices
        public User ConvertIntoUser(UserDto user)
        {

            User newUser = new User();
            newUser.Id = user.Id;
            newUser.Name = user.Name;
            newUser.Nachname = user.Nachname;
            newUser.Anmeldename = user.Anmeldename;
            newUser.Passwort = user.Passwort;
            newUser.Gender = user.Gender;
            newUser.Alter = user.Alter;

            return newUser;
        }
        //Converts User to UserDto Model from DataServices
        public UserDto ConvertIntoUserDto(User user)
        {
            UserDto newUser = new UserDto();
            newUser.Id = user.Id;
            newUser.Name = user.Name;
            newUser.Nachname = user.Nachname;
            newUser.Anmeldename = user.Anmeldename;
            newUser.Passwort = user.Passwort;
            newUser.Gender = user.Gender;
            newUser.Alter = user.Alter;

            return newUser;
        }
        //Get free user id of UserList.csv
        private int GetId()
        {
            int id = 0;
            List<User> users = dataServices.SaveUser();

            foreach (var user in users)
            {
                id = user.Id + 1;
            }

            return id;
        }

        //returns User with right id
        public UserDto GetUserdtoById(int inputId)
        {
            List<User> users = dataServices.SaveUser();

            foreach (var user in users)
            {
                if (user.Id == inputId)
                {
                    //Converting user in UsterDto
                    return ConvertIntoUserDto(user);
                }
            }
            return null;
        }
        //Get all users from UserList.csv
        public List<UserDto> GetAllUsers()
        {
            List<User> users = dataServices.SaveUser();
            List<UserDto> newUsers = new List<UserDto>();
            foreach (var user in users)
            {
                User convertUser = new User();
                convertUser = user;
                newUsers.Add(ConvertIntoUserDto(convertUser));
            }
            return newUsers;
        }

        //Checks if Input Id is in Userlist
        public bool FindUserDtoId(int inputId)
        {
            List<User> users = dataServices.SaveUser();
            bool idTrue = false;
            foreach (var line in users)
            {
                if (inputId == line.Id)
                {
                    idTrue = true;
                    return idTrue;
                }
            }
            return false;
        }
        //Remove User By id
        public UserDto RemoveUserById(int id)
        {
            List<User> list = dataServices.SaveUser();
            var userItem = GetUserdtoById(id);
            var userConvert = dataServices.RemoveUserById(ConvertIntoUser(userItem), list);
            return ConvertIntoUserDto(userConvert);
        }
        //Remove all User
        public void RemoveAllUser()
        {
            dataServices.RemoveAllUser();
        }
        //Replace User by Id
        public UserDto ReplaceUserById(int id, UserDto userDto)
        {
            //set userDto Id from 0 to id
            userDto.Id = id;
            //Get user from given id
            bool UserDtoId = FindUserDtoId(id);
            //Get List of current users
            List<User> list = dataServices.SaveUser();
            User user = new User();
            //Test if user exist
            if (UserDtoId)
            {
                //Converts userdto to user model and gives List to ReplaceUserById from dataService
                user = dataServices.ReplaceUserById(id, ConvertIntoUser(userDto), list);
            }
            return ConvertIntoUserDto(user);
        }

    }
}
