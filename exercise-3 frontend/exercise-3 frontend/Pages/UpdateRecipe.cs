using exercise_3_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace exercise_3_frontend.Pages
{
    public class UpdateRecipeModel : PageModel
    {
        public HttpClient HttpClient = new();
        [BindProperty]
        public Guid ID { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Ingredients { get; set; }
        [BindProperty]
        public string Instructions { get; set; }
        [BindProperty]
        public Guid[] Categories { get; set; } = new Guid[0];
        public async Task<IActionResult> OnPost()
        {
            Recipe toEdit = new Recipe("", new(), new(), new());
            toEdit.ID = ID;
            toEdit.Title = Title;
            foreach (Guid category in Categories)
            {
                toEdit.Categories.Add(category);
            }

            toEdit.Instructions = new();
            // using the method
            Instructions = Instructions.Trim();
            String[] strlist = Instructions.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                toEdit.Instructions.Add(s);
            }
            Ingredients = Ingredients.Trim();
            strlist = Ingredients.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                toEdit.Ingredients.Add(s);
            }


            var temp = JsonSerializer.Serialize(toEdit);
            var res = await HttpClient.PutAsync("https://localhost:7295/recipes/" + ID, new StringContent(temp, Encoding.UTF8, "application/json"));
            return Redirect("/Recipes");

        }

    }
}
