using System.Net.Http.Json;
using LlamaStore.Http;
using LlamaStore.Http.Serialization;
using LlamaStore.Models;

namespace LlamaStore.Services;

public class LlamaService : BaseService
{
    internal LlamaService(HttpClient httpClient)
        : base(httpClient) { }

    /// <summary>Get all the llamas.</summary>
    public async Task<List<Llama>> GetLlamasAsync()
    {
        var request = new RequestBuilder(HttpMethod.Get, "llama").Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<List<Llama>>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>Create a new llama. Llama names must be unique.</summary>
    public async Task<Llama> CreateLlamaAsync(LlamaCreate input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));

        var request = new RequestBuilder(HttpMethod.Post, "llama")
            .SetContentAsJson(input, _jsonSerializerOptions)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<Llama>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>Get a llama by ID.</summary>
    /// <param name="llamaId">The llama's ID</param>
    public async Task<Llama> GetLlamaByIdAsync(int llamaId)
    {
        var request = new RequestBuilder(HttpMethod.Get, "llama/{llama_id}")
            .SetPathParameter("llama_id", llamaId)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<Llama>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>
    /// Update a llama. If the llama does not exist, create it.
    ///
    /// When updating a llama, the llama name must be unique. If the llama name is not unique, a 409 will be returned.
    /// </summary>
    /// <param name="llamaId">The llama's ID</param>
    public async Task<Llama> UpdateLlamaAsync(LlamaCreate input, int llamaId)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));

        var request = new RequestBuilder(HttpMethod.Put, "llama/{llama_id}")
            .SetPathParameter("llama_id", llamaId)
            .SetContentAsJson(input, _jsonSerializerOptions)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<Llama>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>Delete a llama. If the llama does not exist, this will return a 404.</summary>
    /// <param name="llamaId">The llama's ID</param>
    public async Task DeleteLlamaAsync(int llamaId)
    {
        var request = new RequestBuilder(HttpMethod.Delete, "llama/{llama_id}")
            .SetPathParameter("llama_id", llamaId)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }
}
