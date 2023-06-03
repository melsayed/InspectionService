using InspectionService.Models;

namespace InspectionService.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        User FindByUsername(string Username);
        User FindByRefreshToken(string RefreshToken);
        bool VerifyPassword(string userPassword,string passwordhash);
    }
}