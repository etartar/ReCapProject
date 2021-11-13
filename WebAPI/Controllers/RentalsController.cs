using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rentalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRentalDetails()
        {
            var result = await _rentalService.GetRentalDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _rentalService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rental rental)
        {
            var result = await _rentalService.Create(rental);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Rental rental)
        {
            var getRental = await _rentalService.GetById(id);
            if (getRental.Success)
            {
                getRental.Data.CarId = rental.CarId;
                getRental.Data.CustomerId = rental.CustomerId;
                getRental.Data.RentDate = rental.RentDate;
                getRental.Data.ReturnDate = rental.ReturnDate;
                var result = await _rentalService.Update(getRental.Data);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(getRental);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var getRental = await _rentalService.GetById(id);
            if (getRental.Success)
            {
                var result = _rentalService.Delete(getRental.Data);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(getRental);
        }

        [HttpPost]
        public IActionResult CheckIsCarRentalable(CheckIsCarRentalableDto checkIsCarRentalableDto)
        {
            var result = _rentalService.CheckIsCarRentalable(checkIsCarRentalableDto.CarId, checkIsCarRentalableDto.RentDate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
