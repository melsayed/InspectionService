using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using InspectionService.Dtos;
using InspectionService.Interfaces;
using InspectionService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InspectionService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : CustomControllerbase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(IUserRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        [Route("Authenticate")]
        [HttpPost]
        public ActionResult Authenticate(UserLoginDto loginModel)
        {
            if (loginModel == null)
                return BadRequest();

            var user = _repository.FindByUsername(loginModel.Username);
            if (user == null)
                return NotFound("Invalid username.");


            if (!_repository.VerifyPassword(loginModel.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password.");

            var token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            Console.WriteLine($"---- Refresh Token {newRefreshToken}");

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            _repository.SaveChanges();

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        [Route("RefreshToken/{RefreshToken}")]
        [HttpPost]
        public ActionResult RefreshToken(string RefreshToken)
        {

            if (string.IsNullOrEmpty(RefreshToken))
                return BadRequest("Invalid request");

            var user = _repository.FindByRefreshToken(RefreshToken);

            if (user == null)
                return NotFound("Invalid refresh token");

            if (user.RefreshTokenExpiryTime < DateTime.Now)
                return Unauthorized("Refresh Token is expired");

            var token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            _repository.SaveChanges();

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        //Force user to gain a new token ---> refreshtoken = null
        [Route("Revoke/{Username}")]
        [HttpPost]
        public ActionResult Revoke(string Username)
        {
            if (string.IsNullOrEmpty(Username))
                return BadRequest("Invalid request");

            var user = _repository.FindByUsername(Username);
            if (user == null)
                return NotFound("Invalid username");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            _repository.SaveChanges();

            return NoContent();
        }

        //Force all users to gain a new token ---> refreshtoken = null
        [Route("RevokeAll")]
        [HttpPost]
        public ActionResult RevokeAll()
        {
            var users = _repository.GetAll();
            if (users != null)
            {
                foreach (var user in users)
                {
                    user.RefreshToken = null;
                    user.RefreshTokenExpiryTime = null;
                }
                _repository.SaveChanges();
            }

            return NoContent();
        }



        private JwtSecurityToken CreateToken(User user)
        {
            //create claims details based on the user information
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.id.ToString()),
                        new Claim("DisplayName", user.DisplayName),
                        new Claim("UserName", user.Username)
                        // new Claim(ClaimTypes.Role,user.Role) -->  [Authorize(Roles = "Admin")]
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                signingCredentials: signIn);

            return token;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refToken = Convert.ToBase64String(randomNumber).Replace("/", "").Replace(@"\", "");
            // ensure refresh token is unique by checking against db
            var userByRefToken = _repository.FindByRefreshToken(refToken);
            if(userByRefToken!=null)
                return GenerateRefreshToken();
            return refToken;
        }
    }
}