namespace UnfathomableBattleship.Exceptions;

/// <summary>
/// Thrown when an authentication failed.
/// </summary>
public class AuthenticationFailedException() : Exception("Authentication failed. Username or password is incorrect.");
