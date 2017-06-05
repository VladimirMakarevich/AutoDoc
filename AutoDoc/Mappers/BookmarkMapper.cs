using AutoDoc.DAL.Entities;
using AutoDoc.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using AutoDoc.BL.Models;
using System;

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
            var bookmarks = bookmarksList.Select(ToMapNameBookmark).ToList();

            return bookmarks;
        }

        public Bookmark ToMapNameBookmark(string bookmark)
        {
            return new Bookmark { Name = bookmark };
        }

        public Bookmark ToBookmark(BookmarkJsonModel bookmark)
        {
            return _mapper.Map<BookmarkJsonModel, Bookmark>(bookmark);
        }

        public BookmarkJsonModel ToBookmarkJsonModel(Bookmark bookmark)
        {
            return _mapper.Map<Bookmark, BookmarkJsonModel>(bookmark);
        }

        public Bookmark ToBookmarkFromName(WordBookmark bookmarkName, int id)
        {
            return new Bookmark
            {
                Name = bookmarkName.BookmarkData.Key,
                MessageJson = bookmarkName.Message,
                DocumentId = id,
                Type = bookmarkName.BookmarkType
            };

        }
    }
}
