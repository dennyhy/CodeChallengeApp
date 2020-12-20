using System.Web.Http;
using WebActivatorEx;
using CodeChallengeApp;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CodeChallengeApp
{
    /*
     * SwaggerConfig class is added to project because of swagger documentation. 
     */

    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c => c.SingleApiVersion("v1", "CodeChallenge"))
              .EnableSwaggerUi();
        }
    }
}
