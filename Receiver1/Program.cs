﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Models;
using DataLayer;
using Newtonsoft.Json;
using BusinessLayer;

ConnectionFactory factory = new();
factory.Uri = new("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Receiver (1) App";

IConnection cnn = factory.CreateConnection();

IModel channel = cnn.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-RoutingKey";
string queueName = "DemoQueue";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);
channel.BasicQos(0, 1, false);  

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender,args) =>
{
    Task.Delay(5000).Wait();
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);

    // Deserialisera JSON-strängen till ett User-objekt
    var user = JsonConvert.DeserializeObject<User>(message);

    // Spara användaren i din databas
    Create.SaveNewUserToDb(user);

    Console.WriteLine($"User saved to Database: {user.FirstName} {user.LastName}");

    // Bekräfta att meddelandet har tagits emot och behandlats
    channel.BasicAck(args.DeliveryTag, false);
};

string consumerTag = channel.BasicConsume(queueName, false, consumer);
Console.ReadLine();

channel.BasicCancel(consumerTag);
channel.Close();
cnn.Close();
