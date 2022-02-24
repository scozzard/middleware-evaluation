using System.Net;
using System.Xml.Serialization;

namespace Evaluation.Api.ApiClient;

/// <summary>
/// An API client used for making generic HTTP requests to the Middleware NZ API. It is assumed
/// that the API always returns XML objects.
/// </summary>
public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Performs a GET request to a specified path on the Middleware API, and serializes the XML
    /// response to the provided type. 
    /// </summary>
    /// <param name="path">The path not including base url.</param>
    /// <param name="cancellationToken">The cancellation token for async tasks.</param>
    /// <typeparam name="T">The type the response body will be deserialized to.</typeparam>
    /// <returns></returns>
    public async Task<T> Get<T>(string path, CancellationToken cancellationToken)
    {
        // Request the resource.
        var response = await _httpClient.GetAsync(new Uri($"{_httpClient.BaseAddress}{path}.xml"), cancellationToken);

        // If an unsuccessful status code was returned and it was a 404 return null. For other
        // unsuccessful status codes, something serious went wrong so trigger HttpRequestException
        // which will be handled in the exception handling middleware.
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.NotFound) return default;

            response.EnsureSuccessStatusCode();
        }

        // The response is good, now try and deserialize the XML response to the provided type.
        // NOTE: If an exception occurs with the deserialization here, the exception will be caught
        // and logged via the error handling middleware.
        return await DeserializeResponse<T>(response, cancellationToken);
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var serializer = new XmlSerializer(typeof(T));

        return (T)serializer.Deserialize(responseStream);
    }
}

