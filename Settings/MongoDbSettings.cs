namespace MotoDiagnostics.Api.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string SymptomsCollectionName { get; set; } = null!;
}