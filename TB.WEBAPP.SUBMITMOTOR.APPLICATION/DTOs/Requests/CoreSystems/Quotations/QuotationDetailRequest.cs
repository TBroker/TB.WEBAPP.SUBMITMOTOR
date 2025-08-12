using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.CoreSystems.Quotations
{
    public class QuotationDetailRequest
    {
        [JsonPropertyName("QUOTATION_NO")]
        [Description("เลขที่ใบเสนอราคา")]
        public string? QuotationNo { get; set; }
    }
}
