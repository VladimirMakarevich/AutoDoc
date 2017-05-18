using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoDoc.Mappers.Profiles;

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
            Mapper.Initialize(x =>
            {
                x.AddProfile<ModelToEntity>();
                x.AddProfile<EntityToModel>();
            });
        }
    }
}