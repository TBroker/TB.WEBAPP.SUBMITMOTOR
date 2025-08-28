using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Data.Installments
{
    public class NotifyInstallmentContractRequest
    {
        [JsonPropertyName("contract_no")]
        public string? ContractNo { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }
    }
}
