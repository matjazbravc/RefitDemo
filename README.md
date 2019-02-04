# Refit RESTful library POC with ASP.NET Core 2.2

Instead of you make your own HttpClient calls (which is a little bit low level and somewhat annoying) you can use [Refit](https://github.com/reactiveui/refit), an automatic type-safe REST library for .NET Core and Xamarin. It makes it easy to just declare the methods of a client and its associated REST API with a C# interfaces like this:

```csharp
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
```
and then ask for an HttpClient that communicate that API's interface, calling it. Simply amazing.

```csharp
// Authenticate User and retrieve token
var token = await GetAuthenticatedUserToken();

// Pass token to Refit
var usersApi = RestService.For<ICompanyUsersApi>("http://localhost:5002",
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
Refit has now support for the ASP.Net Core 2.x **HttpClientFactory**. Add a reference to Refit.HttpClientFactory and call the provided extension method in your **ConfigureServices** method to configure your Refit interface:
```csharp
// Configure Refit settings here
var settings = new RefitSettings(); 

services.AddRefitClient<IWebApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5002"));
        // Add additional IHttpClientBuilder chained methods as required here:
        // .AddHttpMessageHandler<MyHandler>()
        // .SetHandlerLifetime(TimeSpan.FromMinutes(5));
```
With Refit is really easy to make a quick strongly-typed REST Client for pretty much any API. Read more about it [here](https://reactiveui.github.io/refit/).

## Prerequisites:
- [Visual Studio](https://www.visualstudio.com/vs/community) 2017 15.9 or greater
- [.NET Core SDK 2.2.102](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- [Refit RESTful Library for .NET](https://github.com/reactiveui/refit)

**P.S. To make this POC up and running you have to load and run my [AspNetCore-Api-Demo](https://github.com/matjazbravc/AspNetCore-Api-Demo) solution.**

Go get Refit and play with it today!

## Licence

Licenced under [MIT](http://opensource.org/licenses/mit-license.php).
Developed by [Matjaž Bravc](https://si.linkedin.com/in/matjazbravc)