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
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id:int}")]HttpRequestMessage req, int? id, TraceWriter log)
        {
            log.Info($"GetProductDetails {id}");

            if (!id.HasValue)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid product id");
            }

            var db = new DbModels.PartsUnlimitedContext();
            // Retrieve Product from database
            var product = await db.Products.SingleAsync(a => a.ProductId == id.Value);

            return req.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<ApiModels.Product>(product));
        }
    }
}
