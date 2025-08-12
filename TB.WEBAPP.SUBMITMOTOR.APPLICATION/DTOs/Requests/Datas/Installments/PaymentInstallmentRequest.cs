using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Datas.Installments
{
    public class PaymentInstallmentRequest
    {
        [JsonPropertyName("contract_no")]
        public string? ContractNumber { get; set; }

        [JsonPropertyName("transaction_id")]
        public string? TransactionId { get; set; }
    }
}
