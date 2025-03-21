namespace Comqer.WorkerServices;

public interface IWorkerServiceSelector {
    Task<IWorkerService> GetService();
}