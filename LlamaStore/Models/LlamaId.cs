using System.Text.Json.Serialization;

namespace LlamaStore.Models;

/// <summary>A llama id.</summary>
public record LlamaId(
    /// <value>The ID of the llama.</value>
    [property: JsonPropertyName("id")]
        int Id
);
