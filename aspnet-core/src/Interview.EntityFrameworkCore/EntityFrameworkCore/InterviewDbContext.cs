using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Interview.Authorization.Roles;
using Interview.Authorization.Users;
using Interview.MultiTenancy;

namespace Interview.EntityFrameworkCore
{
    public class InterviewDbContext : AbpZeroDbContext<Tenant, Role, User, InterviewDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public InterviewDbContext(DbContextOptions<InterviewDbContext> options)
            : base(options)
        {
        }
    }
}
