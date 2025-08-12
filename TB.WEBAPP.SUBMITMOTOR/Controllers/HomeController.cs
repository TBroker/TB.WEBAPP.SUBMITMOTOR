using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents;
using TB.WEBAPP.SUBMITMOTOR.Models;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers
{
    public class HomeController(ILogger<HomeController> logger
        , IAgentUseCase agentUseCase
        ,IJwtReaderService jwtReaderService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IAgentUseCase _agentUseCase = agentUseCase;
        private readonly IJwtReaderService _jwtReaderService = jwtReaderService;

        public async Task<IActionResult> Index()
        {
            try
            {
                //var agentCookies = Request.Cookies["AgentAuth"];
                //if (string.IsNullOrEmpty(agentCookies))
                //{
                //    return RedirectToAction("Login", "Auth");
                //}

                //var agentInfo = _jwtReaderService.ReadAgentInfo(agentCookies);
                //if (agentInfo == null)
                //{
                //    return RedirectToAction("Login", "Auth");
                //}

                //ViewBag.AgentCode = agentInfo.Value.AgentCode;
                //ViewBag.AgentToken = agentInfo.Value.AgentToken;
                var agentToken = "9zWd1Q8LmUC9ZAoreAP66w86c53WDB0ijHa32O47FTQMT2Gw86jT06LWGwjfJSQtg==";
                var agentCode = "2051072";

                if (string.IsNullOrEmpty(agentToken) || string.IsNullOrEmpty(agentCode))
                {
                    _logger.LogWarning("Received empty agent token or code.");
                    return RedirectToAction("Index", "Home");
                }

                var agentDetailsResponse = await _agentUseCase.FetchAgentDetail(new AgentDetailRequest() { AgentCode = agentCode });
                var agentDetail = agentDetailsResponse.Data != null && agentDetailsResponse.Data.Count > 0 ? agentDetailsResponse.Data[0] : null;

                if (agentDetail == null)
                {
                    _logger.LogWarning("Failed to fetch agent details for AgentCode: {AgentCode}", agentCode);
                    return RedirectToAction("Index", "Home");
                }

                var jwtToken = GenerateJwtToken(agentCode, agentToken);

                Response.Cookies.Append("AgentAuth", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the form data.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePostData(IFormCollection form)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Received invalid model state.");
                    return RedirectToAction("Index", "Home");
                }

                var agentToken = form["token"].ToString();
                var agentCode = form["agentcode"].ToString();

                if (string.IsNullOrEmpty(agentToken) || string.IsNullOrEmpty(agentCode))
                {
                    _logger.LogWarning("Received empty agent token or code.");
                    return RedirectToAction("Index", "Home");
                }

                var agentDetailsResponse = await _agentUseCase.FetchAgentDetail(new AgentDetailRequest() { AgentCode = agentCode });
                var agentDetail = agentDetailsResponse.Data != null && agentDetailsResponse.Data.Count > 0 ? agentDetailsResponse.Data[0] : null;

                if (agentDetail == null)
                {
                    _logger.LogWarning("Failed to fetch agent details for AgentCode: {AgentCode}", agentCode);
                    return RedirectToAction("Index", "Home");
                }

                var jwtToken = GenerateJwtToken(agentCode, agentToken);

                Response.Cookies.Append("AgentAuth", jwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the form data.");
                return RedirectToAction("Index", "Home");
            }
        }

        private static string GenerateJwtToken(string agentCode, string agentToken)
        {
            var claims = new[]
            {
                new Claim("AgentCode", agentCode)
                , new Claim("AgentToken", agentToken)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f4279a8fd755030073b40c60c2a191ee7afb8892479ca5243ee64f2e277162e0")); // ควรเก็บไว้ใน appsettings.json
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-app",
                audience: "your-app",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
