namespace BookResale.Models.Dtos
{
    public class FileChunks
    {
        public string FileNameNoPath { get; set; } = "";
        public long offset { get; set; }
        public byte[] Data { get; set; }
        public bool FirstChunk = false;
    }
}
