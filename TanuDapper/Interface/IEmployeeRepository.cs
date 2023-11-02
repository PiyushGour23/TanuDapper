﻿using TanuDapper.Model;

namespace TanuDapper.Interface
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> GetEmployee();
        Task<string> CreateEmployee(EmployeeModel employeeModel);
        Task<string> UpdateEmployee(int id, EmployeeModel employeeModel);
        Task<string> DeleteEmployee(int id);
        Task<List<EmployeeModel>> GetByName(string FirstName);
    }
}
