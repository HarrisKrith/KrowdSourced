using Crowdfunding.Data;
using Crowdfunding.Models;
using Crowdfunding.Services.IServices;

namespace Crowdfunding.Services
{
    public class DonationService : IDonationService
    {
        private readonly CrowdfundingDbContext _dbContext;
        private readonly IProjectService _projectService;

        public DonationService(CrowdfundingDbContext context, IProjectService projectService)
        {
            _dbContext = context;
            _projectService = projectService;
        }

        public void AddDonation(Donation donation)
        {
            _dbContext.Add(donation);
            _dbContext.SaveChanges();
        }
    }
}
