using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Shared.API.ActionFilters
{
    public class PaginationHeaderAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as OkObjectResult;

            if (result != null)
            {
                dynamic response = result.Value;

                var paginationMetadata = new
                {
                    totalCount = response.TotalCount,
                    pageSize = response.PageSize,
                    currentPage = response.CurrentPage,
                    totalPages = response.TotalPages
                };

                context.HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

                result.Value = response.Data;
            }

            base.OnResultExecuting(context);
        }
    }
}
