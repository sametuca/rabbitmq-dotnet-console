using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Bağlantı oluşturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://eqafabgk:VWWkNnuR6iuDu27BQyVoFB3NwWzRC0rV@chimpanzee.rmq.cloudamqp.com/eqafabgk");

//Bağlantıyı aktifleştirme ve kanal açma
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
/*
 * Kuyruk oluşturma
 * durable: kuyruğun kalıcı olup olmayacağı
 * exclusive: kuyruğun sadece bir bağlantı tarafından kullanılacağı
 * autoDelete: kuyruğun bağlantı kapatıldığında silinip silinmeyeceği
 * arguments: kuyruğa ekstra özellikler eklemek için kullanılır
 */
channel.QueueDeclare("example-queue", true, false, false, null);

//Mesaj alma
/*
 * autoAck: mesajın otomatik olarak onaylanıp onaylanmayacağı
 * consumer: mesajı alacak olan nesne
 */
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue:"example-queue", autoAck:false, consumer:consumer);
consumer.Received += (sender, e) =>
{
    byte[] body = e.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    channel.BasicAck(deliveryTag:e.DeliveryTag, multiple:false);
    Console.WriteLine("Mesaj alındı: " + message);
};


