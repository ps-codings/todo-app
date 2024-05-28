namespace TodoApp.API.Todos.CreateTodo;

public record CreateTodoCommand(string Name) : ICommand<CreateTodoResult>;
public record CreateTodoResult(Guid Id);

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required!");
    }
}

internal class CreateTodoCommandHandler(TodoDb dbContext)
    : ICommandHandler<CreateTodoCommand, CreateTodoResult>
{
    public async Task<CreateTodoResult> Handle(CreateTodoCommand command,
        CancellationToken cancellationToken)
    {
        var todo = new TodoItem
        {
            Name = command.Name
        };

        dbContext.TodoItems.Add(todo);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateTodoResult(todo.Id);
    }
}
