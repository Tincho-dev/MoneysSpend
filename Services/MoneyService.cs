using Model;
using Repository;

namespace Services;

public class MoneyService : IMoneyService
{
    private readonly RecordContext _context;

    public MoneyService(RecordContext context)
    {
        _context = context;
    }

    public async Task SaveRecord(SpendRecord record)
    {
        await _context.SpendRecords.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task<IQueryable<SpendRecord>> GetRecords()
    {
        return _context.SpendRecords.AsQueryable();
    }

    public async Task EditRecord(SpendRecord record)
    {
        _context.SpendRecords.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecord(SpendRecord record)
    {
        _context.SpendRecords.Remove(record);
        await _context.SaveChangesAsync();
    }

    public async Task GetRecord(SpendRecord record)
    {
        record = await _context.SpendRecords.FindAsync(record.Id);
    }
}

public class MoneyInMemoryService : IMoneyService
{
    private List<SpendRecord> _records = new List<SpendRecord>();

    public MoneyInMemoryService()
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

    public async Task SaveRecord(SpendRecord record)
    {
        _records.Add(record);
    }

    public async Task<IQueryable<SpendRecord>> GetRecords()
    {
        return _records.AsQueryable();
    }

    public async Task EditRecord(SpendRecord record)
    {
        var index = _records.FindIndex(r => r.Id == record.Id);
        _records[index] = record;
    }

    public async Task DeleteRecord(SpendRecord record)
    {
        _records.Remove(record);
    }

    public async Task GetRecord(SpendRecord record)
    {
        var index = _records.FindIndex(r => r.Id == record.Id);
        record = _records[index];
    }
}

public interface IMoneyService
{
    Task SaveRecord(SpendRecord record);
    Task<IQueryable<SpendRecord>> GetRecords();
    Task EditRecord(SpendRecord record);
    Task DeleteRecord(SpendRecord record);
    Task GetRecord(SpendRecord record);
}
