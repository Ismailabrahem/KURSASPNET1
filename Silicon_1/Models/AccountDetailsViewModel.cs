using Infrastructure.Models;

namespace Silicon_1.Models;

public class AccountDetailsViewModel
{
    public AccountBasicInfoModel AccountBasicInfo { get; set; } = null!;

    public AccountAddressInfo AddressInfo { get; set; } = null!;

}
