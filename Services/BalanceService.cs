using Model;
using Repository;

namespace Services;

public class BalanceService //: IBalanceService
{
    private readonly RecordContext _context;

    public BalanceService(RecordContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetBalance()
    {
        return (decimal)_context.SpendRecords.Sum(r => r.Amount);
    }

    public async Task<IEnumerable<string>> GetMonths()
    {
        return _context.SpendRecords.Select(r => r.Date.ToString("MMMM"));
    }
}

public class BalanceServiceInMemory : IBalanceService
{
    private List<SpendRecord> _records = new List<SpendRecord>();
    private string Month = DateTime.Now.ToString("MMMM");

    public BalanceServiceInMemory()
    {
        _records.AddRange(
            new List<SpendRecord>
            {
                new SpendRecord
                {
                    Id = 1,
                    Description = "First record",
                    Date = DateTime.Now,
                    Amount = 100
                },
                new SpendRecord
                {
                    Id= 2,
                    Description = "Second record",
                    Date = DateTime.Now,
                    Amount = 200
                }
            });
    }

    public async Task<decimal> GetBalance()
    {
        return (decimal)_records.Sum(r => r.Amount);
    }

    public async Task<IEnumerable<string>> GetMonths()
    {
        return _records.Select(r => r.Date.ToString("MMMM"));
    }

    public async Task ChangeMonth(string month)
    {
        Month = month;
    }

    public async Task<decimal> GetBalanceByType(SpendType type)
    {
        return (decimal)_records.Where(r => r.Date.ToString("MMMM") == Month && r.Type == type).Sum(r => r.Amount);
    }
}

public interface IBalanceService
{
    Task<decimal> GetBalance();
    
    Task<IEnumerable<string>> GetMonths();

    Task ChangeMonth(string month);

    Task<decimal> GetBalanceByType(SpendType type);
}
