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
    public class CargoDetailController : ControllerBase
    {
        private readonly ICargoDetailReadRepository _readRepository;
        private readonly ICargoDetailWriteRepository _writeRepository;

        public CargoDetailController(ICargoDetailWriteRepository writeRepository, ICargoDetailReadRepository readRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        //GetAll
        [HttpGet]
        public IActionResult CargoDetailGetAll()
        {
            var values = _readRepository.GetAll();

            return Ok(values);
        }

        //Get by id
        [HttpGet("{id}")]
        public async Task<IActionResult> CargoDetailGetById(string id)
        {
            var value = await _readRepository.GetByIdAsync(id);

            return Ok(value);
        }

        //Create
        [HttpPost]
        public async Task<IActionResult> CargoDetailCreate(CreateCargoDetailDto dto)
        {
            var cargoDetail = new CargoDetail
            {
                Id = Guid.NewGuid(),
                BarcodNumber = dto.BarcodNumber,
                CargoCompanyId = Guid.Parse(dto.CargoCompanyId),
                RecieverCustomer = dto.RecieverCustomer,
                SenderCustomer = dto.SenderCustomer
            };

            await _writeRepository.AddAsync(cargoDetail);
            await _writeRepository.SaveChangeAsync();

            return Ok("Cargo Detail başarıyla oluşturuldu");
        }

        //Update
        [HttpPut]
        public async Task<IActionResult> CargoDetailUpdate(UpdateCargoDetailDto dto)
        {
            var cargoDetail = new CargoDetail
            {
                Id = Guid.Parse(dto.CargoDetailId),
                BarcodNumber = dto.BarcodNumber,
                CargoCompanyId = Guid.Parse(dto.CargoCompanyId),
                RecieverCustomer = dto.RecieverCustomer,
                SenderCustomer = dto.SenderCustomer
            };

            _writeRepository.Update(cargoDetail);
            await _writeRepository.SaveChangeAsync();

            return Ok("Cargo Detail başarıyla güncellendi");
        }

        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> CargoDetailDelete(string id)
        {
            var cargoDetail = await _readRepository.GetByIdAsync(id);

            if (cargoDetail == null)
            {
                throw new Exception("Cargo Detail bulunamadı");
            }

            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveChangeAsync();

            return Ok("Cargo Detail başarıyla silindi");
        }
    }
}
