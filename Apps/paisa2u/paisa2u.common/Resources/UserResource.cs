namespace paisa2u.common.Resources
{
    public sealed record UserResource(
        string Username,
        string Email,
        string Pwd,
        string adminrole,
        string JwtToken
        );
}
