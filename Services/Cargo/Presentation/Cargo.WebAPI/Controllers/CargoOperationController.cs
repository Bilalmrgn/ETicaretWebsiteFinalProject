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
    public class CargoOperationController : ControllerBase
    {
        private readonly ICargoOperationReadRepository _readRepository;
        private readonly ICargoOperationWriteRepository _writeRepository;
        public CargoOperationController(ICargoOperationReadRepository readRepository, ICargoOperationWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        //Get all
        [HttpGet]
        public IActionResult GetAll()
        {
            var values = _readRepository.GetAll();

            return Ok(values);
        }

        //GetbyId
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var value = await _readRepository.GetByIdAsync(id);

            if (value == null)
            {
                throw new Exception("Cargo Operation bulunamadı");
            }

            return Ok(value);
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCargoOperationDto dto)
        {
            var cargoOperations = new CargoOperation
            {
                Id = Guid.NewGuid(),
                Barcode = dto.Barcode,
                Description = dto.Description,
                OperationDate = dto.OperationDate,
            };

            await _writeRepository.AddAsync(cargoOperations);

            await _writeRepository.SaveChangeAsync();

            return Ok("Cargo Operation başarıyla oluşturuldu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCargoOperationDto dto)
        {
            var cargoOperation = new CargoOperation
            {
                Id = Guid.Parse(dto.CargoOperationId),
                Barcode = dto.Barcode,
                Description = dto.Description,
                OperationDate = dto.OperationDate,
            };

            _writeRepository.Update(cargoOperation);

            await _writeRepository.SaveChangeAsync();

            return Ok("Cargo Operation başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var value = await _readRepository.GetByIdAsync(id);

            if (value == null)
            {
                throw new Exception("Cargo Operation Bulunamadi");
            }

            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveChangeAsync();

            return Ok("cargo operation başarıyla silindi");
        }
    }
}
