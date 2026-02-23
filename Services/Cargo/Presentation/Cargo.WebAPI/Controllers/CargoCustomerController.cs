using Cargo.Application;
using Cargo.Application.Dtos;
using Cargo.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.WebAPI.Controllers
{
    public class CargoCustomerController : Controller
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

        [HttpGet]
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
