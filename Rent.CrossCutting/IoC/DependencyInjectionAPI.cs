using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rents.Infrastructure.Context;
using Rents.Infrastructure.Repository.Interfaces;
using Rents.Infrastructure.Repository;
using Rents.Application.Services.Inferfaces;
using Rents.Application.Services;
using Rents.Application.DTOs.Mappings;

namespace Rents.CrossCutting.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
        {
            // Adicionando string de conexao
            string mySqlConnection = configuration.GetConnectionString("DefaultConnection");

            // Registrando servico DbContext
            services.AddDbContext<AppDbContext>(options =>
                                options.UseMySql(mySqlConnection,
                                ServerVersion.AutoDetect(mySqlConnection)));

            // Registrando servico Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Registrando servico UserService
            services.AddScoped<IUserService, UserService>();

            // Registrando servico ImageService
            services.AddScoped<IImageService, ImageService>();

            // Registrando servico BikeService
            services.AddScoped<IBikeService, BikeService>();

            // Registrando servico auto mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
