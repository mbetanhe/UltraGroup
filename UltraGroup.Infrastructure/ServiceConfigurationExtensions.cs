using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Hotel;
using UltraGroup.Core.Application.Requests.Rooms;
using UltraGroup.Core.Application.Validators;
using UltraGroup.Infrastructure.Data;
using UltraGroup.Infrastructure.Services;

namespace UltraGroup.Infrastructure
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddFromInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            #endregion

            #region Validators
            services.AddValidatorsFromAssemblyContaining<GetBookingRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateBookingRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUpdateHotelRequest>();
            services.AddValidatorsFromAssemblyContaining<CreateUpdateRoomRequest>();
            services.AddValidatorsFromAssemblyContaining<HotelFilterValidator>();
            #endregion

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IBookingService, BookingService>();
        }
    }
}
