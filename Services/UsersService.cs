using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace awebapi.Services
{
    public class UsersService 
    {
        readonly HealthDbContext _healthDbContext;
        public UsersService(HealthDbContext healthDbContext)
        {
            _healthDbContext = healthDbContext;
        }

        public Task<bool> CheckIfExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> CreateNewAsync(Users model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Users>> FetchAllAsync()
        {
            try
            {
                var allUsers = await _healthDbContext.Users.ToListAsync();
                return allUsers;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all users", ex);
            }
        }

        public Task<Users?> FetchByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IfContainAsync(string model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Users> UpdateRecordAsync(Users model)
        {
            throw new NotImplementedException();
        }
    }
}