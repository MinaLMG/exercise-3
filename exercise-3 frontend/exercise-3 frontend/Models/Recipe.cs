namespace exercise_3_frontend.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();
        public List<Guid> Categories { get; set; } = new();
        public Guid ID { get; set; }
    }
}
