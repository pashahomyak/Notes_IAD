namespace Notes.Dto
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public bool IsFavorites { get; set; }
        public string ImageData { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}