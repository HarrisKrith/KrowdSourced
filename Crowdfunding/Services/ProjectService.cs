using Crowdfunding.Data;
using Crowdfunding.Models;
using Crowdfunding.Services.IServices;
using Crowdfunding.Services.Options.ProjectOptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crowdfunding.Services
{
    public class ProjectService : IProjectService
    {

        private readonly CrowdfundingDbContext _dbContext;

        public ProjectService(CrowdfundingDbContext context)
        {
            _dbContext = context;
        }

        public void AddProject(Project project)
        {
            _dbContext.Add(project);
        }
        public IEnumerable<Project> AllProjects()
        {
            var projects = _dbContext.Set<Project>()
                .Include(u => u.UserCreator)
                .Include(donation => donation.Donations)
                .ToList();


            projects.ForEach(p => p.Description = ExtentionMethods.StripTagsCharArray(p.Description));

            return projects;

        }
        public IEnumerable<Project> TrendsAmountDonated()
        {
            var projects = _dbContext.Set<Project>()
                .Include(u => u.UserCreator)
                .OrderByDescending(p => p.CurrentMoney)
                .ToList();

            projects.ForEach(p => p.Description = ExtentionMethods.StripTagsCharArray(p.Description));

            return projects;
        }
        public IEnumerable<Project> TrendsCategory(Category category)
        {
            var projects = _dbContext.Set<Project>()
                .Include(u => u.UserSponsors)
                .Where(p => p.Category == category)
                .OrderByDescending(p => p.CurrentMoney)
                .ToList();

            projects.ForEach(p => p.Description = ExtentionMethods.StripTagsCharArray(p.Description));

            return projects;
        }
        public Project ProjectById(int projectid)
        {
            var project = SearchProject(new SearchProjectOptions
            {
                ProjectId = projectid
            }).SingleOrDefault();

            return project;
        }
        public IQueryable<Project> SearchProject(SearchProjectOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = AllProjects()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                query = query.Where(p => p.Title == options.Title);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(p => p.Description == options.Description);
            }

            if (options.ProjectId != null)
            {
                query = query.Where(p => p.ProjectId == options.ProjectId);
            }

            if (options.DateCreatedFrom != null)
            {
                query = query.Where(p => p.DateCreated >= options.DateCreatedFrom);
            }

            if (options.DateCreatedTo != null)
            {
                query = query.Where(p => p.DateCreated <= options.DateCreatedTo);
            }

            if (options.GoalMoney != null)
            {
                query = query.Where(p => p.GoalMoney >= options.GoalMoney.Value);
            }

            if (options.CurrentMoney != null)
            {
                query = query.Where(p => p.CurrentMoney >= options.CurrentMoney.Value);
            }
            if (!string.IsNullOrWhiteSpace(options.StringToFind))
            {
                query = query.Where(p => p.Title.ToLower().Contains(options.StringToFind.ToLower()) ||
                                         p.Description.ToLower().Contains(options.StringToFind.ToLower()));

            }
            if (options.category != null)
            {
                query = query.Where(p => p.Category == options.category);
            }
            query = query.Take(500);

            return query;
        }
        public bool UpdateProject(Project project)
        {
            if (project == null)
            {
                return false;
            }

            var projectDb = SearchProject(
                new SearchProjectOptions(){
                    ProjectId = project.ProjectId}).SingleOrDefault();

            if (!string.IsNullOrWhiteSpace(project.Title))
            {
                projectDb.Title = project.Title;
            }
            if (!string.IsNullOrWhiteSpace(project.Description))
            {
                projectDb.Description = project.Description;
            }
            projectDb.DateCreated = DateTime.Now;
            if (project.Donations != null)
            {
                projectDb.Donations = project.Donations;
            }
            if (project.GoalMoney != null)
            {
                projectDb.GoalMoney = project.GoalMoney;
            }
            if (project.Category != null)
            {
                projectDb.Category = project.Category;
            }
            if (project.MediaUrl != null)
            {
                projectDb.MediaUrl = project.MediaUrl;
            }
            if (_dbContext.SaveChanges() > 0)
            {
                return true;
            }
            return false;


        }
        public bool DonationProject(DonationProjectOptions options)
        {
            if (options == null || options.ProjectId == null || options.DonationId == null)
            {
                return false;
            }

            var project = SearchProject(new SearchProjectOptions
            {
                ProjectId = options.ProjectId

            }).SingleOrDefault();

            if (project == null)
            {
                return false;
            }

            var donation = project.Donations.Where(fp => fp.DonationId == options.DonationId).SingleOrDefault();
            if (donation == null)
            {
                return false;
            }
            project.CurrentMoney += donation.Price;


            if (_dbContext.SaveChanges() > 0)
            {
                return true;
            }


            return false;
        }
        public void SaveDaChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
