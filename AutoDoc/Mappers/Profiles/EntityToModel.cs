﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoDoc.DAL.Entities;
using AutoDoc.Models;
using AutoMapper;

namespace AutoDoc.Mappers.Profiles
{
    public class EntityToModel : Profile
    {
        public EntityToModel()
        {
            CreateMap<Bookmark, BookmarksJsonModel>();
            CreateMap<Document, DocumentJsonModel>();
        }
    }
}
