using Frontend.DtosLayer.OrderDtos;
using System.Collections.Generic;

namespace Frontend.DtosLayer.AccountSettingsDtos
{
    public class AccountSettingsViewModelDto
    {
        public GetUserDto User { get; set; }
        public List<ResultOrderDto> Orders { get; set; } = new();
    }
}
