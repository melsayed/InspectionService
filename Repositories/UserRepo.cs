using InspectionService.Data;
using InspectionService.Interfaces;
using InspectionService.Models;

namespace InspectionService.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public User FindByUsername(string Username) => FindByCondition(u => u.Username == Username);

        public User FindByRefreshToken(string RefreshToken)=> FindByCondition(u => u.RefreshToken == RefreshToken);

        public bool VerifyPassword(string userPassword, string passwordhash)=>BCrypt.Net.BCrypt.Verify(userPassword,passwordhash);
        
    }
}