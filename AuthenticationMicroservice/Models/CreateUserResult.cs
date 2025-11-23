namespace AuthenticationMicroservice.Models;

public class CreateUserResult
{
    public User User { get; set; }
    public string GeneratedPassword { get; set; }
}