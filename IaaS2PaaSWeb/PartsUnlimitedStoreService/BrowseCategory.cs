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
    public static class BrowseCategory
    {
        static BrowseCategory()
        {
            AutoMapperConfig.RegisterMappings();
        }

        [FunctionName("BrowseCategory")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            // parse query parameter
            string v = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "categoryId", true) == 0)
                .Value;

            if (!int.TryParse(v, out int categoryId))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid categoryId on the query string");
            }

            log.Info("C# HTTP trigger function processed a request.");

            var db = new DbModels.PartsUnlimitedContext();
            // Retrieve Category genre and its Associated associated Products products from database
            var genreModel = await db.Categories.Include("Products").SingleAsync(g => g.CategoryId == categoryId);

            return req.CreateResponse(HttpStatusCode.OK, AutoMapper.Mapper.Map<ApiModels.Category>(genreModel));
        }
    }
}
