using Alabo.Domains.Services;
using Alabo.Tables.Dtos;

namespace Alabo.Tables.Domain.Services {

    public interface ISmsService : IService {

        SmsEntity Sent(string phone, string content);
    }
}