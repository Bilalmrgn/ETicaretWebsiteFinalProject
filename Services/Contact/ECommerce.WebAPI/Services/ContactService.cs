using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Dtos;
using ECommerce.WebAPI.Entities;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;

namespace ECommerce.WebAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactDbContext _context;

        public ContactService(ContactDbContext context)
        {
            _context = context;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            var contactMessage = new Contact
            {
                Email = createContactDto.Email,
                Message = createContactDto.Message,
                NameSurname = createContactDto.NameSurname,
                SendDate = DateTime.Now,
                Subject = createContactDto.Subject,
                IsRead = false
            };

            await _context.Contacts.AddAsync(contactMessage);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contactMessage = await _context.Contacts.FindAsync(id);

            if (contactMessage == null)
            {
                throw new Exception("Contact Mesaj bulunamadı (ContactService)");
            }

            _context.Contacts.Remove(contactMessage);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ResultContactDto>> GetAllContactsAsync()
        {
            var values = await _context.Contacts.OrderByDescending(x => x.SendDate).ToListAsync();

            return values.Select(y => new ResultContactDto
            {
                ContactId = y.ContactId,
                NameSurname = y.NameSurname,
                Email = y.Email,
                Subject = y.Subject,
                Message = y.Message,
                SendDate = y.SendDate,
                IsRead = y.IsRead
            }).ToList();
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(int id)
        {
            var contactMessage = await _context.Contacts.FindAsync(id);

            if (contactMessage == null)
            {
                throw new Exception("Mesaj bulunamadı. (GetByIdContactAsync metodunda)");
            }

            if (contactMessage != null && contactMessage.IsRead == false)
            {
                contactMessage.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return new GetByIdContactDto
            {
                ContactId = contactMessage.ContactId,
                Email = contactMessage.Email,
                Subject = contactMessage.Subject,
                IsRead = contactMessage.IsRead,
                Message = contactMessage.Message,
                NameSurname = contactMessage.NameSurname,
                SendDate = contactMessage.SendDate,
            };
        }

        

    }
}
