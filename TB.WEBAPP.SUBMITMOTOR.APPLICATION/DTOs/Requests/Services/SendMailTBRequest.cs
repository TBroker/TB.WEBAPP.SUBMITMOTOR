using System.Text.Json.Serialization;

namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.DTOs.Requests.Services
{
    public class SendMailTBRequest
    {
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        [JsonPropertyName("body")]
        public Body? Body { get; set; }

        [JsonPropertyName("to_recipients")]
        public List<ToRecipient>? ToRecipients { get; set; }

        [JsonPropertyName("attachments")]
        public List<Attachments>? Attachments { get; set; }

        [JsonPropertyName("save_to_sent_items")]
        public bool SaveToSentItems { get; set; } = false;
    }

    public class Body
    {
        [JsonPropertyName("content_type")]
        public string? ContentType { get; set; }

        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }

    public class ToRecipient
    {
        [JsonPropertyName("email_address")]
        public EmailAddress? EmailAddress { get; set; }
    }

    public class EmailAddress
    {
        [JsonPropertyName("address")]
        public string? Address { get; set; }
    }

    public class Attachments
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("content_type")]
        public string? ContentType { get; set; }

        [JsonPropertyName("content_bytes")]
        public string? ContentBytes { get; set; }
    }
}