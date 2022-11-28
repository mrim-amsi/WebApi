using PostStoreApi.Data;
using Microsoft.AspNetCore.Identity;

namespace PostStoreApi.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string Generate(ApplicationUser user);
    }
}
