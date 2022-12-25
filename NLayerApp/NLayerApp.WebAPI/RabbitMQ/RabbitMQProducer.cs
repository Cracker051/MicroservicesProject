using System;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
namespace NLayerApp.WebAPI.RabbitMQ
{
	public class RabbitMQProducer:IRabbitMQProducer
	{
		public void SendDepartmentMessage<T>(T message)
		{
			var factory = new ConnectionFactory
			{
				HostName = Environment.GetEnvironmentVariable("RABBIT_ADDRESS")
			};
			var connection = factory.CreateConnection();
			using
			var channel = connection.CreateModel();
			channel.QueueDeclare("department", exclusive: false);
			var json = JsonConvert.SerializeObject(message, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
			var body = Encoding.UTF8.GetBytes(json);
			channel.BasicPublish(exchange: "", routingKey: "department", body: body);

		}
	}
}

