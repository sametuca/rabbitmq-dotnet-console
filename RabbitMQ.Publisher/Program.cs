using System.Text;
using RabbitMQ.Client;

//Bağlantı oluşturma
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://eqafabgk:VWWkNnuR6iuDu27BQyVoFB3NwWzRC0rV@chimpanzee.rmq.cloudamqp.com/eqafabgk");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Kuyruk oluşturma
//durability: kuyruğun kalıcı olup olmayacağı
//exclusive: kuyruğun sadece bir bağlantı tarafından kullanılacağı
//autoDelete: kuyruğun bağlantı kapatıldığında silinip silinmeyeceği
//arguments: kuyruğa ekstra özellikler eklemek için kullanılır
channel.QueueDeclare("example-queue", true, false, false, null);

//Mesaj gönderme
//exchange: mesajın gönderileceği exchange
//routingKey: mesajın gönderileceği kuyruk
//basicProperties: mesajın özellikleri
//body: gönderilecek mesajın byte dizisi
//rabbitMq kuyruğa atacağı mesajları byte dizisi olarak gönderir. Bu yüzden göndermek istediğimiz mesajı byte dizisine çevirmemiz gerekiyor.
//Bunun için Encoding.UTF8.GetBytes() metodu kullanılır.
channel.BasicPublish("", "example-queue", null, Encoding.UTF8.GetBytes("Hello World!"));

Console.WriteLine("Mesaj gönderildi");

Console.ReadLine();

// Path: RabbitMQ.Subscriber/Program.cs
// using System;
// using System.Text;
// using RabbitMQ.Client;
// using RabbitMQ.Client.Events;
//
//Bağlantı oluşturma
// ConnectionFactory factory = new ConnectionFactory();
// factory.Uri = new Uri("amqps://eqafabgk:");
//
//Bağlantıyı aktifleştirme ve kanal açma
// using IConnection connection = factory.CreateConnection();
// using IModel channel = connection.CreateModel();

//Kuyruk oluşturma
//durability: kuyruğun kalıcı olup olmayacağı
//exclusive: kuyruğun sadece bir bağlantı tarafından kullanılacağı
//autoDelete: kuyruğun bağlantı kapatıldığında silinip silinmeyeceği
//arguments: kuyruğa ekstra özellikler eklemek için kullanılır
// channel.QueueDeclare("example-queue", true, false, false, null);

//Mesaj alma
//queue: mesajın alınacağı kuyruk
//autoAck: mesajın otomatik olarak onaylanıp onaylanmayacağı
//consumer: mesajın alınacağı consumer
// channel.BasicConsume("example-queue", true, new EventingBasicConsumer(channel));
    

//Mesaj alındığında tetiklenecek event
// ((EventingBasicConsumer)channel.BasicGet("example-queue", true)).Received += (sender, e) =>
// {
//     Console.WriteLine(Encoding.UTF8.GetString(e.Body.ToArray()));
// };

//Console.ReadLine();

//RabbitMQ ile birlikte gelen bir web arayüzü olan RabbitMQ Management Plugin ile kuyrukların durumunu görebiliriz. RabbitMQ Management Plugin’i kurmak için aşağıdaki komutu çalıştırabiliriz.
//sudo rabbitmq-plugins enable rabbitmq_management
    

