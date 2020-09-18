using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using RefitDemo.Entities;

namespace RefitDemo.ApiInterfaces
{
    public interface ICompanyUsersApi
    {
        [Post("/api/v1.1/users/authenticate")]
        Task<User> AuthenticateUser([Body] UserDto user);

        [Post("/api/v1.1/users/create")]
        [Headers("Authorization: Bearer")]
        Task<User> CreateUser([Body] User user);

        [Delete("/api/v1.1/users/{userName}")]
        [Headers("Authorization: Bearer")]
        Task DeleteUser(string userName);

        [Get("/api/v1.1/users/getall")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<User>> GetAllUsers();

        [Get("/api/v1.1/users/{userName}")]
        [Headers("Authorization: Bearer")]
        Task<User> GetUser(string userName);
    }
}
