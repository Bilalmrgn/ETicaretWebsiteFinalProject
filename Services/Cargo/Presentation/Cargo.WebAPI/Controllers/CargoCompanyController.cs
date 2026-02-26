using Cargo.Application;
using Cargo.Application.Dtos;
using Cargo.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompanyController : ControllerBase
    {
        private readonly ICargoCompanyReadRepository _readRepository;
        private readonly ICargoCompanyWriteRepository _writeRepository;
        public CargoCompanyController(ICargoCompanyReadRepository readRepository, ICargoCompanyWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        //Get Cargo Company
        [HttpGet]
        public IActionResult CargoCompanyList()
        {
            var values = _readRepository.GetAll();

            return Ok(values);
        }

        //Get By id cargo company
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCargoCompany(string id)
        {
            var value = await _readRepository.GetByIdAsync(id);

            return Ok(value);
        }

        //Add cargo company
        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto dto)
        {
            CargoCompany cargoCompany = new CargoCompany()
            {
                Id = Guid.NewGuid(),
                CargoCompanyName = dto.CargoCompanyName,
            };

            await _writeRepository.AddAsync(cargoCompany);
            
            await _writeRepository.SaveChangeAsync();

            return Ok("kargo şirketi başarıyla oluşturuldu");
        }

        //update cargo company
        [HttpPut]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto dto)
        {
            CargoCompany cargoCompany = new CargoCompany()
            {
                Id = Guid.Parse(dto.Id),
                CargoCompanyName = dto.CargoCompanyName,
            };

            _writeRepository.Update(cargoCompany);
            
            await _writeRepository.SaveChangeAsync();

            return Ok("Kargo şirketi başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var value = await _readRepository.GetByIdAsync(id);

            if (value == null)
            {
                throw new Exception("Cargo Company Bulunamadi");
            }

            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveChangeAsync();

            return Ok("cargo company başarıyla silindi");
        }
    }
}
