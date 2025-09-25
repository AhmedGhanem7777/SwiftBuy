using AutoMapper;
using SwiftBuy.AdminDashboard.Models;
using SwiftBuy.Core.Domain.Common.Entities;

namespace SwiftBuy.AdminDashboard.Helper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
