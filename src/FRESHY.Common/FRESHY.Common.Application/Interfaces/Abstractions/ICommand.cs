using MediatR;

namespace FRESHY.Common.Application.Interfaces.Abstractions;

public interface ICommand : IRequest
{
}

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}