namespace GetLinksAnywhere.Model
{
    public class Chunk
    {
        public Chunk(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        public bool IsProcessed { get; set; }
    }
}
