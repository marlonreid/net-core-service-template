﻿using Microsoft.Extensions.DependencyInjection;

namespace NetCoreServiceTemplate.Versioning
{
    static class ApiVersioningExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning((o =>
            {
                o.UseApiBehavior = true;
                o.ReportApiVersions = true;
            }));
            serviceCollection.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            return serviceCollection;
        }
    }
}
