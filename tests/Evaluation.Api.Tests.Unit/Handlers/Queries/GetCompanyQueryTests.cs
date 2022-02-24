using Evaluation.Api.ApiClient;
using Evaluation.Api.ApiClient.Model.Responses;
using Evaluation.Api.Handlers.Queries;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Evaluation.Api.Tests.Unit.Handlers.Queries
{
    public class GetCompanyQueryTests
    {
        private readonly Mock<IApiClient> _mockApiClient;

        public GetCompanyQueryTests()
        {
            _mockApiClient = new Mock<IApiClient>();

            // Setup mocked API request to get a company which returns a company (happy path).
            _mockApiClient.Setup(x => x.Get<CompanyResponse>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CompanyResponse
                {
                    Id = 1,
                    Description = "This is the description.",
                    Name = "Company Name"
                });
        }

        [Fact]
        public async Task GetCompanyQuery_ReturnsCompany_WhenCompanyWithIdExists()
        {
            // Arrange
            var handler = new GetCompanyHandler(_mockApiClient.Object);

            // Act
            var company = await handler.Handle(new GetCompanyQuery(1), new CancellationToken());

            // Assert
            Assert.NotNull(company);
        }

        [Fact]
        public async Task GetCompanyQuery_ReturnsNull_WhenCompanyWithIdDoesNotExist()
        {
            // Arrange
            _mockApiClient.Setup(x => x.Get<CompanyResponse>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CompanyResponse) null);

            var handler = new GetCompanyHandler(_mockApiClient.Object);

            // Act
            var company = await handler.Handle(new GetCompanyQuery(1), new CancellationToken());

            // Assert
            Assert.Null(company);
        }
    }
}