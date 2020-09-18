using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using RefitDemo.Entities;

namespace RefitDemo.ApiInterfaces
{
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
}
