using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Interview.EntityFrameworkCore
{
    public static class InterviewDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<InterviewDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<InterviewDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
