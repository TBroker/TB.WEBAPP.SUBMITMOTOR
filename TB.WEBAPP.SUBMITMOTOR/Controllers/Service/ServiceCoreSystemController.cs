using Microsoft.AspNetCore.Mvc;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Quotations;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystem.Agents;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Installments;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Masters;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces.CoreSystems.Quotations;

namespace TB.WEBAPP.SUBMITMOTOR.Controllers.Service
{
    [Route("api/agent/")]
    [ApiController]
    public class AgentController(
        IAgentUseCase getAgentDetailUseCase
        , IJwtReaderService jwtReaderService
        ) : ControllerBase
    {
        private readonly IAgentUseCase _getAgentDetailUseCase = getAgentDetailUseCase;
        private readonly IJwtReaderService _jwtReaderService = jwtReaderService;


        [HttpPost]
        [Route("fetch/detail")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchAgentDetail()
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

            var response = await _getAgentDetailUseCase.FetchAgentDetail(new AgentDetailRequest() { AgentCode = agentInfo.Value.AgentCode });
            return Ok(response);
        }
    }

    [Route("api/installment/")]
    [ApiController]
    public class InstallmentController(IInstallmentUseCase installmentUseCase) : ControllerBase
    {
        private readonly IInstallmentUseCase _installmentUseCase = installmentUseCase;

        [HttpPost]
        [Route("fetch/calculate/period")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchCalculatePeriod([FromBody] InstallmentCalculatePeriodRequest request)
        {
            var response = await _installmentUseCase.FetchCalculatePeriod(request);
            return Ok(response);
        }
    }

    [Route("api/master/")]
    [ApiController]
    public class MasterController(IMasterUseCase getMasterUseCase) : ControllerBase
    {
        private readonly IMasterUseCase _getMasterUseCase = getMasterUseCase;

        [HttpPost]
        [Route("fetch/district")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchAddressDistrict([FromBody] AddressDistrictRequest request)
        {
            var response = await _getMasterUseCase.FetchAddressDistrict(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/province")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchAddressProvince()
        {
            var response = await _getMasterUseCase.FetchAddressProvince();
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/subdistrict")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchAddressSubDistrict([FromBody] AddressSubDistrictRequest request)
        {
            var response = await _getMasterUseCase.FetchAddressSubDistrict(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/car/seat")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchCarBodySeat()
        {
            var response = await _getMasterUseCase.FetchCarBodySeat();
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/car/brand")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchCarBrand([FromBody] CarBrandRequest request)
        {
            var response = await _getMasterUseCase.FetchCarBrand(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/car/model")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchCarBrand([FromBody] CarModelRequest request)
        {
            var response = await _getMasterUseCase.FetchCarModel(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/car/color")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchCarColor([FromBody] CarColorRequest request)
        {
            var response = await _getMasterUseCase.FetchCarColor(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/car/used")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchCarUsed()
        {
            var response = await _getMasterUseCase.FetchCarUsed();
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/car/voluntary/code")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchCarVoluntaryCode()
        {
            var response = await _getMasterUseCase.FetchCarVoluntaryCode();
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/occupation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchOccupation([FromBody] OccupationRequest request)
        {
            var response = await _getMasterUseCase.FetchOccupation(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/prename")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchPrename([FromBody] PreNameRequest request)
        {
            var response = await _getMasterUseCase.FetchPrename(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("fetch/relationship")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FetchRelationshipName()
        {
            var response = await _getMasterUseCase.FetchRelationship();
            return Ok(response);
        }
    }

    [Route("api/quotation/")]
    [ApiController]
    public class ServiceCoreSystemController(IQuotationUseCase quotationUseCase) : ControllerBase
    {
        [HttpPost]
        [Route("fetch/detail")]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> FetchQuotationDetail([FromBody] QuotationDetailRequest request)
        {
            var response = await quotationUseCase.FetchQuotationDetail(request);
            return Ok(response);
        }
    }
}