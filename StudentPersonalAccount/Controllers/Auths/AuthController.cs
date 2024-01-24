using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentPersonalAccount.EF;
using StudentPersonalAccount.Interfaces;
using StudentPersonalAccount.Logging;
using StudentPersonalAccount.Models;
using StudentPersonalAccount.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentPersonalAccount.Controllers.Auths;

public class AuthController : BaseCRUDController<Auth>
{
    private readonly IRepository<Auth> _repository;

    public AuthController(IRepository<Auth> repository) : base(repository)
    {
        _repository = repository;
    }
    [HttpPost("Auth")]
    public IActionResult AuthUser([FromBody]LoginData loginData)
    {
        Logger.Instance.Log("Начало работы запроса авторизации");
        // находим пользователя 
        Auth? user = ListAll
            .FirstOrDefault(p => p.Login == loginData.Login && p.Password == loginData.Password);

        // если пользователь не найден, отправляем статусный код 400
        if (user is null)
        {
            Logger.Instance.Log("Пользователь не был найден");
            return BadRequest();
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login), new Claim("Id", user.Student.Guid.ToString()) };
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        // формируем ответ
        var response = new
        {
            access_token = encodedJwt,
            username = user.Login,
            guid = user.Guid.ToString(),
            access = user.Access
        };
        Logger.Instance.Log("Запрос был завершён");
        return Ok(response);
    }

    protected override IQueryable<Auth> ListWithAttachmentsAndFilter()
    {
        return _repository.GetListQuery()
            .Include(x => x.Student);
    }
}
