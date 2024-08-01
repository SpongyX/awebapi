using Microsoft.EntityFrameworkCore;

namespace awebapi.Services
{
    public class TestService
    {
        readonly HealthDbContext _healthDbContext;
        public TestService(HealthDbContext healthDbContext)
        {
            _healthDbContext = healthDbContext;
        }

        public async Task<List<Users>> FetchAllAsync()
        {
            try
            {
                return await _healthDbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all users", ex);
            }
        }
    }
}