using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using TanuDapper.Interface;
using TanuDapper.Model;

namespace TanuDapper.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperDbContext _dapperDbContext;

        public EmployeeRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext ?? throw new ArgumentNullException(nameof(dapperDbContext));
        }

        public async Task<List<EmployeeModel>> GetEmployee()
        {
            string sqlquery = "sp_getemployees";
            using (var db = _dapperDbContext.CreateConnection())
            {
                var emp = await db.QueryAsync<EmployeeModel>(sqlquery, commandType: CommandType.StoredProcedure);
                return emp.ToList();
            }
        }

        public async Task<string> CreateEmployee(EmployeeModel employeeModel)
        {
            string sqlquery = "INSERT INTO Employees (Title,FirstName,LastName,Gender,Email,CompanyId)" +
                "values (@Title,@FirstName,@LastName,@Gender,@Email,@CompanyId)";
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("title", employeeModel.Title);
            parameters.Add("firstname", employeeModel.FirstName);
            parameters.Add("lastname", employeeModel.LastName);
            parameters.Add("gender", employeeModel.Gender);
            parameters.Add("email", employeeModel.Email);
            parameters.Add("companyid", employeeModel.CompanyId);
            using (var db = _dapperDbContext.CreateConnection())
            {
                await db.ExecuteAsync(sqlquery, parameters);
                response = "Complete Success";
            }
            return response;
        }

        public async Task<string> UpdateEmployee(int id, EmployeeModel employeeModel)
        {
            string sqlquery = "UPDATE Employees SET Title=@Title, FirstName=@FirstName, " +
                "LastName=@LastName, Gender=@Gender, Email=@Email, CompanyId=@CompanyId where id=@id";
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            parameters.Add("title", employeeModel.Title);
            parameters.Add("firstname", employeeModel.FirstName);
            parameters.Add("lastname", employeeModel.LastName);
            parameters.Add("gender", employeeModel.Gender);
            parameters.Add("email", employeeModel.Email);
            parameters.Add("companyid", employeeModel.CompanyId);
            using (var db = _dapperDbContext.CreateConnection())
            {
                await db.ExecuteAsync(sqlquery, parameters);
                response = "Updated Succsfully";
            }
            return response;
        }


        public async Task<string> DeleteEmployee(int id)
        {
            string sqlquery = "DELETE FROM Employees WHERE Id=@Id";
            string response = string.Empty;
            using (var db = _dapperDbContext.CreateConnection())
            {
                await db.ExecuteAsync(sqlquery, new { id });
                response = "Deleted Successfully";
            }
            return response;
        }

        public async Task<List<EmployeeModel>> GetByName(string FirstName)
        {
            string sqlquery = "SELECT * FROM Employees WHERE FirstName=@FirstName";
            using (var db = _dapperDbContext.CreateConnection())
            {
                var emp = await db.QueryAsync<EmployeeModel>(sqlquery, new { FirstName });
                return emp.ToList();
            }
        }
    }
}
