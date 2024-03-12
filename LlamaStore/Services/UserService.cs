using System.Net.Http.Json;
using LlamaStore.Http;
using LlamaStore.Http.Serialization;
using LlamaStore.Models;

namespace LlamaStore.Services;

public class UserService : BaseService
{
    internal UserService(HttpClient httpClient)
        : base(httpClient) { }

    /// <summary>
    /// Get a user by email.
    ///
    /// This endpoint will return a 404 if the user does not exist. Otherwise, it will return a 200.
    /// </summary>
    /// <param name="email">The user's email address</param>
    public async Task<User> GetUserByEmailAsync(string email)
    {
        ArgumentNullException.ThrowIfNull(email, nameof(email));

        var request = new RequestBuilder(HttpMethod.Get, "user/{email}")
            .SetPathParameter("email", email)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<User>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }

    /// <summary>
    /// Register a new user.
    ///
    /// This endpoint will return a 400 if the user already exists. Otherwise, it will return a 201.
    /// </summary>
    public async Task<User> RegisterUserAsync(UserRegistration input)
    {
        ArgumentNullException.ThrowIfNull(input, nameof(input));

        var request = new RequestBuilder(HttpMethod.Post, "user")
            .SetContentAsJson(input, _jsonSerializerOptions)
            .Build();

        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response
                .Content.ReadFromJsonAsync<User>(_jsonSerializerOptions)
                .ConfigureAwait(false) ?? throw new Exception("Failed to deserialize response.");
    }
}
