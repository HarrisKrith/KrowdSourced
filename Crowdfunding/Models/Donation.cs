using System;

namespace Crowdfunding.Models
{
    public class Donation
    {
        public int DonationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset DateCreated { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public Donation() // Donation's constructor
        {
            DateCreated = DateTime.Now;
        }
    }
}
