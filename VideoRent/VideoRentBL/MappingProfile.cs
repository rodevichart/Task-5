using AutoMapper;
using VideoRentBL.DTOs;
using VideoRentDAL.Core.Domain;

namespace VideoRentBL
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();


            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, MembershipType>();

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();

            CreateMap<Rental, RentalDto>();
            CreateMap<RentalDto, Rental>();
        }
    }
}