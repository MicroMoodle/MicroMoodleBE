namespace UserService.Application.Models;

public class ApiResult<T>
{
    private ApiResult() { }

    private ApiResult(int statusCode, bool succeeded, T? result, IEnumerable<string> errors, string? message = null)
    {
        StatusCode = statusCode;
        Succeeded = succeeded;
        Result = result;
        Errors = errors;
        Message = message;
    }

    public int StatusCode { get; set; }

    public bool Succeeded { get; set; }

    public T? Result { get; set; }

    public IEnumerable<string> Errors { get; set; }

    public string? Message { get; set; }

    public static ApiResult<T?> Success(int statusCode, T? result, string message)
    {
        return new ApiResult<T?>(statusCode, true, result, new List<string>(), message);
    }

    public static ApiResult<T?> Failure(int statusCode, IEnumerable<string> errors)
    {
        return new ApiResult<T?>(statusCode, false, default, errors);
    }
}
