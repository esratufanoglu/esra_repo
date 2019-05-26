using AltamiraShared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreApiClient
{
    public partial class ApiClient
    {
        public async Task<Message<Product>> CreateProduct(Product model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "products"));
            return await PostAsync<Product>(requestUrl, model);
        }

        public async Task<List<Product>> GetProducts()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "products"));
            return await GetAsync<List<Product>>(requestUrl);
        }

        public async Task<Product> GetProduct(int id)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "products/"+id));
            return await GetAsync<Product>(requestUrl);
        }

    }
}
