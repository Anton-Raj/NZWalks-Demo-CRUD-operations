using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ILoginRepository
    {
        string CreateJwtToken(IdentityUser user,List<string>roles);
    }
}
