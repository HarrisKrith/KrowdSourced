using Crowdfunding.Models;
using System;


namespace Crowdfunding.Services.Options.ProjectOptions
{
    public class SearchProjectOptions
    {
        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? DateCreatedFrom { get; set; }
        public DateTimeOffset? DateCreatedTo { get; set; }
        public string StringToFind { get; set; }

        public decimal? GoalMoney { get; set; }
        public decimal? CurrentMoney { get; set; }
        public DateTimeOffset? DateCreated { get; set; }

        public Category? category { get; set; }
    }
}
