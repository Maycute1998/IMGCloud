using Newtonsoft.Json;
using IMGCloud.Utilities.RedisConfig;

namespace IMGCloud.Infrastructure.Services;

public sealed class CacheService : ICacheService
{
    private readonly StackExchange.Redis.IDatabase _db;
    public CacheService()
    {
        _db = ConnectionHelper.Connection.GetDatabase();
    }

    public T? GetData<T>(string key)
    {
        var value = _db.StringGet(key);
        if (!string.IsNullOrWhiteSpace(value))
        {
            return JsonConvert.DeserializeObject<T>(value!);
        }
        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        return isSet;
    }

    public object RemoveData(string key)
    {
        bool _isKeyExist = _db.KeyExists(key);
        if (_isKeyExist)
        {
            return _db.KeyDelete(key);
        }
        return false;
    }
}
