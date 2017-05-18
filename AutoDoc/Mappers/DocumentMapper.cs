using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoDoc.DAL.Entities;
using AutoMapper;

namespace AutoDoc.Mappers
{
    public class DocumentMapper
    {
        private IMapper _mapper;

        public DocumentMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Document GetDocument(string fileName, string filePath)
        {
            return new Document() { Name = fileName, Path = filePath};
        }
    }
}
