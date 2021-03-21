namespace Marketplace.Domain.Seed
{
    public interface IBusinessRule
    {

        string Message { get; }
        bool IsBroken();
    }
}
