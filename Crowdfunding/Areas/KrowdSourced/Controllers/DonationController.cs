using Crowdfunding.Models;
using Crowdfunding.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfunding.Areas.KrowdSourced.Controllers
{

    [Area("KrowdSourced")]

    [Authorize]
    public class DonationController : Controller
    {
        private readonly IDonationService _donationService;
        private readonly IProjectService _projectService;

        public DonationController(
            IDonationService donation,
            IProjectService projectService)
        {
            _donationService = donation;
            _projectService = projectService;
        }

        public IActionResult CreateDonation(int id)
        {
            var project = _projectService.ProjectById(id);

            if (project.UserCreator.Id == User.GetUser())
            {
                var donation = new Donation()
                {
                    Project = project,
                    ProjectId = project.ProjectId
                };
                return View(donation);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDonation(Donation donation)
        {
            if (ModelState.IsValid)
            {
                _donationService.AddDonation(donation);

                return RedirectToAction("MyProjects", "Project");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}