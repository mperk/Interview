using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Interview.Authorization;

namespace Interview
{
    [DependsOn(
        typeof(InterviewCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class InterviewApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<InterviewAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(InterviewApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
