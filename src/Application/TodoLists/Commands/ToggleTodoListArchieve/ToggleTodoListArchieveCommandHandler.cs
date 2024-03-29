using Application.Common.Errors;
using Application.Core;

using Domain.TodoLists;

using MediatR;

namespace Application.TodoLists.Commands.ToggleTodoListArchieve;

public class ToggleTodoListArchieveCommandHandler(ITodoListRepository todoListRepository)
    : IRequestHandler<ToggleTodoListArchieveCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(ToggleTodoListArchieveCommand command, CancellationToken cancellationToken)
    {
        var todoList = await todoListRepository.GetByIdAsync(command.TodoListId, cancellationToken);

        if (todoList?.UserId != command.UserId)
        {
            return TodoListErrors.NotFound;
        }

        todoList.ToggleArchived(command.IsArchived);

        await todoListRepository.UpdateAsync(todoList, cancellationToken);

        return Unit.Value;
    }
}