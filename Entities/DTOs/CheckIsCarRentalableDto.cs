using Core.Entities;
using System;

namespace Entities.DTOs
{
    public class CheckIsCarRentalableDto : IDto
    {
        public int CarId { get; set; }
        public DateTime RentDate { get; set; }
    }
}
