using Crowdfunding.Models;
using System.Collections.Generic;

namespace Crowdfunding.ViewModels
{
    public class TrendsViewModel
    {
        public IEnumerable<Project> TrendsAmountDonated { get; set; }
        public IEnumerable<Project> TrendsCategory { get; set; }

        public TrendsViewModel()
        {
            TrendsAmountDonated = new List<Project> ();
            TrendsCategory = new List<Project>();
        }
    }
}
