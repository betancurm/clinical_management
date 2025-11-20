namespace UserService.Exceptions;

public class UserNotFoundException : Exception
{
    public int UserId { get; }

    public UserNotFoundException(int userId) 
        : base($"Usuario con ID {userId} no encontrado.")
    {
        UserId = userId;
    }
}

public class DuplicateCedulaException : Exception
{
    public string Cedula { get; }

    public DuplicateCedulaException(string cedula)
        : base($"Ya existe un usuario con la cédula: {cedula}")
    {
        Cedula = cedula;
    }
}

public class DuplicateUsernameException : Exception
{
    public string NombreUsuario { get; }

    public DuplicateUsernameException(string nombreUsuario)
        : base($"Ya existe un usuario con el nombre de usuario: {nombreUsuario}")
    {
        NombreUsuario = nombreUsuario;
    }
}

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
        : base("Credenciales inválidas.")
    {
    }
}

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("No autorizado para realizar esta operación.")
    {
    }
}