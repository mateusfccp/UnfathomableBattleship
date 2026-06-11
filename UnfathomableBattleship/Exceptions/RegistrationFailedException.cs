namespace UnfathomableBattleship.Exceptions;

/// <summary>
/// Thrown when a user registration failed.
/// </summary>
public class RegistrationFailedException() : Exception("Registration failed. The username is already taken.");
