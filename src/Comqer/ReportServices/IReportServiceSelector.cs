namespace Comqer.ReportServices;

public interface IReportServiceSelector {
    Task<IReportService> GetService();
}