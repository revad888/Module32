namespace Module32.Models.Db
{
    public interface IRequestRepository
    {
        Task AddReuest(Request request);
        Task<Request[]> GetRequests();
    }
}

