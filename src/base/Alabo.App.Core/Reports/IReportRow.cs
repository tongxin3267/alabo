namespace Alabo.App.Core.Reports {

    public interface IReportRow {

        bool HasColumn(string columnName);

        T GetData<T>(string columnName);

        bool TryGetData<T>(string columnName, out T value);
    }
}