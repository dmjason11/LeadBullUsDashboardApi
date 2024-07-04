using Api.Errors;
using Api.Helpers;
using Core.IRepos;
using Core.IServices;
using Infrastructure.Repos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(typeof(GoogleSheetsHelper));
            services.AddAutoMapper(typeof(MyMapper));
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                    var errorResponse = new ApiResponseError();
                    errorResponse.Errors = errors;
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
