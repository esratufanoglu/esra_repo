using AltamiraShared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraShared.Services
{
    public class RabbitMQService
    {
        private readonly IOptions<QueueSettingsModel> configuration;
        private string _hostName;
        private string _userName;
        private string _password;
        public static string SerialisationQueueName;
        public RabbitMQService(IOptions<QueueSettingsModel> iConfig)
        {
            configuration = iConfig;
            _hostName = configuration.Value.HostName;
            _userName = configuration.Value.Username;
            _password = configuration.Value.Password;
            SerialisationQueueName = configuration.Value.QueueName;
    }        

        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            return connectionFactory.CreateConnection();
        }
    }
}
