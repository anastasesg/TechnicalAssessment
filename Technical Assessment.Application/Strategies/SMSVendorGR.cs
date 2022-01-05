using System.Text.RegularExpressions;
using Technical_Assessment.Application.Common;
using Technical_Assessment.Core.Entities;
using Technical_Assessment.Infrastructure.Repositories;

namespace Technical_Assessment.Application.Strategies
{
    public class SMSVendorGR : ISMSVendor
    {
        public async Task<string> Send(SMS sms, SMSRepository repository)
        {
            // Check that message only contains Greek chars.
            string[] messageWords = sms.Message.Split(" ");
            Regex regex = new Regex(@"\p{IsGreekandCoptic}");
            foreach (string word in messageWords)
            {
                if (!regex.IsMatch(word)) return "Message must only contain Greek characters";
            }

            // Check if message length is greater than 480 chars.
            if (sms.Message.Length <= 480)
            {
                await repository.AddAsync(sms);
            } else
            {
                var messages = new MessageSplitter().Split(sms.Message, 480);
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
