using Alabo.Domains.Base.Dtos;
using Alabo.Domains.Services;

namespace Alabo.Domains.Base.Services
{
    public interface ISmsService : IService
    {
        SmsEntity Sent(string phone, string content);
    }
}