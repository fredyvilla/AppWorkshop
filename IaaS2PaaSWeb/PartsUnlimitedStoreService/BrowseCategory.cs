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
    public static class BrowseCategory
    {
        static BrowseCategory()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [FunctionName("BrowseCategory")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categories/{id:int}")]HttpRequestMessage req, int? id, TraceWriter log)
        {
            log.Info($"Get Product Category {id}");

            if (!id.HasValue)
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid category ID");

            var db = new DbModels.PartsUnlimitedContext();
            var genreModel = await db.Categories.Include("Products").SingleAsync(g => g.CategoryId == id.Value);

            return req.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<ApiModels.Category>(genreModel));
        }
    }
}
