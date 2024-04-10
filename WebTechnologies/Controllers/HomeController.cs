using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebTechnologies.Controllers
{
	public class HomeController : Controller
	{
		private List<ListDemo> _listDemo;
		public HomeController()
		{
			_listDemo = new List<ListDemo>
				{
				new ListDemo{ ListItemValue=1, ListItemText="Item 1"},
				new ListDemo{ ListItemValue=2, ListItemText="Item 2"},
				new ListDemo{ ListItemValue=3, ListItemText="Item 3"}
				};
		}

		[ViewData]
		public string Text { get; set; }
		public IActionResult Index()
		{
			Text = "Лабораторная работа 2";
			ViewData["Lst"] = new SelectList(_listDemo, "ListItemValue", "ListItemText");

			return View();
		}

	}
	public class ListDemo
	{
		public int ListItemValue { get; set; }
		public string ListItemText { get; set; }
	}

}
