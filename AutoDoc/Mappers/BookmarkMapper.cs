﻿using AutoDoc.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDoc.Mappers
{
    public class BookmarkMapper
    {
        private IMapper _mapper;

        public BookmarkMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BookmarksListJsonModel GetBookmarksListJsonModel(List<string> bookmarksList)
        {
            throw new NotImplementedException();
        }
    }
}
