﻿using AutoMapper;
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

            CreateMap<Rental, RentalDto>();
            CreateMap<RentalDto, Rental>();

            #endregion

        }
        
    }
}