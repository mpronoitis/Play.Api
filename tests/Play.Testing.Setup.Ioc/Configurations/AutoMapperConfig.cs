using Microsoft.Extensions.DependencyInjection;
using Play.Application.AutoMapper;

namespace Play.Testing.Ioc.Configurations;

public static class AutoMapperConfig
{
    public static void AddAutoMapperConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));
    }
}