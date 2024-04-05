using IdentityServer4.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TeamHelper.Application.Athletes.Commands;
using TeamHelper.Application.Athletes.Queries;
using TeamHelper.Application.Coaches.Commands;
using TeamHelper.Application.Organizations.Commands;
using TeamHelper.WebApi.Models;

namespace TeamHelper.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private IMediator _mediator;
        private IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private Guid UserId;
        internal string Role;

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

        public AuthController(SignInManager<AppUser> signInManager,
                             UserManager<AppUser> userManager,
                             IIdentityServerInteractionService interactionService,
                             ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var vm = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<string>> GetRole()
        {
            return Ok(Role);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(vm.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");
            return View(vm);
        }

        [HttpGet]
        public IActionResult RegisterOrganization(string returnUrl)
        {
            var viewModel = new RegisterOrganizationViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrganization(RegisterOrganizationViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            CreateOrganizationCommand command = new CreateOrganizationCommand()
            {
                Adress = viewModel.Adress,
                Email = viewModel.Email,
                Name = viewModel.Name,
                PhoneNumber = viewModel.PhoneNumber
            };
            var id = await Mediator.Send(command);
            if (id == Guid.Empty || id == null)
            {
                return View(viewModel);
            }
            var user = new AppUser()
            {
                UserName = id.ToString(),
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Organization");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Redirect(viewModel.ReturnUrl);
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCoach([FromBody]RegisterCoachModel registerCoachModel)
        {
            InitClaims();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CreateCoachCommand command = new CreateCoachCommand()
            {
                Age = registerCoachModel.Age,
                PhoneNumber = registerCoachModel.PhoneNumber,
                Email = registerCoachModel.Email,
                Name = registerCoachModel.Name
            };
            if (registerCoachModel.OrganizationId == Guid.Empty)
            {
                command.OrganizationId = UserId;
            }
            else
            {
                command.OrganizationId = registerCoachModel.OrganizationId;
            }
            var id = await Mediator.Send(command);

            if (id == Guid.Empty || id == null)
            {
                return BadRequest();
            }
            var user = new AppUser()
            {
                UserName = id.ToString(),
                Email = registerCoachModel.Email,
                PhoneNumber = registerCoachModel.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerCoachModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Coach");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAthlete([FromBody] RegisterAthleteModel registerAthleteModel)
        {
            InitClaims();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CreateAthleteCommand command = new CreateAthleteCommand()
            {
                Age = registerAthleteModel.Age,
                PhoneNumber = registerAthleteModel.PhoneNumber,
                Email = registerAthleteModel.Email,
                Name = registerAthleteModel.Name
            };
            if (registerAthleteModel.OrganizationId == Guid.Empty)
            {
                command.OrganizationId = UserId;
            }
            else
            {
                command.OrganizationId = registerAthleteModel.OrganizationId;
            }
            var id = await Mediator.Send(command);

            if (id == Guid.Empty || id == null)
            {
                return BadRequest();
            }
            var user = new AppUser()
            {
                UserName = id.ToString(),
                Email = registerAthleteModel.Email,
                PhoneNumber = registerAthleteModel.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerAthleteModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Athlete");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
