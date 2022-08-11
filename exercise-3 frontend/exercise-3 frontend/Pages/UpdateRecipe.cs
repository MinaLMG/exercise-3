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
        private readonly IConfiguration Configuration;

        public UpdateRecipeModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<IActionResult> OnPost()
        {
            Recipe toEdit = new Recipe("", new(), new(), new());
            toEdit.ID = ID;
            toEdit.Title = Title.Trim();
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
                if (s.Trim() != "")
                    toEdit.Instructions.Add(s.Trim());
            }
            Ingredients = Ingredients.Trim();
            strlist = Ingredients.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                if (s.Trim() != "")
                    toEdit.Ingredients.Add(s.Trim());
            }


            var temp = JsonSerializer.Serialize(toEdit);
            var res = await HttpClient.PutAsync(Configuration["BaseUrl"]+"recipes/" + ID, new StringContent(temp, Encoding.UTF8, "application/json"));
            if ((int)res.StatusCode == 200)
                return Redirect("/recipes?ReqResult=success&Msg=the recipe has been updated successfully");
            else
                return Redirect("/recipes?ReqResult=failure&Msg=something went wrong with your request .. review your data and try again&open=edit&title=" + Title+"&id="+ID); /*+ "&instructions=" + Instructions + "&ingredients=" + Ingredients);*/

        }

    }
}
