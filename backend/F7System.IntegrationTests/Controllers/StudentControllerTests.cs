using System.Linq;
using System.Net;
using System.Threading.Tasks;
using F7System.Api.Domain.Commands.Student;
using FluentAssertions;
using Xunit;

namespace F7System.IntegrationTests.Controllers
{
    public class StudentControllerTests : BaseIntegrationTest
    {
        [Fact]
        public async Task CreateStudentTest()
        {
            var cmd = new CreateStudentCommand()
            {
            };
            var request = new
            {
                Url = "/Student/CreateStudent",
                Body = cmd
            };

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            var students = _f7DbContext.StudentDbSet.ToList();
            students.Should().NotBeNullOrEmpty();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}