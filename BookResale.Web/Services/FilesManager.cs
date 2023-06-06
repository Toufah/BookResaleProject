using BookResale.Models.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BookResale.Web.Services
{
    public class FilesManager
    {
        HttpClient http;

        public FilesManager(HttpClient _http)
        {
            http = _http;
        }

        public async Task<List<string>> GetFileNames()
        {
            try
            {
                var result = await http.GetAsync("files");
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<string>>(responseBody);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UploadFileChunk(FileChunks FileChunk)
        {
            try
            {
                var result = await http.PostAsJsonAsync("files", FileChunk);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                return Convert.ToBoolean(responseBody);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
