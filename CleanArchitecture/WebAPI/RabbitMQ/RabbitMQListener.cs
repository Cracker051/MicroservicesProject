using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using MediatR;
using Application.Department.Commands.CreateDepartment;
using Application.Common.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.RabbitMQ
{
	public class RabbitMQListener:BackgroundService
	{
		private IConnection _connection;
		private IModel _channel;
		private IMediator _mediatr;
		public RabbitMQListener(IMediator mediatr)
		{
			var factory = new ConnectionFactory { HostName = Environment.GetEnvironmentVariable("RABBIT_ADDRESS") };
			_mediatr = mediatr;
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: "department", durable: false, exclusive: false);
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			stoppingToken.ThrowIfCancellationRequested();
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (ch, ea) =>
			{
				var content = Encoding.UTF8.GetString(ea.Body.ToArray());
				Console.WriteLine($"Recieved message => {content}");
				var departmentToCreate = JsonConvert
					.DeserializeObject<CreateDepartmentCommand>(content);
				await _mediatr.Send(departmentToCreate!);
				_channel.BasicAck(ea.DeliveryTag, false);
			};
			_channel.BasicConsume("department", false, consumer);
			return Task.CompletedTask;
		}

        public override void Dispose()
        {
			_channel.Close();
			_connection.Close();
            base.Dispose();
        }
    }
}

