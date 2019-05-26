using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltamiraShared.Models;
using AltamiraShared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace AltamiraProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IOptions<QueueSettingsModel> _config;
        private readonly ProductContext _context;
        public ProductsController(IOptions<QueueSettingsModel> config, ProductContext context)
        {
            _config = config;
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var products = _context.Product.ToList();
            return products;
        }

        // GET: api/Products/1
        [HttpGet]
        [Route("{id}")]
        public Product Get(int id)
        {
            var product = _context.Product.FirstOrDefault(m => m.Id == id);
            return product;
        }

        // POST api/products
        // body de gelen product ı queue ya yazar
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            RabbitMQService factory = new RabbitMQService(_config);

            using (var connection = factory.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: product.Name,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    var productSerialized = JsonConvert.SerializeObject(product);

                    var body = Encoding.UTF8.GetBytes(productSerialized);

                    channel.BasicPublish(exchange: "",
                                        routingKey: product.Name,
                                        basicProperties: null,
                                        body: body);
                }
            }
        }        
       
    }
}
