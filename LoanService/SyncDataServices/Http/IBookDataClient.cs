namespace LoanService.SyncDataServices.Http
{
    public interface IBookDataClient
    {
        Task<int> GetBookCountAsync(int bookID);
        Task UpdateBookCount(int bookId, int n);
    }
}
