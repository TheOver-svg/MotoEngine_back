namespace MotoEngine_back.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string SymptomsCollectionName { get; set; } = null!;
}