using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestAuthenticatedApp.AppLayer.Dtos;
using TestAuthenticatedApp.AppLayer.Interfaces;

namespace TestAuthenticatedApp.Mvc.Controllers
{
    public class AccountController(IKeycloakTokenService keycloakTokenService) : Controller
    {
        private readonly IKeycloakTokenService keycloakTokenService = keycloakTokenService;

        [HttpPost("token")]
        public async Task<IActionResult> AuthorizeAsync([FromBody] KeycloakUserDto keycloakUserDto)
        {
            try
            {
                var response = await keycloakTokenService
                    .GetTokenResponseAsync(keycloakUserDto)
                    .ConfigureAwait(false);

                return new OkObjectResult(response);
            }
            catch (KeycloakException)
            {

                return BadRequest("Authorization has failed!");
            }
            catch (Exception)
            {
                return BadRequest("An error has occured!");
            }
        }

        [Authorize]
        [HttpGet("check/authorization")]
        public IActionResult CheckKeycloakAuthorization()
        {
            return new OkObjectResult(HttpStatusCode.OK);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
