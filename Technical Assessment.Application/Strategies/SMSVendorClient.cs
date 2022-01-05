using Technical_Assessment.Core.Entities;
using Technical_Assessment.Infrastructure.Repositories;

namespace Technical_Assessment.Application.Strategies
{
    public class SMSVendorClient
    {
        private ISMSVendor smsVendor;

        public SMSVendorClient(SMS sms)
        {
            if (sms.PhoneNumber[..3] == "+30")
            {
                smsVendor = new SMSVendorGR();
            } else if (sms.PhoneNumber[..4] == "+357")
            {
                smsVendor = new SMSVendorCY();
            } else
            {
                smsVendor = new SMSVendorRest();
            }
        }

        public async Task<string> Send(SMS sms, SMSRepository repository)
        {
            return await smsVendor.Send(sms, repository);
        }
    }
}
