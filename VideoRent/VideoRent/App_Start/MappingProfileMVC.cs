using AutoMapper;
using VideoRentBL.DTOs;
using Customer = VideoRent.Models.Customer;
using MembershipType = VideoRent.Models.MembershipType;
using Movie = VideoRent.Models.Movie;
using Genre = VideoRent.Models.Genre;
using Rental = VideoRent.Models.Rental;

namespace VideoRent
{
    public class MappingProfileMVC:Profile
    {
        public MappingProfileMVC()
        {
            #region MVC Model Mapping

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<MembershipTypeDto, MembershipType>();


            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();

            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            CreateMap<Rental, RentalDto>().ReverseMap();
      

            #endregion

            #region Entity To DTO Mapping

            //CreateMap<VideoRentDAL.Core.Domain.Customer, CustomerDto>();
            //CreateMap<CustomerDto, VideoRentDAL.Core.Domain.Customer>();


            //CreateMap<VideoRentDAL.Core.Domain.Genre, GenreDto>();
            //CreateMap<GenreDto, VideoRentDAL.Core.Domain.Genre>();

            //CreateMap<VideoRentDAL.Core.Domain.MembershipType, MembershipTypeDto>();
            //CreateMap<MembershipTypeDto, VideoRentDAL.Core.Domain.MembershipType>();

            //CreateMap<VideoRentDAL.Core.Domain.Movie, MovieDto>();
            //CreateMap<MovieDto, VideoRentDAL.Core.Domain.Movie>();

            //CreateMap<VideoRentDAL.Core.Domain.Rental, RentalDto>();
            //CreateMap<RentalDto, VideoRentDAL.Core.Domain.Rental>();
            #endregion
        }
        
    }
}