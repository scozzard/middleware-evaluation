using Evaluation.Api.ApiClient;
using Evaluation.Api.ApiClient.Model.Responses;
using Evaluation.Api.Model;
using MediatR;

namespace Evaluation.Api.Handlers.Queries
{
    public record GetCompanyQuery(int CompanyId) : IRequest<Company>;

    public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, Company>
    {
        private readonly IApiClient _apiClient;

        // It seems the root of the Middleware XML API is used in providing company resources
        // (assuming ~/xml-api/ is the root here). So although it seems kind of pointless to 
        // have an empty string for the resource path here, I'm going to add one to be explicit.
        // I imagine there could be other resources available, and if so they'd have a path.
        private const string CompanyResource = "";

        public GetCompanyHandler(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Company> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var company = await _apiClient.Get<CompanyResponse>($"{CompanyResource}/{request.CompanyId}", cancellationToken);

            if (company == null) return default;

            return new Company
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description
            };
        }
    }
}
