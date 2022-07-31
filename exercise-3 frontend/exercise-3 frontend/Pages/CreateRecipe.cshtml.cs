using exercise_3_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace exercise_3_frontend.Pages
{
    public class CreateRecipeModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Ingredients { get; set; }
        [BindProperty]
        public string Instructions { get; set; }
        [BindProperty]
        public Guid[] Categories { get; set; } = new Guid[0];
        public HttpClient HttpClient = new();
        public async Task<IActionResult> OnPost()
        {
            Recipe toAdd = new Recipe();
            toAdd.Title = Title;
            toAdd.Instructions = new();
            // using the method
            String[] strlist = Instructions.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                toAdd.Instructions.Add(s);
            }
             strlist = Ingredients.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                toAdd.Ingredients.Add(s);
            }

            
                var temp = JsonSerializer.Serialize(toAdd);
                var res = await HttpClient.PostAsync("https://localhost:7295/recipes", new StringContent(temp, Encoding.UTF8, "application/json"));
                return Redirect("/Recipes");
            
        }
    }
}
