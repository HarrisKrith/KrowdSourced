using Crowdfunding.Models;

namespace Crowdfunding.Services.IServices
{
    public interface IUserService
    {
        ApplicationUser UserById(string Id);
    }
}
