using Microsoft.EntityFrameworkCore;

namespace Module32.Models.Db
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext _context;
        public RequestRepository(BlogContext context) 
        {
            _context = context;
        }
        public async Task AddReuest(Request request)
        {
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }
    }
}
