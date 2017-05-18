using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoDoc.Mappers.Profiles
{
    public class ModelToEntity : Profile
    {
        public string ProfileName 
        {
            get { return "ModelToEntity"; }
        }

        protected void Configure()
        {

        }
    }
}
