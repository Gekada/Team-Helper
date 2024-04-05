using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        internal Guid UserId = Guid.Empty;
        internal string Role = "";

        public BaseController()
        {}

        [NonAction]
        public void InitClaims()
        {
            string accessToken = HttpContext.Request.Headers["Authorization"];

            if (accessToken.StartsWith("Bearer "))
            {
                accessToken = accessToken.Substring("Bearer ".Length);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(accessToken);

            // Retrieve claims
            UserId = Guid.Parse(token.Claims.FirstOrDefault(claim => claim.Type == "name")?.Value);
            Role = token.Claims.FirstOrDefault(claim => claim.Type == "role")?.Value;
        }
    }
}