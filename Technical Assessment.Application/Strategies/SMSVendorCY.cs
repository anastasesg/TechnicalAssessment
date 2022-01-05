using Technical_Assessment.Application.Common;
using Technical_Assessment.Core.Entities;
using Technical_Assessment.Infrastructure.Repositories;

namespace Technical_Assessment.Application.Strategies
{
    public class SMSVendorCY : ISMSVendor
    {
        public async Task<string> Send(SMS sms, SMSRepository repository)
        {
            // Check that message length is not greater than 160 chars.
            if (sms.Message.Length <= 160)
            {
                await repository.AddAsync(sms);
            } else
            {
                var messages = new MessageSplitter().Split(sms.Message, 160);
                foreach (var message in messages)
                {
                    SMS tmpSMS = new()
                    {
                        PhoneNumber = sms.PhoneNumber,
                        Message = message
                    };
                    await repository.AddAsync(tmpSMS);
                }
            }
            return "Ok";
        }
    }
}
