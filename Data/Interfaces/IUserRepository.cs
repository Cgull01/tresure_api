using API_tresure.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace tresure_api.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(string id);

        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();

    }
}
