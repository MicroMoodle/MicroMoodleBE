namespace AuthService.Application.TodoItems.Commands;

public class TodoItemValidatorConfiguration
{
    public const int MinimumTitleLength = 5;

    public const int MaximumTitleLength = 50;

    public const int MinimumBodyLength = 5;

    public const int MaximumBodyLength = 100;
}
