using AutoMapper;
using Microsoft.Extensions.Configuration;
using SwiftBuy.Core.Application.Abstraction.Models;
using SwiftBuy.Core.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Mapping
{
    public class BrandPictureUrlResolver : IValueResolver<ProductBrand, BrandDto, string>
    {
        private readonly IConfiguration _configuration;

        public BrandPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(ProductBrand source, BrandDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
        }
    }
}
