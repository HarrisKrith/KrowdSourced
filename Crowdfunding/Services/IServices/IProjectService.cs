using Crowdfunding.Models;
using Crowdfunding.Services.Options.ProjectOptions;
using System.Collections.Generic;
using System.Linq;

namespace Crowdfunding.Services.IServices
{
    public interface IProjectService
    {
        IQueryable<Project> SearchProject(SearchProjectOptions options);
        bool UpdateProject(Project project);
        Project ProjectById(int id);
        IEnumerable<Project> AllProjects();
        IEnumerable<Project> TrendsAmountDonated();
        IEnumerable<Project> TrendsCategory(Category category);
        void AddProject(Project project);
        bool DonationProject(DonationProjectOptions donationProjectOptions);
        void SaveDaChanges();
    }
}
