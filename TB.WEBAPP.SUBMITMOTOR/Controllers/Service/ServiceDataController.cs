using Microsoft.AspNetCore.Mvc;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Data.Rewards;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.Service
{
    [Route("api/data/reports/")]
    [ApiController]
    public class ReportDataController(
        IReportUseCase getReportUseCase
        , IJwtReaderService jwtReaderService) : ControllerBase
    {
        private readonly IReportUseCase _getReportUseCase = getReportUseCase;
        private readonly IJwtReaderService _jwtReaderService = jwtReaderService;

        [HttpPost]
        [Route("fetch/quotation/motor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchQuotationReportList([FromBody] QuotationListRequest request)
        {
            var agentCookies = Request.Cookies["AgentAuth"];
            if (string.IsNullOrEmpty(agentCookies))
            {
                return BadRequest("Invalid agent information.");
            }

            var agentInfo = _jwtReaderService.ReadAgentInfo(agentCookies);
            if (agentInfo == null)
            {
                return BadRequest("Invalid agent information.");
            }

            request.UserId = agentInfo.Value.AgentCode; // Example user ID, replace with actual logic to get user ID
            var response = await _getReportUseCase.FetchQuotationReportList(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/quotation/detail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchQuotationReportDetail([FromBody] QuotationDetailRequest request)
        {
            var response = await _getReportUseCase.FetchQuotationReportDetail(request);
            return Ok(response);
        }
    }

    [Route("api/data/premiums/")]
    [ApiController]
    public class PremiumDataController(IPremiumUseCase getPremiumUseCase) : ControllerBase
    {
        private readonly IPremiumUseCase _getPremiumUseCase = getPremiumUseCase;

        [HttpPost]
        [Route("fetch/masterplan/detail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchPremiumReportList([FromBody] MasterPlanRequest request)
        {
            var response = await _getPremiumUseCase.FetchMasterPlanDetail(request);
            return Ok(response);
        }
    }

    [Route("api/data/rewards/")]
    [ApiController]
    public class RewardDataController(IRewardUesCase rewardUesCase
        , IJwtReaderService jwtReaderService) : ControllerBase
    {
        private readonly IRewardUesCase _rewardUesCase = rewardUesCase;
        private readonly IJwtReaderService _jwtReaderService = jwtReaderService;

        [HttpPost]
        [Route("fetch/point")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchRewardPointAsync()
        {
            var agentCookies = Request.Cookies["AgentAuth"];
            if (string.IsNullOrEmpty(agentCookies))
            {
                return BadRequest("Invalid agent information.");
            }

            var agentInfo =  _jwtReaderService.ReadAgentInfo(agentCookies);
            if (agentInfo == null)
            {
                return BadRequest("Invalid agent information.");
            }

            var UserId = agentInfo.Value.AgentCode; // Example user ID, replace with actual logic to get user ID
            var response = await _rewardUesCase.FetchRewardPointAsync(UserId);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/list/point")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchListRewardPointAsync()
        {
            var response = await _rewardUesCase.FetchListRewardPointAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/list/point/promotion")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchListRewardPromotionAsync()
        {
            var response = await _rewardUesCase.FetchListRewardPromotionAsync();
            return Ok(response);
        }
    }
}
