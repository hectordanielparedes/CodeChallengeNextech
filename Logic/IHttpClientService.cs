namespace Logic
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
