namespace Evaluation.Api.ApiClient;

public interface IApiClient
{
    Task<T> Get<T>(string url, CancellationToken cancellationToken);
}