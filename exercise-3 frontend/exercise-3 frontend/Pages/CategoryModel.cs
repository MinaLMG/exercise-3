using exercise_3_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace exercise_3_frontend.Pages
{
    public class CategoryModel : PageModel
    {
        public HttpClient HttpClient = new();
        public List<Category> Categories = new ();
        [BindProperty]
        public string Name { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public CategoryModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
           await ListCategories();
        }
        public async Task ListCategories()
        {
            var res = await HttpClient.GetAsync("https://localhost:7295/categories");
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var inBetween = res.Content.ReadAsStringAsync().Result;
            List<Category> categories = JsonSerializer.Deserialize<List<Category>>(inBetween, serializeOptions);
            this.Categories= categories;
        }
        public async Task<IActionResult> OnPost()
        {
            Category toAdd = new Category(Name);
            var temp = JsonSerializer.Serialize(toAdd);
            var res = await HttpClient.PostAsync("https://localhost:7295/categories", new StringContent(temp, Encoding.UTF8, "application/json"));
            return Redirect("/Categories");
        }
    }
}