namespace Models.Interfaces
{
    public interface IUniqueUCustomerChecker
    {
        bool IsUserUnique(string customerEmail);
    }
}