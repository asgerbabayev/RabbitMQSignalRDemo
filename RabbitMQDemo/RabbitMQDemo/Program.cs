using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQDemo.Models;
using RabbitMQ.Client.Events;
using Microsoft.AspNetCore.SignalR.Client;

HubConnection hubConnection = new HubConnectionBuilder().WithUrl("https://localhost:7133/chathub").Build();
await hubConnection.StartAsync();
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://tgfofbye:pmMAT3zJLy502QwNPqCPqzjhAv2N2ljh@cougar.rmq.cloudamqp.com/tgfofbye");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
channel.QueueDeclare("messagequeue", false, false, true);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("messagequeue", true, consumer);
consumer.Received += async (sender, e) =>
{
    User user = JsonSerializer.Deserialize<User>(Encoding.UTF8.GetString(e.Body.Span));
    //EmailSender.SendEmail(user.Email, "Report", user.Message);
    Console.WriteLine($"{user.Email} adresine mesaj gonderildi");
    await hubConnection.InvokeAsync("SendMessage", $"{user.Email} adresine mesaj gonderildi");
};

Console.Read();