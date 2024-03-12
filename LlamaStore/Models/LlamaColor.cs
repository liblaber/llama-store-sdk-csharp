using System.Text.Json.Serialization;
using LlamaStore.Json;

namespace LlamaStore.Models;

/// <summary>The color of a llama.</summary>
public record LlamaColor : ValueEnum<string>
{
    internal LlamaColor(string value)
        : base(value) { }

    public LlamaColor()
        : base("brown") { }

    public static LlamaColor Brown = new("brown");
    public static LlamaColor White = new("white");
    public static LlamaColor Black = new("black");
    public static LlamaColor Gray = new("gray");
}
