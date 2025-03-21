namespace Comqer.WorkerServices;

public interface IWorkerService {
    Task<string> Command(string input);
    Task<string> Query(string input);
}