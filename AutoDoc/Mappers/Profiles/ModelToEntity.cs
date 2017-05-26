using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoDoc.DAL.Entities;
using AutoDoc.Models;
using Newtonsoft.Json;

namespace AutoDoc.Mappers.Profiles
{
    public class ModelToEntity : Profile
    {
        public ModelToEntity()
        {
            //CreateMap<BookmarksJsonModel, Bookmark>().ForMember(d => d.Message, op => op.MapFrom(src => JsonConvert.DeserializeObject(src.Message)));
            CreateMap<DocumentJsonModel, Document>();
            CreateMap<BookmarksJsonModel, Bookmark>();
        }
    }
}
