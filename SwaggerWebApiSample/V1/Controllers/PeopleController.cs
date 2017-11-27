namespace Microsoft.Examples.V1.Controllers
{
    using Microsoft.Examples.Filter;
    using Microsoft.Web.Http;
    using Models;
    using System.Diagnostics;
    using System.Web.Http;
    using System.Web.Http.Description;

    /// <summary>
    /// Represents a RESTful people service.
    /// </summary>
    [ApiVersion( "1.0", Deprecated = true )]
    //[ApiVersion( "0.9", Deprecated = true )]
    [RoutePrefix( "api/v{api-version:apiVersion}/people" )]
    public class PeopleController : ApiController
    {
        /// <summary>
        /// Gets a single person.
        /// </summary>
        /// <param name="id">The requested person identifier.</param>
        /// <returns>The requested person.</returns>
        /// <response code="200">The person was successfully retrieved.</response>
        /// <response code="404">The person does not exist.</response>
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Person))]
        [RequestAuthorize]
        public IHttpActionResult Get(int id)
        {
            Debug.WriteLine("11");
            return  Ok(
                new Person()
                {
                    Id = id,
                    FirstName = "John",
                    LastName = "Doe"
                }
            );
        }
    }
}