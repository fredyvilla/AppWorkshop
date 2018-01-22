using System.Collections.Generic;
using System.Data.Entity;
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
    public static class GetCategories
    {
        static GetCategories()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [FunctionName("GetCategories")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("Retrieving Product Categories");

            var db = new DbModels.PartsUnlimitedContext();
            var categories = await db.Categories.ToListAsync();

            return req.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<List<ApiModels.Category>>(categories));
        }
    }
}
