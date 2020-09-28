using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using F7System.Api.Domain.Commands.Student;
using FluentAssertions;
using Xunit;

namespace F7System.IntegrationTests.Controllers
{
    public class StudentControllerTests : BaseIntegrationTest
    {
        [Fact]
        public async Task MustCreateStudentWithAccess()
        {
            var cmd = new CreateStudentCommand()
            {
                Id = Guid.NewGuid(),
                Username = Fixture.Create<string>(),
                Password = Fixture.Create<string>(),
                Name = Fixture.Create<string>()
            };
            
            var response = await DoRequest("/Student/CreateStudent", cmd);
            
            var student = _f7DbContext.StudentDbSet.First(x => x.Username == cmd.Username);

            student.Should().NotBeNull();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            student.Name.Should().Be(cmd.Name);
            student.Username.Should().Be(cmd.Username);
        }
        
        [Fact]
        public async Task ChangeStudentTest()
        {
            // var cmd = new ChangeStudentCommand()
            // {
            //     StudentId = Guid.NewGuid(),
            //     Name = Fixture.Create<string>()
            // };
            //
            // var response = await DoRequest(cmd);
            //
            // var user = _f7DbContext.UserDbSet.First(x => x.StudentId == cmd.StudentId);
            //
            // user.Should().NotBeNull();
            // user.Student.Should().NotBeNull();
            //
            // response.StatusCode.Should().Be(HttpStatusCode.OK);
            // user.Name.Should().Be(cmd.Name);
            // user.Username.Should().Be(cmd.Username);
            // user.Student.Id.Should().Be(cmd.StudentId);
        }
    }
}