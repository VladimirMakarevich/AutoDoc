using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoDoc.DAL.Entities;
using AutoDoc.Models;

namespace AutoDoc.Mappers.Profiles
{
    public class ModelToEntity : Profile
    {
        public ModelToEntity()
        {
            CreateMap<BookmarksJsonModel, Bookmark>();
            CreateMap<DocumentJsonModel, Document>();
        }
    }
}
