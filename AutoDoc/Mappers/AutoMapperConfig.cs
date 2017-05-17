using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoDoc.Mappers
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration GetMapper(IServiceCollection services)
        {
            return new MapperConfiguration(RegisterMappings);
        }

        private static void RegisterMappings(IMapperConfigurationExpression config)
        {
        }
    }
}