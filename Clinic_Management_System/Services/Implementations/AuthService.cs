using Clinic_Management_System.Configurations;
using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clinic_Management_System.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public readonly IMongoDatabase _database;
        public readonly DBSettings _dbSetting;
        public readonly MongoClient _mongoClient;
        public readonly JWTSettings _jWTSetting;

        public AuthService(DBSettings dbSetting, JWTSettings jwtSetting)
        {
            _dbSetting = dbSetting;
            _jWTSetting = jwtSetting;
            _mongoClient = new MongoClient(_dbSetting.ConnectionString);
            _database = _mongoClient.GetDatabase(_dbSetting.DatabaseName);
        }

        public async Task<UserResponse> LoginAsync([FromBody] LoginDTO Obj)
        {
            if (Obj != null)
            {
                IMongoCollection<User> UserCollection = _database.GetCollection<User>("User");

                var filterUser = Builders<User>.Filter.Where(u => u.LoginId == Obj.LoginId && u.Status == 1);
                var userData = await UserCollection.Find(filterUser).FirstOrDefaultAsync();
                UserResponse response;
                if (userData != null)
                {
                    var passwordHasher = new PasswordHasher<User>();
                    var isPasswordValid = passwordHasher.VerifyHashedPassword(null, userData.Password, Obj.Password);

                    if (isPasswordValid == PasswordVerificationResult.Success)
                    {
                        var token = GenerateToken(userData);
                        response = new UserResponse
                        {
                            UserId = userData.UserId,
                            UserTypeId = userData.UserTypeId,
                            ClinicId = userData.ClinicId,
                            FirstName = userData.FirstName,
                            LastName = userData.LastName,
                            DateOfBirth = userData.DateOfBirth,
                            Email = userData.Email,
                            Address = userData.Address,
                            Gender = userData.Gender,
                            PhoneNumber = userData.PhoneNumber,
                            Token = token,
                            Status = userData.Status
                        };

                        return response;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            return null;
        }


        public string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserTypeId)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jWTSetting.SecretKey));

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jWTSetting.Issuer,
                audience: _jWTSetting.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jWTSetting.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
