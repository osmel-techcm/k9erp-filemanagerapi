namespace fileManagerCore.Entities
{
    public class FileItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public long Size { get; set; }

        public int Parent { get; set; }

        public int Type { get; set; }
    }
}
