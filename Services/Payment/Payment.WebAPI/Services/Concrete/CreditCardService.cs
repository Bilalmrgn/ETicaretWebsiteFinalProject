using Microsoft.EntityFrameworkCore;
using Payment.WebAPI.Context;
using Payment.WebAPI.Dtos.CreditCardDtos;
using Payment.WebAPI.Models;
using Payment.WebAPI.Services.Interface;
using System.Security.Claims;

namespace Payment.WebAPI.Services.Concrete
{
    public class CreditCardService : ICreditCardService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreditCardService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateCreditCardAsync(CreateCreditCardDto dto)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated.");
            }

            var creditCard = new CreditCard
            {
                UserId = userId,
                CardHolderName = dto.CardHolderName,
                CardNumber = dto.CardNumber,
                CVV = dto.CVV,
                ExpireMonth = dto.ExpireMonth,
                ExpireYear = dto.ExpireYear
            };

            _context.CreditCards.Add(creditCard);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ResultCreditCardDto>> GetAllCreditCardAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated.");
            }

            return await _context.CreditCards
                .Select(cc => new ResultCreditCardDto
                {
                    CreditCardId = cc.CreditCardId,
                    UserId = cc.UserId,
                    CardHolderName = cc.CardHolderName,
                    CardNumber = cc.CardNumber,
                    CVV = cc.CVV,
                    ExpireDate = $"{cc.ExpireMonth}/{cc.ExpireYear}",
                    CreatedDate = cc.CreatedDate
                })
                .ToListAsync();

        }

        //kullanıcının id sine göre kart bilgisini getirme
        public async Task<List<ResultCreditCardDto>> GetCreditCardByUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated.");
            }

            return await _context.CreditCards
                .Where(cc => cc.UserId == userId)
                .Select(cc => new ResultCreditCardDto
                {
                    CreditCardId = cc.CreditCardId,
                    UserId = cc.UserId,
                    CardHolderName = cc.CardHolderName,
                    CardNumber = cc.CardNumber,
                    CVV = cc.CVV,
                    ExpireDate = $"{cc.ExpireMonth}/{cc.ExpireYear}",
                    CreatedDate = cc.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<List<ResultCreditCardDto>> GetCreditCardsByUserNameAsync(string userId)
        {

            return await _context.CreditCards
                .Where(cc => cc.UserId == userId)
                .Select(cc => new ResultCreditCardDto
                {
                    CreditCardId = cc.CreditCardId,
                    UserId = cc.UserId,
                    CardHolderName = cc.CardHolderName,
                    CardNumber = cc.CardNumber,
                    CVV = cc.CVV,
                    ExpireDate = $"{cc.ExpireMonth}/{cc.ExpireYear}",
                    CreatedDate = cc.CreatedDate
                })
                .ToListAsync();
        }

        public async Task UpdateCreditCardAsync(UpdateCreditCardDto dto)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;   

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated.");
            }

            //kart bulunur
            var card = await _context.CreditCards.FirstOrDefaultAsync(x => x.CreditCardId == dto.CreditCardId && x.UserId == userId);

            if(card == null)
            {
                throw new Exception("kart bulunamadı");
            }

            card.CardHolderName = dto.CardHolderName;
            card.CardNumber = dto.CardNumber;
            card.CVV = dto.CVV;
            card.ExpireMonth = dto.ExpireMonth;
            card.ExpireYear = dto.ExpireYear;

            await _context.SaveChangesAsync();
        }
    }
}
