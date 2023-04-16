namespace BillingManagement.Repository.Abstrations;

public interface IItemsRepository
{
    void GetRecords();
    void GetRecord(Guid id);
    void InsertRecord();
    void DeleteRecord(Guid id);
    void UpdateRecord(Guid id);
}
