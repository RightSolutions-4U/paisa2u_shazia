namespace paisa2u.common.Resources
{
    public sealed record SubscriptionResource
    (
        int Recid,
        int RegId,
        float Appowner,
        float Vendor,
        float Subvendor,
        float Customer,
        DateTime Endate,
        string Enuser
);
}