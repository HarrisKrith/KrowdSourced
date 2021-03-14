using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Crowdfunding.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MediaUrl { get; set; }
        public decimal GoalMoney { get; set; }
        public decimal CurrentMoney { get; set; }
        public decimal Percentage { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ExpDate { get; set; }

        public Category Category { get; set; }

        public List<Donation> Donations { get; set; }
        public ApplicationUser UserCreator { get; set; }
        public List<ApplicationUser> UserSponsors { get; set; }

        public Project()
        {
            Donations = new List<Donation>();
            UserSponsors = new List<ApplicationUser>();
            DateCreated = DateTime.Now;
            ExpDate = DateTime.Now;
        }

        public static void UploadMedia(Project project, string webRootPath, IFormFileCollection files)
        {
            //https://stackoverflow.com/questions/64407650/if-model-invalid-then-repoulate-the-files-uploaded
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-create-a-file-or-folder

            string newFileName = System.IO.Path.GetRandomFileName();
            var uploads = Path.Combine(webRootPath, @"images\projects");
            var getExtension = Path.GetExtension(files[0].FileName);

            if (project.MediaUrl != null)
            {
                var mediaPath = Path.Combine(webRootPath, project.MediaUrl.TrimStart('\\'));
                if (System.IO.File.Exists(mediaPath))
                {
                    System.IO.File.Delete(mediaPath);
                }
            }

            using (var filesStreams = new FileStream(Path.Combine(uploads, newFileName + getExtension), FileMode.Create))
            {
                files[0].CopyTo(filesStreams);
            }

            project.MediaUrl = @"\images\projects\" + newFileName + getExtension;
        }
    }
}
