using System.Net.Http.Json;
using LlamaStore.Http;
using LlamaStore.Http.Serialization;
using LlamaStore.Models;

namespace LlamaStore.Services;

public class TokenService : BaseService
{
    internal TokenService(HttpClient httpClient)
        : base(httpClient) { }

    /// <summary>
    /// Create an API token for a user. These tokens expire after 30 minutes.
    ///
    /// Once you have this token, you need to pass it to other endpoints in the Authorization header as a Bearer token.
    /// </summary>
    public async Task<ApiToken> CreateApiTokenAsync(ApiTokenRequest input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));

        var request = new RequestBuilder(HttpMethod.Post, "token")
            .SetContentAsJson(input, _jsonSerializerOptions)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<ApiToken>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }
}
