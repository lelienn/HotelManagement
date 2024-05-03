using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.RequestModels;
using HotelManagement.ResponseModels;
using HotelManagement.ResponseModels.GuestResponseModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagement.Services
{
    public class AuthenticationnService:IAuthenticationnService
    {
        private IGenericRepository<Guest> genericRepository;
        readonly IConfiguration configuration;

        public AuthenticationnService(IGenericRepository<Guest> genericRepository, IConfiguration configuration)
        {
            this.genericRepository = genericRepository;
            this.configuration = configuration;
        }

        public string CreateAccessToken(Guest customer, TimeSpan expiration)
        {
            var keyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>()
            {
                new Claim("userName", customer.Email),
                new Claim("role", "Admin"),
                new Claim("userId", customer.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(configuration.GetSection("Jwt:ExpirationSeconds").Value)),
                signingCredentials: signingCredentials);

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
            return rawToken;
        }

        public string GetToken(Guest customer)
        {
            var token = CreateAccessToken(customer, TimeSpan.FromMinutes(10));
            return token;
        }


        public GetGuestResponse GetUserByEmail(string email)
        {
            var user = genericRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();
            if (user is null)
                throw new Exception("User is not found");

            GetGuestResponse users = new GetGuestResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                Phone = user.Phone
            };

            return users;
        }

        public LoginResponseModel Login(LoginRequestModel request)
        {
            LoginResponseModel login = new LoginResponseModel();

            var user = genericRepository.Query(x => x.Email == request.Email)?.FirstOrDefault();
            if (user is null)
                return new LoginResponseModel { Status = false };

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                login = new LoginResponseModel
                {
                    Token = GetToken(user),
                    Status = true
                };
                return login;
            }
            return login;
        }
    }
}
