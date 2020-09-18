# Refit RESTful library POC with ASP.NET Core 3.1

Instead of you make your own HttpClient calls (which is a little bit low level and somewhat annoying) you can use [Refit](https://github.com/reactiveui/refit), an automatic type-safe REST library for .NET Core and Xamarin. It makes it easy to just declare the methods of a client and its associated REST API with a C# interfaces like this:

```csharp
public interface ICompanyDepartmentsApi
    {
        [Post("/api/v1.1/departments/create")]
        [Headers("Authorization: Bearer")]
        Task<Department> CreateDepartment([Body] Department department);

        [Delete("/api/v1.1/departments/{id}")]
        [Headers("Authorization: Bearer")]
        Task DeleteDepartment(int id);

        [Get("/api/v1.1/departments/getall")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<DepartmentDto>> GetAllDepartments();

        [Get("/api/v1.1/departments/{id}")]
        [Headers("Authorization: Bearer")]
        Task<Department> GetDepartment(int id);
    }
```
and then ask for an HttpClient that communicate that API's interface, calling it. Simply amazing.

```csharp
...
private static async Task<string> GetAuthenticatedUserToken()
{
    var usersAuthenticationApi = RestService.For<ICompanyUsersApi>("http://localhost:52330");
    var user = new UserDto {Username = "johnw", Password = "test"};
    var authenticatedUser = await usersAuthenticationApi.AuthenticateUser(user);
    return authenticatedUser.Token;
}
...

// Authenticate User and retrieve token
var token = await GetAuthenticatedUserToken();

// Pass token to Refit
var usersApi = RestService.For<ICompanyUsersApi>("http://localhost:52330",
    new RefitSettings
    {
        AuthorizationHeaderValueGetter = () => Task.FromResult(token)
    });
    
var allUsers = await usersApi.GetAllUsers();
foreach (var usr in allUsers)
{
    Console.WriteLine(JsonConvert.SerializeObject(usr));
}    
```
## What about HttpClientFactory?
Refit has now support for the ASP.Net Core 3.x **HttpClientFactory**. Add a reference to Refit.HttpClientFactory and call the provided extension method in your **ConfigureServices** method to configure your Refit interface:
```csharp
// Configure Refit settings here
var settings = new RefitSettings(); 

services.AddRefitClient<IWebApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:52330"));
        // Add additional IHttpClientBuilder chained methods as required here:
        // .AddHttpMessageHandler<MyHandler>()
        // .SetHandlerLifetime(TimeSpan.FromMinutes(5));
```
With Refit is really easy to make a quick strongly-typed REST Client for pretty much any API. Read more about it [here](https://reactiveui.github.io/refit/).

## Prerequisites
- [Visual Studio](https://www.visualstudio.com/vs/community) 2019 16.4.5 or greater
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Refit RESTful Library for .NET](https://github.com/reactiveui/refit)

****NOTE: To make this demo up and running you have to load and run my [OpenAPI.Swagger.Demo](https://github.com/matjazbravc/OpenAPI.Swagger.Demo) solution.**

Go get Refit and play with it today. Enjoy!

## Licence
Licenced under [MIT](http://opensource.org/licenses/mit-license.php).
Contact me on [LinkedIn](https://si.linkedin.com/in/matjazbravc).
