using EFCoreWebAPI.Services.VO;

namespace EFCoreWebAPI.Services
{
    public interface ILogin
    {
        TokenVO ValidateCredentials(UserVO user);

        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string userName);
    }
}