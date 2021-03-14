using Crowdfunding.Data;
using Crowdfunding.Models;
using Crowdfunding.Services.IServices;

namespace Crowdfunding.Services
{
    public class UserService : IUserService
    {
        private readonly CrowdfundingDbContext _dbContext;
        public UserService(CrowdfundingDbContext context)
        {
            _dbContext = context;
        }

        public ApplicationUser UserById(string id)
        {
            var user = _dbContext.ApplicationUsers.Find(id);

            return user;
        }
    }
}
