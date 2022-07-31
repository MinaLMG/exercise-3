using exercise_3_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace exercise_3_frontend.Pages
{
    public class RecipeModel : PageModel
    {
        public HttpClient HttpClient = new();
        public List<Recipe> Recipes = new();
        public List<Category> Categories = new();
        public Dictionary<Guid, string> categoriesNamesMap = new Dictionary<Guid, string>();

        //[BindProperty]
        //public string Name { get; set; }
        private readonly ILogger<IndexModel> _logger;

        public RecipeModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            await ListCategories();
        }
        public async Task ListCategories()
        {

            var res = await HttpClient.GetAsync("https://localhost:7295/recipes");
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var inBetween = res.Content.ReadAsStringAsync().Result;
            List<Recipe> recipes = JsonSerializer.Deserialize<List<Recipe>>(inBetween, serializeOptions);
            this.Recipes = recipes;
            /* getting categories */
            res = await HttpClient.GetAsync("https://localhost:7295/categories");
            inBetween = res.Content.ReadAsStringAsync().Result;
            List<Category> categories = JsonSerializer.Deserialize<List<Category>>(inBetween, serializeOptions);
            this.Categories = categories;
            /* storing some dictionaries*/
            //Dictionary<string, Guid> categoriesMap = new Dictionary<string, Guid>();
            for (int i = 0; i < categories.Count; i++)
            {
                //categoriesMap[categories[i].Name] = categories[i].ID;
                this.categoriesNamesMap[categories[i].ID] = categories[i].Name;
            }
        }
    }
}