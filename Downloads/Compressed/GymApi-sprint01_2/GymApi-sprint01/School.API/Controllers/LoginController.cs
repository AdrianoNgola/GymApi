
using Microsoft.AspNetCore.Mvc;
using School.Aplication.Services.User.Interface;
using School.Domain.Account;
using School.Domain.Interfaces;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUtilizadorService _utilizadorService;
        private readonly IPerfilRepository _Perfil;
         private readonly IAuthenticate _authenticate;

        public LoginController(IUtilizadorService utilizadorService, IAuthenticate authenticate, IPerfilRepository Perfil)
        {
            _utilizadorService = utilizadorService;
            _authenticate = authenticate;
             _Perfil = Perfil;
        }
        [HttpPost("login")]
        
         public async Task<ActionResult> GetUtilizadorToken(string email, string passeWord)
        {
            var Utilizador = await _authenticate.GetUserByEmail(email);
            var UtilizadorValido = await _authenticate.UserAuthenticate(email, passeWord);
            if (!UtilizadorValido)
            {
                return BadRequest(new { Message = "Utilizador ou senha inválida." });
            }

             var Role = await _Perfil.GetByIdAsync(Utilizador.PerfilId);
            
            var token = _authenticate.GenerateToken(Utilizador?.Email?.ToLower(), Utilizador?.UtilizadorId.ToString(), Utilizador.PerfilId);
            

            return Ok(new { 
                Id = Utilizador?.UtilizadorId,
                Nome = Utilizador?.Nome,
                Email = Utilizador?.Email,
                Perfil = Role.Nome,
                Token = token,
                
            });
        }
    }
}