using Crowdfunding.Models;
using Crowdfunding.Services.IServices;
using Crowdfunding.Services.Options.ProjectOptions;
using Crowdfunding.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Crowdfunding.Areas.KrowdSourced.Controllers
{
    [Area("KrowdSourced")]

    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IUserSponsorService _userSponsorService;

        private readonly IWebHostEnvironment _hostEnviroment;

        public ProjectController(
            IProjectService projectService, 
            IWebHostEnvironment hostEnvironment, 
            IUserService userService, 
            IUserSponsorService userSponsorService )
        {
            _projectService = projectService;
            _hostEnviroment = hostEnvironment;
            _userService = userService;
            _userSponsorService = userSponsorService;
        }

        public IActionResult Trends(Category category)
        {
            var trendsByDonations = _projectService.TrendsAmountDonated().ToList();
            var trendsByCategory = _projectService.TrendsCategory(category).ToList();

            if (trendsByDonations == null ) {
                return null;
            }

            trendsByDonations.ForEach(p => p.Description =
                    ExtentionMethods.StripTagsCharArray(p.Description));
            trendsByCategory.ForEach(p => p.Description =
                    ExtentionMethods.StripTagsCharArray(p.Description));

            var TrendsViewModel = new TrendsViewModel() {
                TrendsAmountDonated = trendsByDonations,
                TrendsCategory = trendsByCategory

            };
            return View(TrendsViewModel);
        } 

        public IActionResult AllProjects()
        {
            var projects = _projectService.AllProjects().ToList();
            
            return View(projects);
        } 

        [Authorize]
        public IActionResult MyProjects()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var projects = _projectService.AllProjects().
                Where(u=>u.UserCreator.Id == claim.Value)
                .ToList();

            return View(projects);
        } 

        public IActionResult ProjectInfo(int id)
        {
            var project = _projectService.ProjectById(id);
            ExtentionMethods.StripTagsCharArray(project.Description);

            project.Donations.ForEach(p => p.Description = ExtentionMethods.StripTagsCharArray(p.Description));

            return View(project);
        } 

        [HttpGet]
        public IActionResult ShowByCategory(Category cat)
        {
            var projects = _projectService.SearchProject(new SearchProjectOptions()
            {
                category = cat
            }); ;

            return View(projects);
        } 

        [HttpGet]
        public IActionResult Search(string searchType)
        {
            if (string.IsNullOrWhiteSpace(searchType)) {
                return NotFound();
            }
            var options = new SearchProjectOptions()
            {
                StringToFind = searchType
            };

            var projects = _projectService.SearchProject(options).ToList();

            return View(projects);
        } 

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject(Project project)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _hostEnviroment.WebRootPath;

                if (files.Count > 0)
                {
                    Project.UploadMedia(project, webRootPath, files);
                }
                else
                {
                    if (project.ProjectId != 0)
                    {
                        var objFromDb = _projectService.ProjectById(project.ProjectId);
                        project.MediaUrl = objFromDb.MediaUrl;
                    }
                }
                if (project.ProjectId == 0)
                {
                    _projectService.AddProject(project);

                    project.UserCreator = _userService.UserById(User.GetUser());
                }
                else
                {
                    _projectService.UpdateProject(project);
                }

                _projectService.SaveDaChanges();

                return RedirectToAction("MyProjects");
            }
            else
            {
                if (project.ProjectId != 0)
                {
                    project = _projectService.ProjectById(project.ProjectId);
                }
            }
            return View(project);
        } 

        [Authorize]
        public IActionResult CreateProject(int? id)
        {
            var project = new Project();

            if (id == null)
            {
                return View(project);
            }

            project = _projectService.ProjectById(id.GetValueOrDefault());

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        } 

        [HttpPost]
        public IActionResult Donation([FromBody] DonationViewModel viewmodel)
        {
            if (viewmodel == null)
            {
                return BadRequest();
            }

            var donationOptions = new DonationProjectOptions()
            {
                DonationId = int.Parse(viewmodel.DonationId),
                ProjectId = int.Parse(viewmodel.ProjectId)
            };

            _projectService.DonationProject(donationOptions);

            var projectDonation = _projectService.ProjectById(int.Parse(viewmodel.ProjectId));
            var sponsor = _userService.UserById(User.GetUser());

            _userSponsorService.AddSponsor(new UserSponsor(projectDonation.ProjectId, sponsor.Id));

            _projectService.SaveDaChanges();

            return Ok();
        }

    }
}