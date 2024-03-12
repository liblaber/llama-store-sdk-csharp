using System.Text.Json.Serialization;

namespace LlamaStore.Models;

/// <summary>An API token to use for authentication.</summary>
public record ApiToken(
    /// <value>The bearer token to use with the API. Pass this in the Authorization header as a bearer token.</value>
    [property: JsonPropertyName("access_token")]
        string AccessToken,
    /// <value>The type of token. This will always be bearer.</value>
    [property:
        JsonPropertyName("token_type"),
        JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)
    ]
        string? TokenType = null
);
