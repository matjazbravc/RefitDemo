using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using RefitDemo.Entities;

namespace RefitDemo.ApiInterfaces
{
    public interface ICompanyUsersApi
    {
        [Post("/api/users/authenticate")]
        Task<User> AuthenticateUser([Body] UserDto user);

        [Post("/api/users/create")]
        [Headers("Authorization: Bearer")]
        Task<User> CreateUser([Body] User user);

        [Delete("/api/users/{userName}")]
        [Headers("Authorization: Bearer")]
        Task DeleteUser(string userName);

        [Get("/api/users/getall")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<User>> GetAllUsers();

        [Get("/api/users/{userName}")]
        [Headers("Authorization: Bearer")]
        Task<User> GetUser(string userName);
    }
}
