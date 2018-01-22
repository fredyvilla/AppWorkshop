using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using DbModels = PartsUnlimited.Models;
using ApiModels = PartsUnlimitedStoreService.Models;
using PartsUnlimitedStoreService.App_Start;

namespace PartsUnlimitedStoreService
{
    public static class GetProductDetails
    {
        static GetProductDetails()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [FunctionName("GetProductDetails")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            // parse query parameter
            string v = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "id", true) == 0)
                .Value;

            if (!int.TryParse(v, out int id))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid id on the query string");
            }

            log.Info($"GetProductDetails {id}");

            var db = new DbModels.PartsUnlimitedContext();
            // Retrieve Product from database
            var product = await db.Products.SingleAsync(a => a.ProductId == id);

            return req.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<ApiModels.Product>(product));
        }
    }
}
