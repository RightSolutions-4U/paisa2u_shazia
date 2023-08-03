//Added by Mohtashim 03-07-2023
namespace paisa2u.common.Resources
{
    public sealed record TransactionsResource
    (
        int tranid,
        int regId,
        double amount,
        DateTime endate,
        string enuser,
        string drcrid
    );

}
