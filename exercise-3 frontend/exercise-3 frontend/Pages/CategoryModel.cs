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
        private readonly IConfiguration Configuration;
        public CategoryModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public async Task OnGet()
        {
           await ListCategories();
        }
        public async Task ListCategories()
        {
            var res = await HttpClient.GetAsync(Configuration["BaseUrl"]+"categories");
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
            var res = await HttpClient.PostAsync(Configuration["BaseUrl"]+"categories", new StringContent(temp, Encoding.UTF8, "application/json"));
            return Redirect("/Categories");
        }
    }
}