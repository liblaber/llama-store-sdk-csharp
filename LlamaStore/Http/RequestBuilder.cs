using System.Net.Http.Json;
using System.Text.Json;
using LlamaStore.Http.Serialization;

namespace LlamaStore.Http;

public class RequestBuilder
{
    private readonly string _urlTemplate;

    private readonly HttpMethod _httpMethod;

    private readonly Dictionary<string, string> _pathParameters = new();
    private readonly List<string> _queryParameters = new();
    private readonly Dictionary<string, string> _headers = new();

    private HttpContent? _content;

    public RequestBuilder(HttpMethod httpMethod, string urlTemplate)
    {
        _httpMethod = httpMethod;
        _urlTemplate = urlTemplate;
    }

    public RequestBuilder SetPathParameter(
        string key,
        object value,
        PathSerializationStyle style = PathSerializationStyle.Simple,
        bool explode = false
    )
    {
        var serializedValue = Serializer.Serialize(key, value, (SerializationStyle)style, explode);
        _pathParameters.Add(key, serializedValue);
        return this;
    }

    public RequestBuilder SetQueryParameter(
        string key,
        object? value,
        QuerySerializationStyle style = QuerySerializationStyle.Form,
        bool explode = true
    )
    {
        var serializedValue = Serializer.Serialize(key, value, (SerializationStyle)style, explode);
        if (!string.IsNullOrEmpty(serializedValue))
        {
            _queryParameters.Add(serializedValue);
        }
        return this;
    }

    public RequestBuilder SetOptionalQueryParameter(
        string key,
        object? value,
        QuerySerializationStyle style = QuerySerializationStyle.Form,
        bool explode = true
    )
    {
        if (value is not null)
        {
            SetQueryParameter(key, value, style, explode);
        }
        return this;
    }

    public RequestBuilder SetHeader(string key, object? value, bool explode = false)
    {
        var serializedValue = Serializer.Serialize(key, value, SerializationStyle.Simple, explode);
        if (!string.IsNullOrEmpty(serializedValue))
        {
            _headers.Add(key, serializedValue);
        }
        return this;
    }

    public RequestBuilder SetOptionalHeader(string key, object? value, bool explode = false)
    {
        if (value is not null)
        {
            SetHeader(key, value, explode);
        }
        return this;
    }

    public RequestBuilder SetContentAsJson(object content, JsonSerializerOptions? options = null)
    {
        _content = JsonContent.Create(content, options: options);
        return this;
    }

    private string BuildUrl()
    {
        var url = _urlTemplate;
        foreach (var (key, value) in _pathParameters)
        {
            url = url.Replace($"{{{key}}}", value);
        }

        if (_queryParameters.Any())
        {
            url += "?" + string.Join("&", _queryParameters);
        }

        return url;
    }

    public HttpRequestMessage Build()
    {
        var requestMessage = new HttpRequestMessage(_httpMethod, BuildUrl()) { Content = _content };

        foreach (var (key, value) in _headers)
        {
            requestMessage.Headers.Add(key, value);
        }

        return requestMessage;
    }
}
