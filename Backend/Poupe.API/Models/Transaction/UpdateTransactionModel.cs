using Poupe.Domain.Enums;
using System.Text.Json.Serialization;

namespace Poupe.API.Models.Transaction;

public class UpdateTransactionModel
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    [JsonPropertyName("type")]
    public TransactionType Type { get; set; }

    [JsonPropertyName("categoryId")]
    public string? CategoryId { get; set; }
}
