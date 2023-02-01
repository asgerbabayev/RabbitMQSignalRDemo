using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SignalRExample.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace SignalRExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Chat(User user)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://tgfofbye:pmMAT3zJLy502QwNPqCPqzjhAv2N2ljh@cougar.rmq.cloudamqp.com/tgfofbye");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            string serializedData = JsonSerializer.Serialize(user);
            byte[] byteData = Encoding.UTF8.GetBytes(serializedData);
            channel.QueueDeclare("messagequeue", false, false, true);
            channel.BasicPublish("", routingKey: "messagequeue", body: byteData);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}