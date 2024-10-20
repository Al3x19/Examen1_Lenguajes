﻿using Examen_Lenguajes1_.API.Database.Entities;
using Examen_Lenguajes1_.API.Dtos.Auth;
using Examen_Lenguajes1_.API.Dtos.Common;
using Examen_Lenguajes1_.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Examen_Lenguajes1_.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<EmployeeEntity> _signInManager;
        private readonly UserManager<EmployeeEntity> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            SignInManager<EmployeeEntity> signInManager,
            UserManager<EmployeeEntity> userManager, 
            IConfiguration configuration
            )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager
                .PasswordSignInAsync(dto.Email, 
                                     dto.Password, 
                                     isPersistent: false, 
                                     lockoutOnFailure: false);

            var uuserEntity = await _userManager.FindByEmailAsync(dto.Email);
            if (uuserEntity == null)
            {
                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "User not found"
                };
            }


            if (result.Succeeded) 
            {

                var userEntity = await _userManager.FindByEmailAsync(dto.Email);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                };



                var userRoles = await _userManager.GetRolesAsync(userEntity);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return new ResponseDto<LoginResponseDto> 
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Inicio de sesion satisfactorio",
                    Data = new LoginResponseDto 
                    {
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo,
                    }
                };

            }

            return new ResponseDto<LoginResponseDto> 
            {
                Status = false,
                StatusCode = 401,
                Message = "Fallo el inicio de sesión"
            };


        }

     
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, 
                    SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
