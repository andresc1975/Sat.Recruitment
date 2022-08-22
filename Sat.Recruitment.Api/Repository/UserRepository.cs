using Sat.Recruitment.Api.Domain;
using Sat.Recruitment.Api.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string UsersFilePath = "/Files/Users.txt";
        private List<User> _users;

        public UserRepository() 
        {
            _users = ReadUsersFromFile().Result;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        private string GetUsersFilePath()
        {
            return Directory.GetCurrentDirectory() + UsersFilePath;
        }

        private async Task<List<User>> ReadUsersFromFile()
        {

            FileStream fileStream = new FileStream(GetUsersFilePath(), FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);

            var users = new List<User>();
            while (reader.Peek() >= 0)
            {
                var row = await reader.ReadLineAsync();
                var user = UserFromFileRow(row);
                users.Add(user);
            }
            reader.Close();

            return users;
        }

        private User UserFromFileRow(string fileRow)
        {
            var rawUserType = fileRow.Split(',')[4].ToString();
            return new User {
                Name = fileRow.Split(',')[0].ToString(),
                Email = fileRow.Split(',')[1].ToString(),
                Phone = fileRow.Split(',')[2].ToString(),
                Address = fileRow.Split(',')[3].ToString(),
                UserType = UserTypeConverter.UserTypeFromString(rawUserType),
                Money = decimal.Parse(fileRow.Split(',')[5].ToString())
            };
        }

    }
}
