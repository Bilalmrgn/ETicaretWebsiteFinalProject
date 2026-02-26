using Cargo.Application;
using Cargo.Application.Dtos;
using Cargo.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomerController : ControllerBase
    {
        private readonly ICargoCustomerReadRepository _readRepository;
        private readonly ICargoCustomerWriteRepository _writeRepository;
        public CargoCustomerController(ICargoCustomerReadRepository readRepository, ICargoCustomerWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        [HttpGet]
        public IActionResult CargoCustomerGetAll()
        {
            var values = _readRepository.GetAll();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CargoCustomerGetById(string id)
        {
            var value = _readRepository.GetByIdAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CargoCustomerCreate(CreateCargoCustomerDto dto)
        {
            var cargoCustomer = new CargoCustomer
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                District = dto.District,
                Address = dto.Address,

            };

            await _writeRepository.AddAsync(cargoCustomer);

            await _writeRepository.SaveChangeAsync();

            return Ok("cargo customer is created");
        }

        [HttpPut]
        public async Task<IActionResult> CargoCustomerUpdate(UpdateCargoCustomer dto)
        {
            var cargoCustomer = new CargoCustomer
            {
                Id = Guid.Parse(dto.Id),
                Name = dto.Name,
                Surname = dto.Surname,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                District = dto.District,
                Address = dto.Address,
            };

            _writeRepository.Update(cargoCustomer);
            await _writeRepository.SaveChangeAsync();

            return Ok("Cargo Customer başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var value = await _readRepository.GetByIdAsync(id);

            if (value == null)
            {
                throw new Exception("Cargo Customer Bulunamadi");
            }

            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveChangeAsync();

            return Ok("cargo operation başarıyla silindi");
        }
    }
}
