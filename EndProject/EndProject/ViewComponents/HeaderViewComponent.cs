using EndProject.Services.Interfaces;
using EndProject.ViewModels.Layout;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public HeaderViewComponent(ILayoutService layoutService
                        )
        {
            _layoutService = layoutService;
         
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                GetSettingDatas = _layoutService.GetSettings(),
             
            };
            return await Task.FromResult(View(model));
        }
    }
}
