using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Commands.Tags;
using TweetBook.Contracts.V1.Queries.Tags;

namespace TweetBook.Api.Controllers.V1
{
    
    public class TagsController : Controller
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid tagId)
        {
            var query = new GetTagByIdQuery(tagId);

            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);


           
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "MustWorkForGoogle")]
        [HttpGet(TweetBook.Contracts.V1.ApiRoutes.Tags.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var querry = new GetAllTagsQuery();

            var result = await _mediator.Send(querry);

            
            return Ok(result);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(TweetBook.Contracts.V1.ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagMergedCommand command)
        {
            var result = await _mediator.Send(command);



            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + Contracts.V1.ApiRoutes.Tags.Get.Replace("{tagId}", result.Id.ToString());

            

            return Created(locationUri, result);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(Contracts.V1.ApiRoutes.Tags.Update)]
        public async Task<IActionResult> Delete([FromRoute] Guid tagId)
        {
            var command = new DeleteTagCommand
            {
                Id = tagId
            };

            var result = await _mediator.Send(command);


            if (result)
                return NoContent();

            return NotFound();
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(Contracts.V1.ApiRoutes.Tags.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid tagId,[FromBody] UpdateTagCommand command)
        {
            var mergedCommand = new UpdateTagMergedCommand
            {
                Id = tagId,
                Name = command.Name
            };

            var result = await _mediator.Send(mergedCommand);


            if (result == null)
            {
                return BadRequest(new { error = "You do not own this tag or it doesn't exist!" });
            }

            return Ok(result);
        }

    }
}
