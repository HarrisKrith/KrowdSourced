using Crowdfunding.Data;
using Crowdfunding.Models;
using Crowdfunding.Services.IServices;

namespace Crowdfunding.Services
{
    public class UserSponsorService : IUserSponsorService
    {
        private readonly CrowdfundingDbContext _dbContext;

        public UserSponsorService(CrowdfundingDbContext context)
        {
            _dbContext = context;
        }

        public void AddSponsor(UserSponsor userSponsor)
        {
            _dbContext.Add(userSponsor);
            _dbContext.SaveChanges();
        }
    }
}
