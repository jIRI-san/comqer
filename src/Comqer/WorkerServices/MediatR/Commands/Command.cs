using MediatR;

namespace Comqer.WorkerServices.MediatR.Commands;

public class Command(string input) : IRequest<string> {
  private readonly string _input = input;

  public string Input => _input;
}