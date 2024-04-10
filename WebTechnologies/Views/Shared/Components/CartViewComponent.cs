using Microsoft.AspNetCore.Mvc;

namespace WebTechnologies.Views.Shared.Components
{
	public class CartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}

}
