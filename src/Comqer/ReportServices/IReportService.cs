namespace Comqer.ReportServices;

public interface IReportService {
    Task<string> Command(string input);
    Task<string> Query(string input);
}