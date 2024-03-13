using BusinessLayer;
using Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new(); 
factory.Uri = new("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sender App";

IConnection cnn = factory.CreateConnection();

IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-RoutingKey";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);

for(int i = 0; i < 60; i++)
{
    Console.WriteLine($"Sending User {i}");
    User user = await Create.GenerateRandomUser();
    string userJson = JsonConvert.SerializeObject(user);
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes(userJson);
    channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
    Thread.Sleep(1000);
}

channel.Close();
cnn.Close();
