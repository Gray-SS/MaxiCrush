using FluentResults;
using MaxiCrush.Domain.Entities;
using MediatR;

namespace MaxiCrush.Application.Controls.Roles.Commands.Create;

public record CreateRoleCommand(
    Guid SenderId,
    string Name,
    int Power) : IRequest<Result<Role>>;
