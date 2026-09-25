using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MotoEngine_back.Models;
using MotoEngine_back.Services.Interfaces;
using MotoEngine_back.Settings;

namespace MotoEngine_back.Services;

public class RuleService : IRuleService
{
    private readonly IMongoCollection<Rule> _rules;

    public RuleService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _rules = database.GetCollection<Rule>(settings.Value.RulesCollectionName);
    }

    public async Task<List<Rule>> GetAllAsync()
    {
        return await _rules.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(Rule rule)
    {
        await _rules.InsertOneAsync(rule);
    }

    public async Task DeleteAsync(string id)
    {
        await _rules.DeleteOneAsync(id);
    }
}