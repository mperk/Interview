using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Interview.Configuration;
using Interview.Web;

namespace Interview.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class InterviewDbContextFactory : IDesignTimeDbContextFactory<InterviewDbContext>
    {
        public InterviewDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<InterviewDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            InterviewDbContextConfigurer.Configure(builder, configuration.GetConnectionString(InterviewConsts.ConnectionStringName));

            return new InterviewDbContext(builder.Options);
        }
    }
}
