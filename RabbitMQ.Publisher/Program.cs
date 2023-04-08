using System.Text;
using RabbitMQ.Client;

//Bağlantı oluşturma
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://eqafabgk:VWWkNnuR6iuDu27BQyVoFB3NwWzRC0rV@chimpanzee.rmq.cloudamqp.com/eqafabgk");

//Bağlantıyı aktifleştirme ve kanal açma
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

/*
 * Kuyruk oluşturma
 * durability: kuyruğun kalıcı olup olmayacağı
 * exclusive: kuyruğun sadece bir bağlantı tarafından kullanılacağı
 * autoDelete: kuyruğun bağlantı kapatıldığında silinip silinmeyeceği
 * arguments: kuyruğa ekstra özellikler eklemek için kullanılır
 */
channel.QueueDeclare("example-queue", durable:true, exclusive:false, autoDelete:false, arguments:null);

/*
 * Mesaj gönderme
 * exchange: mesajın gönderileceği exchange
 * routingKey: mesajın gönderileceği kuyruk
 * basicProperties: mesajın özellikleri
 * body: gönderilecek mesajın byte dizisi
 * rabbitMq kuyruğa atacağı mesajları byte dizisi olarak gönderir. Bu yüzden göndermek istediğimiz mesajı byte dizisine çevirmemiz gerekiyor.
 * Bunun için Encoding.UTF8.GetBytes() metodu kullanılır.
 */

channel.BasicPublish(exchange:"", routingKey:"example-queue", basicProperties:null, body:Encoding.UTF8.GetBytes("Hello World!"));

Console.WriteLine("Mesaj gönderildi");

Console.ReadLine();
    

