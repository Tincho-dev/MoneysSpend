namespace Model;

public class SpendRecord
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public Category Category { get; set; } = new Category();
    public double Amount {  get; set; }
    public SpendType Type { get; set; }
}

public enum SpendType
{
    Income,
    Expense,
    Saving
    //Investment,
    //Debt,
    //Transfer,
    //Other
}

public class SpendRecordDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public Category Category { get; set; } = new Category();
    public double Amount {  get; set; }
}

public class SpendRecordMapper
{
    public static SpendRecordDto Map(SpendRecord record)
    {
        return new SpendRecordDto
        {
            Id = record.Id,
            Description = record.Description,
            Date = record.Date,
            Amount = record.Amount
        };
    }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Logo { get; set; } = string.Empty;
}