namespace SeizureTrackerBlazer.Models;

public class ServiceResult<T>(T? data, string? errorMessage = null)
{
    public T? Data { get; } = data;
    public string? ErrorMessage { get; } = errorMessage;
    public bool Success => ErrorMessage == null;

    // Static helper for Success
    public static ServiceResult<T> Ok(T data) 
        => new ServiceResult<T>(data, null);

    // Static helper for Failure
    public static ServiceResult<T> Fail(string message) 
        => new ServiceResult<T>(default, message);
}