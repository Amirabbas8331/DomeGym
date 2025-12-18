using MediatR;
namespace GymManagement.Application.Common.Interfaces;

public interface ICommandBase<TResponse> : IRequest<TResponse> { }
