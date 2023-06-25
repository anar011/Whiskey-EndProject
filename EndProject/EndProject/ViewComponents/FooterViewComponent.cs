using EndProject.Services.Interfaces;
using EndProject.ViewModels.Layout;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;

        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                GetSettingDatas = _layoutService.GetSettings()
            };
            return await Task.FromResult(View(model));
        }
    }
}
