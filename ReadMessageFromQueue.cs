using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Mail;

namespace AzServiceBusDemo
{
    public static class ReadMessageFromQueue
    {
        //[FunctionName("ReadMessageFromQueue")]
        //public static void Run([ServiceBusTrigger("az-queue")]string myQueueItem, ILogger log)
        //{
        //    log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        //}


        [FunctionName("ReadMessageFromQueue")]
        [return: SendGrid(ApiKey = "SendGridApiKey")]
        public static SendGridMessage Run([ServiceBusTrigger("az-queue")] string myQueueItem, ILogger log)
        {
            //  log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
           // dynamic DynamicData = JsonConvert.DeserializeObject(myQueueItem);
            dynamic details = myQueueItem.Split(":")[1].TrimEnd(')');
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("mukesh.prajapati@rsystems.com", "Mukesh Prajapati"),
                Subject = "Sending emails with Twilio SendGrid is Fun",
                PlainTextContent = details,
                HtmlContent = details
            };
            msg.AddTo(new EmailAddress("prajapatimukesh9@gmail.com", "prajapatimukesh"));

            return msg;

        }
    }
}
