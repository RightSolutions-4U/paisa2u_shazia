namespace paisa2u.common.Resources
{
    public sealed record SubscriptionSetupResource
    (
        int Subsid,
        string Subtype, //G,D,P
        int Subfee,
        double Appowner,
        double Vendor,
        double Subvendor,
        double Customer,
        DateTime Endate,
        string Enuser
    );
}
