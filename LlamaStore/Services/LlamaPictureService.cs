using System.Net.Http.Json;
using LlamaStore.Http;
using LlamaStore.Http.Serialization;
using LlamaStore.Models;

namespace LlamaStore.Services;

public class LlamaPictureService : BaseService
{
    internal LlamaPictureService(HttpClient httpClient)
        : base(httpClient) { }

    /// <summary>Get a llama's picture by the llama ID. Pictures are in PNG format.</summary>
    /// <param name="llamaId">The ID of the llama to get the picture for</param>
    public async Task<object> GetLlamaPictureByLlamaIdAsync(int llamaId)
    {
        var request = new RequestBuilder(HttpMethod.Get, "llama/{llama_id}/picture")
            .SetPathParameter("llama_id", llamaId)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<object>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>Create a picture for a llama. The picture is sent as a PNG as binary data in the body of the request.</summary>
    /// <param name="llamaId">The ID of the llama that this picture is for</param>
    public async Task<LlamaId> CreateLlamaPictureAsync(object input, int llamaId)
    {
        var request = new RequestBuilder(HttpMethod.Post, "llama/{llama_id}/picture")
            .SetPathParameter("llama_id", llamaId)
            .SetContentAsJson(input, _jsonSerializerOptions)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<LlamaId>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>
    /// Update a picture for a llama. The picture is sent as a PNG as binary data in the body of the request.
    ///
    /// If the llama does not have a picture, one will be created. If the llama already has a picture,
    ///  it will be overwritten.
    /// If the llama does not exist, a 404 will be returned.
    /// </summary>
    /// <param name="llamaId">The ID of the llama that this picture is for</param>
    public async Task<LlamaId> UpdateLlamaPictureAsync(object input, int llamaId)
    {
        var request = new RequestBuilder(HttpMethod.Put, "llama/{llama_id}/picture")
            .SetPathParameter("llama_id", llamaId)
            .SetContentAsJson(input, _jsonSerializerOptions)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<LlamaId>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>Delete a llama's picture by ID.</summary>
    /// <param name="llamaId">The ID of the llama to delete the picture for</param>
    public async Task DeleteLlamaPictureAsync(int llamaId)
    {
        var request = new RequestBuilder(HttpMethod.Delete, "llama/{llama_id}/picture")
            .SetPathParameter("llama_id", llamaId)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
    }
}
