using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Net;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        //Not really a Get, but just a PoC
        [HttpGet(Name = "GetMessage")]
        public ActionResult Get()
        {
            try
            {

                //
                // You should put the factory/model in the container, use a facade, etc. 
                // This is just a PoC.
                //

                ConnectionFactory factory = new()
                {          
                    UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER"),
                    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"),
                    VirtualHost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST"),
                    HostName = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")
                };

                IConnection conn = factory.CreateConnection();

                var model = conn.CreateModel();
                var queueName = "TestQueue";
                model.QueueDeclare(queueName);

                byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes("Hello, world!");
                model.BasicPublish(string.Empty, queueName, null, messageBodyBytes);

                var successMessage = "Message sent successfully";
                Console.WriteLine(successMessage);

                return Ok(successMessage);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to send message: {ex.Message}";
                Console.WriteLine(errorMessage);

                return new ObjectResult(errorMessage)
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError
                };
            }            
        }
    }
}
