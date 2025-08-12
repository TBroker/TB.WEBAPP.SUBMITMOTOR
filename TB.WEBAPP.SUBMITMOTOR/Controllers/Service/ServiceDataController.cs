using Microsoft.AspNetCore.Mvc;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Premiums;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Reports;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.Datas.Rewards;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.Service
{
    [Route("api/data/reports/")]
    [ApiController]
    public class ReportDataController(IReportUseCase getReportUseCase) : ControllerBase
    {
        private readonly IReportUseCase _getReportUseCase = getReportUseCase;

        [HttpPost]
        [Route("fetch/quotation/motor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchQuotationReportList([FromBody] QuotationListRequest request)
        {
            request.UserId = "2051072"; // Example user ID, replace with actual logic to get user ID
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
    public class RewardDataController(IRewardUesCase rewardUesCase) : ControllerBase
    {
        private readonly IRewardUesCase _rewardUesCase = rewardUesCase;

        [HttpPost]
        [Route("fetch/point")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchRewardPointAsync()
        {
            var UserId = "2051072"; // Example user ID, replace with actual logic to get user ID
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
