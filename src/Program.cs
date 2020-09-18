using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using RefitDemo.ApiInterfaces;
using RefitDemo.Entities;

namespace RefitDemo
{
    internal class Program
    {
        private static async Task CallDepartmentsApi()
        {
            try
            {
                // Authenticate User and retrieve token
                var token = await GetAuthenticatedUserToken();

                //*****************
                // Departments API
                //*****************
                var departmentsApi = RestService.For<ICompanyDepartmentsApi>("http://localhost:52330",
                    new RefitSettings
                    {
                        AuthorizationHeaderValueGetter = () => Task.FromResult(token)
                    });
                var newDepartment = await CreateDepartment(departmentsApi, "MyTestDepartment");
                await GetAllDepartments(departmentsApi);
                await DeleteDepartment(departmentsApi, newDepartment.DepartmentId);
                await GetAllDepartments(departmentsApi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured: {ex.Message}");
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter for continue.");
            Console.ReadLine();
        }

        private static async Task CallUsersApi()
        {
            try
            {
                //***********
                // Users API
                //***********

                // Authenticate User and retrieve token
                var token = await GetAuthenticatedUserToken();

                // Pass token to Refit
                var usersApi = RestService.For<ICompanyUsersApi>("http://localhost:52330",
                    new RefitSettings
                    {
                        AuthorizationHeaderValueGetter = () => Task.FromResult(token)
                    });

                await CreateUser(usersApi, "mytestuser");
                await GetAllUsers(usersApi);
                await GetUser(usersApi, "mytestuser");
                await DeleteUser(usersApi, "mytestuser");
                await GetAllUsers(usersApi);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured: {ex.Message}");
            }
            Console.WriteLine();
            Console.WriteLine("Press Enter for Exit.");
            Console.ReadLine();
        }

        private static async Task<Department> CreateDepartment(ICompanyDepartmentsApi departmentsApi, string departmentName)
        {
            var newDepartment = new Department
            {
                Name = departmentName
            };

            Console.WriteLine();
            Console.WriteLine("*** CreateDepartment ***");
            var savedNewDepartment = await departmentsApi.CreateDepartment(newDepartment);
            Console.WriteLine(JsonConvert.SerializeObject(savedNewDepartment));
            return savedNewDepartment;
        }

        private static async Task CreateUser(ICompanyUsersApi usersApi, string userName)
        {
            var newCompany = new Company
            {
                Name = "Company TEST",
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };
            var newDepartment = new Department
            {
                Name = "Department TEST"
            };
            var newEmployee = new Employee
            {
                FirstName = "Silvio",
                LastName = "Holt",
                BirthDate = new DateTime(1995, 8, 7),
                Company = newCompany,
                Department = newDepartment,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };
            var newUser = new User
            {
                EmployeeId = newEmployee.EmployeeId,
                Employee = newEmployee,
                Username = userName,
                Password = "test",
                Token = string.Empty
            };

            Console.WriteLine();
            Console.WriteLine("*** CreateUser ***");
            var savedUser = await usersApi.CreateUser(newUser);
            Console.WriteLine(JsonConvert.SerializeObject(savedUser));
        }

        private static async Task DeleteDepartment(ICompanyDepartmentsApi departmentsApi, int departmentId)
        {
            Console.WriteLine();
            Console.WriteLine("*** DeleteDepartment ***");
            await departmentsApi.DeleteDepartment(departmentId);
        }

        private static async Task DeleteUser(ICompanyUsersApi usersApi, string userName)
        {
            Console.WriteLine();
            Console.WriteLine("*** DeleteUser ***");
            await usersApi.DeleteUser(userName);
        }

        private static async Task GetAllDepartments(ICompanyDepartmentsApi departmentsApi)
        {
            Console.WriteLine("*** GetAllDepartments ***");
            var allDepartments = await departmentsApi.GetAllDepartments();
            foreach (var dep in allDepartments)
            {
                Console.WriteLine(JsonConvert.SerializeObject(dep));
            }
        }

        private static async Task GetAllUsers(ICompanyUsersApi usersApi)
        {
            Console.WriteLine("*** GetAllUsers ***");
            var allUsers = await usersApi.GetAllUsers();
            foreach (var usr in allUsers)
            {
                Console.WriteLine(JsonConvert.SerializeObject(usr));
            }
        }

        private static async Task<string> GetAuthenticatedUserToken()
        {
            var usersAuthenticationApi = RestService.For<ICompanyUsersApi>("http://localhost:52330");
            var user = new UserDto {Username = "johnw", Password = "test"};
            var authenticatedUser = await usersAuthenticationApi.AuthenticateUser(user);
            return authenticatedUser.Token;
        }
 
        private static async Task GetUser(ICompanyUsersApi usersApi, string userName)
        {
            Console.WriteLine();
            Console.WriteLine("*** GetUser ***");
            var existingUser = await usersApi.GetUser(userName);
            Console.WriteLine(JsonConvert.SerializeObject(existingUser));
        }
 
        private static void Main()
        {
            CallDepartmentsApi().Wait();
            CallUsersApi().Wait();
        }
    }
}