﻿using System.Collections.Generic;
using VideoRentBL.DTOs;
using VideoRentDAL.Core.Domain;

namespace VideoRentBL.Services.Core
{
    public interface IRentalService:IService<Rental,RentalDto>
    {
        IList<RentalDto> GetAllRentalsWhithCustomersMoviesNMembershipType(int pageIndex, int pageSize = 10);
    }
}