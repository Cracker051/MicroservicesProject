using System;
namespace NLayerApp.WebAPI.RabbitMQ
{
	public interface IRabbitMQProducer
	{
		public void SendDepartmentMessage<T>(T message);
	}
}

