using Technical_Assessment.Core.Entities;
using Technical_Assessment.Infrastructure.Repositories;

namespace Technical_Assessment.Application.Strategies
{
    public interface ISMSVendor
    {
        public Task<string> Send(SMS sms, SMSRepository repository);
    }
}
