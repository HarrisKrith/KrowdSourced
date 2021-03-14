using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfunding.Models
{
    public class UserSponsor
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        // Identity id is set as string by Asp.Net Core Identity
        // Id saved in dbo.AspNetUsers = nvarchar
        // query dbo for columns data types - https://datatofish.com/data-type-columns-sql-server/
        public string UserId { get; set; }
        
        // UserSponsor's constructor
        public UserSponsor(int projectId, string userId) 
        {
            ProjectId = projectId;
            UserId = userId;
        }
    }
}
