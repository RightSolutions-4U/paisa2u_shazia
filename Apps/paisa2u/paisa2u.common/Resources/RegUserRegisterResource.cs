namespace paisa2u.common.Resources
{
    public sealed record RegUserRegisterResource
    (
        int    regid,
        string Firstname,
        string Middlename,
        string Lastname,
        string Email,
        string Username,
        string Pwd,
        int Referredby,
        string Regtype,
        string Vendortype,
        string Phonenumber,
        DateTime Endate,
        string Enuser,
        string Substype,
        Boolean Regstatus,
        Boolean Autorenewal,
        string Qrpicture,
        string PasswordSalt,
        string PasswordHash,
        string vendorfilename
    );
}
