using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using RefitDemo.Entities;

namespace RefitDemo.ApiInterfaces
{
    public interface ICompanyDepartmentsApi
    {
        [Post("/api/departments/create")]
        Task<Department> CreateDepartment([Body] Department department);

        [Delete("/api/departments/{id}")]
        Task DeleteDepartment(int id);

        [Get("/api/departments/getall")]
        Task<IEnumerable<DepartmentDto>> GetAllDepartments();

        [Get("/api/departments/{id}")]
        Task<Department> GetDepartment(int id);
    }
}
