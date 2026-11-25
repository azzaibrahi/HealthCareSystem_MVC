namespace HealthCareSystem_MVC.BLL.Models;

/// <summary>Service result carrying either data or an error.</summary>
public sealed class Result<T>
{
    public bool Successeded { get; }

    public T? Data { get; }

    public string? Error { get; }

    private Result(bool successeded, T? data, string? error = null)
    {
        Successeded = successeded;
        Data = data;
        Error = error;
    }

    public static Result<T> Success(T data) => new(true, data);

    public static Result<T> Failure(string error) => new(false, default, error);
}
