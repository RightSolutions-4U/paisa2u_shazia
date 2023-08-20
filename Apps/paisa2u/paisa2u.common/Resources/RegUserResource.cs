using System;

namespace paisa2u.common.Resources
{
    public sealed record RegUserResource(
        int Regid,
        string Firstname,
        string Middlename,
        string Lastname,
        string Email,
        string Username,
        string Pwd,
        string Referredby,
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

