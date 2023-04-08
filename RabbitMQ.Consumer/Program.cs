using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Bağlantı oluşturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://eqafabgk:VWWkNnuR6iuDu27BQyVoFB3NwWzRC0rV@chimpanzee.rmq.cloudamqp.com/eqafabgk");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();
channel.QueueDeclare("example-queue", true, false, false, null);

//Mesaj alma
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue:"example-queue", autoAck:false, consumer:consumer);
consumer.Received += (model, ea) =>
{
    byte[] body = ea.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine("Mesaj alındı: " + message);
};


