using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InterpreteDebitoAPI.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InterpreteDebitoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogInController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public LogInController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public ActionResult<LogInResponseDTO> LogIn(LoginRequestDTO pLogIn)
		{
            try
            {
                if (ValidaLoginApp(pLogIn))
                {
                    var tok = GenerarTokenJWT();

                    return Ok(new LogInResponseDTO() { Result = 0, Mensaje = "Ok", tokenInfo = tok });
                }
                else
                {
                    return Unauthorized(new LogInResponseDTO() { Result = 1, Mensaje = "Usuario no autorizado", tokenInfo = null });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LogInResponseDTO() { Result =1, Mensaje = ex.Message, tokenInfo = null});
            }
        }

        private bool ValidaLoginApp(LoginRequestDTO pLogIn)
        {
            List<AllowedUser> LstUsers = configuration.GetSection("AllowedUsers").Get<List<AllowedUser>>();

            AllowedUser? MatchUser = LstUsers.FirstOrDefault(i => i.User == pLogIn?.User?.ToLower());

            if (MatchUser!= null && MatchUser.Password== pLogIn.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        private TokenInfo GenerarTokenJWT()
        {
            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["SecretKet"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Exipra a la 24 horas.
            DateTime Caducidad = DateTime.UtcNow.AddHours(24);

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: "",
                    audience: "",
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: Caducidad
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new TokenInfo()
            {

                Token = new JwtSecurityTokenHandler().WriteToken(_Token),
                fechaCaducidad = Caducidad

            };
        }
    }
}

