using AutoMapper;
using VideoRentBL.DTOs;
using VideoRentDAL.Core.Domain;

namespace TestConsole
{
    public class MappingProfileMVC:Profile
    {
        public MappingProfileMVC()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.MembershipType, s => s.MapFrom(c => c.MembershipType));
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.MembershipType, s => s.MapFrom(c => c.MembershipType));

            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, MembershipType>();

            CreateMap<VideoRentDAL.Core.Domain.Customer, CustomerDto>();
            CreateMap<CustomerDto, VideoRentDAL.Core.Domain.Customer>();


            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            CreateMap<VideoRentDAL.Core.Domain.MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, VideoRentDAL.Core.Domain.MembershipType>();

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();

            CreateMap<Rental, RentalDto>();
            CreateMap<RentalDto, Rental>();
        }
        
    }
}