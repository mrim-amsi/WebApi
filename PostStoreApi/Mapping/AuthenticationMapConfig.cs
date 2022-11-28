using PostStoreApi.Data;
using PostStoreApi.DTOs;
using Mapster;

namespace PostStoreApi.Mapping
{
    public class AuthenticationMapConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequestDto, ApplicationUser>()
                .Map(dest => dest.UserName, src => src.Username)
                //.TwoWays()
                ;
            //throw new NotImplementedException();
        }
    }
}
