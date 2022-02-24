using Evaluation.Api.Model;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Evaluation.Api.Tests.Integration.Controllers
{
    public class CompaniesControllerTests : IClassFixture<IntegrationTestsFixture>
    {
        private readonly IntegrationTestsFixture _fixture;

        private const string CompaniesControllerRoute = "v1/companies";

        public CompaniesControllerTests(IntegrationTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetCompanies_ReturnsOkWithCompany_WhenCompanyWithIdExists(int id)
        {
            // Arrange
            var path = $"{CompaniesControllerRoute}/{id}";

            // Act
            var response = await _fixture.HttpClient.GetAsync(path);

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var company = await JsonSerializer.DeserializeAsync<Company>(responseStream);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(company?.Id);
            Assert.NotNull(company?.Name);
            Assert.NotNull(company?.Description);
        }

        [Fact]
        public async Task GetCompanies_ReturnsNotFoundWithError_WhenCompanyWithIdDoesNotExist()
        {
            // Arrange
            const int invalidId = 999999;
            var path = $"{CompaniesControllerRoute}/{invalidId}";

            // Act
            var response = await _fixture.HttpClient.GetAsync(path);


            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var error = await JsonSerializer.DeserializeAsync<Error>(responseStream);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("Not Found", error?.Name);
            Assert.Equal($"Resource with id '{invalidId}' was not found.", error?.Description);
        }
    }
}
