using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Entities.DTOs
{
    public class CarImageUpdateDto : IDto
    {
        public int CarId { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
    }
}
