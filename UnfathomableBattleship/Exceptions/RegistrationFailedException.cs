namespace UnfathomableBattleship.Exceptions;

/// <summary>
/// Thrown when a user registration failed.
/// </summary>
public class RegistrationFailedException(string v) : Exception("Registration failed. The username is already taken.");
