using AutoDoc.DAL.Entities;
using AutoDoc.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoDoc.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Document, DocumentJsonModel>().ReverseMap();

            CreateMap<Bookmark, BookmarkJsonModel>();
            CreateMap<BookmarkJsonModel, string>();
            CreateMap<Bookmark, string>();
        }
    }
}