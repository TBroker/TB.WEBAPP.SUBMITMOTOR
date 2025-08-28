using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Responses.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents;
using TB.WEBAPP.SUBMITMOTOR.Models;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers
{
    public class HomeController(ILogger<HomeController> logger
        , IAgentUseCase agentUseCase
        , IJwtReaderService jwtReaderService) : Controller
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

                var agentToken = "/kX36Sxprk2fpHrVWy4CaglVEsbEhJYUm5jIUNami4SQ0JHuL6rhpUiwlVk2g+gYQQ==";
                var agentCode = "2051072";
                //var agentToken = agentInfo.Value.AgentToken;
                //var agentCode = agentInfo.Value.AgentCode;

                if (string.IsNullOrEmpty(agentToken) || string.IsNullOrEmpty(agentCode))
                {
                    _logger.LogWarning("Received empty agent token or code.");
                    return RedirectToAction("AgentNotFound", "PageError");
                }

                var agentDetail = await FetchAgentDetail(agentCode);
                if (agentDetail.AgentCode == null)
                {
                    _logger.LogWarning("Failed to fetch agent details for AgentCode: {AgentCode}", agentCode);
                    return RedirectToAction("AgentNotFound", "PageError");
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
                return View("Error", new ErrorViewModel { RequestId = "500" });
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
                    return RedirectToAction("AgentNotFound", "PageError");
                }

                var agentToken = form["token"].ToString();
                var agentCode = form["agentcode"].ToString();

                if (string.IsNullOrEmpty(agentToken) || string.IsNullOrEmpty(agentCode))
                {
                    _logger.LogWarning("Received empty agent token or code.");
                    return RedirectToAction("AgentNotFound", "PageError");
                }

                var agentDetail = await FetchAgentDetail(agentCode);
                if (agentDetail.AgentCode == null)
                {
                    _logger.LogWarning("Failed to fetch agent details for AgentCode: {AgentCode}", agentCode);
                    return RedirectToAction("AgentNotFound", "PageError");
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
                return View("Error", new ErrorViewModel { RequestId = "500" });
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
                issuer: "SubmitMotor",
                audience: "SubmitMotor",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<AgentDetailResponse> FetchAgentDetail(string agentCode)
        {
            var agentDetailsResponse = await _agentUseCase.FetchAgentDetail(new AgentDetailRequest { AgentCode = agentCode });
            agentDetailsResponse.Data = agentDetailsResponse.Data ?? []; // ตรวจสอบว่า Data ไม่เป็น null
            return agentDetailsResponse.Data.FirstOrDefault() ?? new AgentDetailResponse(); // กำหนดค่าเริ่มต้นหากไม่มีข้อมูล
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}