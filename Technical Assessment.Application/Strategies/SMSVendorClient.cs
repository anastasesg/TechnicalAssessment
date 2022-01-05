using Technical_Assessment.Core.Entities;
using Technical_Assessment.Infrastructure.Repositories;

namespace Technical_Assessment.Application.Strategies
{
    public class SMSVendorClient
    {
        private ISMSVendor smsVendor;
        private readonly Dictionary<string, ISMSVendor> smsVendorList = new()
        {
            { "+30", new SMSVendorGR() },
            { "+357", new SMSVendorCY() },
        };

        public SMSVendorClient(SMS sms) => smsVendor = smsVendorList.FirstOrDefault(smsVendorItem => smsVendorItem.Key == sms.PhoneNumber.Split(' ')[0]).Value ?? new SMSVendorRest();

        public async Task<string> Send(SMS sms, SMSRepository repository)
        {
            return await smsVendor.Send(sms, repository);
        }
    }
}
