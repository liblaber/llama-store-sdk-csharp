using System.Text.Json.Serialization;

namespace LlamaStore.Models;

/// <summary>A user of the llama store</summary>
public record User(
    /// <value>The email address of the user. This must be unique across all users.</value>
    [property: JsonPropertyName("email")]
        string Email,
    /// <value>The ID of the user. This is unique across all users.</value>
    [property: JsonPropertyName("id")]
        int Id
);
