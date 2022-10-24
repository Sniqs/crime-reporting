namespace CrimeApi.Exceptions;

public class NoOfficerAvailableException : Exception
{
	public NoOfficerAvailableException(string message) : base(message) { }
}
