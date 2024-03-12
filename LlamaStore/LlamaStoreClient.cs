using LlamaStore.Hooks;
using LlamaStore.Http.Handlers;
using LlamaStore.Services;
using Environment = LlamaStore.Http.Environment;

namespace LlamaStore;

public class LlamaStoreClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly TokenHandler _accessTokenHandler;

    public LlamaPictureService LlamaPicture { get; private set; }
    public LlamaService Llama { get; private set; }
    public TokenService Token { get; private set; }
    public UserService User { get; private set; }

    public LlamaStoreClient(string? accessToken = null)
    {
        var hookHandler = new HookHandler(new CustomHook());
        var retryHandler = new RetryHandler(hookHandler);
        _accessTokenHandler = new TokenHandler(retryHandler)
        {
            Header = "Authorization",
            Prefix = "Bearer",
            Token = accessToken
        };

        _httpClient = new HttpClient(_accessTokenHandler)
        {
            BaseAddress = Environment.Default.Uri,
            DefaultRequestHeaders = { { "user-agent", "liblab/2.0.19 dotnet/7.0" } }
        };

        LlamaPicture = new LlamaPictureService(_httpClient);
        Llama = new LlamaService(_httpClient);
        Token = new TokenService(_httpClient);
        User = new UserService(_httpClient);
    }

    public void SetEnvironment(Environment environment)
    {
        SetBaseUrl(environment.Uri);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    public void SetBaseUrl(string baseUrl)
    {
        SetBaseUrl(new Uri(baseUrl));
    }

    public void SetBaseUrl(Uri uri)
    {
        _httpClient.BaseAddress = uri;
    }

    public void SetAccessToken(string token)
    {
        _accessTokenHandler.Token = token;
    }
}

// c029837e0e474b76bc487506e8799df5e3335891efe4fb02bda7a1441840310c
