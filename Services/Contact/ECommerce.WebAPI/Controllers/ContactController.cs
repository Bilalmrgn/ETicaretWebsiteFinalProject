using ECommerce.WebAPI.Dtos;
using ECommerce.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        //Get all contact message
        [HttpGet]
        public async Task<IActionResult> GetAllContactMessage()
        {
            var values = await _contactService.GetAllContactsAsync();
            return Ok(values);
        }

        //Get by id contact message
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdContactMessage(int id)
        {
            var value = await _contactService.GetByIdContactAsync(id);
            return Ok(value);
        }

        //Create contact message 
        [HttpPost]
        public async Task<IActionResult> CreateContactMessage(CreateContactDto dto)
        {
            await _contactService.CreateContactAsync(dto);
            return Ok("Contact Message Başarıyla Oluşturuldu");
        }

        //Delete contact message
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactMessage(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return Ok("Contact Message Başarıyla Silindi");
        }
    }
}
