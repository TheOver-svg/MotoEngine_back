using MotoEngine_back.Models;

namespace MotoEngine_back.Services.Interfaces;

public interface IRuleService
{   
    Task<List<Rule>> GetAllAsync();
    Task CreateAsync(Rule rule);
    Task DeleteAsync(string id);
}