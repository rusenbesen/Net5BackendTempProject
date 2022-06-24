using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using TempProject.Core.ViewModels;
using TempProject.Core.Entity;
using TempProject.Core.Services;

namespace TempProject.Web.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            var errorViewModel = new ErrorViewModel();
            errorViewModel.Errors.Add($"{typeof(T).Name} not found");
            context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);
        }
    }
}
