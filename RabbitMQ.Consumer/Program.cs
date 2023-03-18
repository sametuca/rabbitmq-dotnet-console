using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://eqafabgk:VWWkNnuR6iuDu27BQyVoFB3NwWzRC0rV@chimpanzee.rmq.cloudamqp.com/eqafabgk");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
channel.QueueDeclare("example-queue", true, false, false, null);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Mesaj alındı: " + message);
};


