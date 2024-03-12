namespace LlamaStore.Hooks;

public interface IHook
{
    public Task<HttpRequestMessage> BeforeRequestAsync(HttpRequestMessage request);
    public Task<HttpResponseMessage> AfterResponseAsync(HttpResponseMessage response);
    public Task OnErrorAsync(HttpResponseMessage response);
}
