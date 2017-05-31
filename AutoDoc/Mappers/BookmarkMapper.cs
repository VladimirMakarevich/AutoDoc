using AutoDoc.DAL.Entities;
using AutoDoc.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDoc.Mappers
{
    public class BookmarkMapper
    {
        private readonly IMapper _mapper;

        public BookmarkMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<Bookmark> ToBookmarks(List<string> bookmarksList)
        {
            var bookmarks = bookmarksList.Select(ToBookmark).ToList();

            return bookmarks;
        }

        public Bookmark ToBookmark(BookmarkJsonModel bookmark)
        {
            return new Bookmark { Name = bookmark };
        }
    }
}
