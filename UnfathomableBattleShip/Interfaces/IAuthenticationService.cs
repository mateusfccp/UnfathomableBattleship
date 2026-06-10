using UnfathomableBattleship.Exceptions;

namespace UnfathomableBattleship.Interfaces;

public interface IAuthenticationService
{
    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <exception cref="RegistrationFailedException">Thrown if the username is already taken.</exception>
    void CreateUser(string username, string password);

    /// <summary>
    /// Tries to log in into the game and returns the game manager for the user if successful.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    /// <exception cref="AuthenticationFailedException">Thrown if the username or password is incorrect.</exception>
    IGameManager Login(string username, string password);
}
