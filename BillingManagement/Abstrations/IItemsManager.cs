namespace BillingManagement.Abstrations
{
    public interface IItemsManager
    {
        void GetRecords();
        void GetRecord(Guid id);
        void InsertRecord();
        void DeleteRecord(Guid id);
        void UpdateRecord(Guid id);
    }
}
