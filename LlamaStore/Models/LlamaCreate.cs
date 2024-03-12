using System.Text.Json.Serialization;

namespace LlamaStore.Models;

/// <summary>A new llama for the llama store.</summary>
public record LlamaCreate(
    /// <value>The name of the llama. This must be unique across all llamas.</value>
    [property: JsonPropertyName("name")]
        string Name,
    /// <value>The age of the llama in years.</value>
    [property: JsonPropertyName("age")]
        int Age,
    /// <value>The color of a llama.</value>
    [property: JsonPropertyName("color")]
        LlamaColor Color,
    /// <value>The rating of the llama from 1 to 5.</value>
    [property: JsonPropertyName("rating")]
        int Rating
);
