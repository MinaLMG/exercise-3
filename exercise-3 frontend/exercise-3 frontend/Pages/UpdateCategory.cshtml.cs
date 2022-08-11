using exercise_3_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace exercise_3_frontend.Pages
{
    public class UpdateCategoryModel : PageModel
    {
        public HttpClient HttpClient = new();
        [BindProperty]
        public Guid ID { get; set; }
        [BindProperty]
        public string Name { get; set; }
        private readonly IConfiguration Configuration;

        public UpdateCategoryModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<IActionResult>OnPost()
        {
            Category toEdit = new Category(Name);
            toEdit.ID = ID;
            var temp = JsonSerializer.Serialize(toEdit);
            var res = await HttpClient.PutAsync(Configuration["BaseUrl"]+"categories/" + ID, new StringContent(temp, Encoding.UTF8, "application/json"));
            if ((int)res.StatusCode == 200)
                return Redirect("/Categories?ReqResult=success&Msg=the category has been updated successfully");
            else
                return Redirect("/Categories?ReqResult=failure&Msg=something went wrong with your request .. review your data and try again");

        }
    }
}
