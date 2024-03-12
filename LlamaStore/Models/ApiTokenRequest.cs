using System.Text.Json.Serialization;

namespace LlamaStore.Models;

/// <summary>A request to get an API token for a given user.</summary>
public record ApiTokenRequest(
    /// <value>The email address of the user. This must be unique across all users.</value>
    [property: JsonPropertyName("email")]
        string Email,
    /// <value>The password of the user. This must be at least 8 characters long, and contain at least one letter, one number, and one special character.</value>
    [property: JsonPropertyName("password")]
        string Password
);
