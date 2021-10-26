using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> GetAll(int carId)
        {
            var result = await _carImageService.GetAll(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carImageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CarImageCreateDto carImageCreateDto)
        {
            var result = await _carImageService.Create(carImageCreateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] CarImageUpdateDto carImageUpdateDto, int id)
        {
            var getCarImage = await _carImageService.GetById(id);
            if (getCarImage.Success)
            {
                var result = await _carImageService.Update(getCarImage.Data, carImageUpdateDto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(getCarImage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var getCarImage = await _carImageService.GetById(id);
            if (getCarImage.Success)
            {
                var result = _carImageService.Delete(getCarImage.Data);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(getCarImage);
        }
    }
}
