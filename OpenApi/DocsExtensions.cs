using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCoreServiceTemplate.OpenApi;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetCoreServiceTemplate.OpenApi
{
    public static class DocsExtensions
    {
        public static IServiceCollection AddOpenApiDocs(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureOptions>();
            serviceCollection.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<DefaultValues>();

                    // integrate xml comments
                    // options.IncludeXmlComments(XmlCommentsFilePath);
                });
            return serviceCollection;
        }

        public static IApplicationBuilder UseOpenApiDocs(this IApplicationBuilder applicationBuilder, IApiVersionDescriptionProvider provider)
        {
            var swaggerOptions = new SwaggerOptions();
            swaggerOptions = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<SwaggerOptions>>().Value;
            applicationBuilder.UseMiddleware<SwaggerMiddleware>((object)swaggerOptions);
            applicationBuilder.UseSwaggerUI(
                options =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                    options.InjectStylesheet("/swagger/custom.css");
                });

            return applicationBuilder;
        }
    }
}
