using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Cache
{
    public class RedisConfigurationManager
    {
        private readonly StackExchangeRedisCacheClient _redisCacheClient;
        private string _ipAddress;
        private int _port;
        public RedisConfigurationManager(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            var serializer = new NewtonsoftSerializer();
            _redisCacheClient = new StackExchangeRedisCacheClient(serializer, GetRedisConfiguration());
        }

        private RedisConfiguration GetRedisConfiguration()
        {
            var redisConfiguration = new RedisConfiguration()
            {
                AbortOnConnectFail = false,
                //KeyPrefix = "_my_key_prefix_",
                Hosts = new RedisHost[]
                {
                    new RedisHost()
                    {
                        Host =_ipAddress,
                        Port = _port
                    }
                },
                AllowAdmin = true,
                ConnectTimeout = 60000,
                Database = 0,
                //Ssl = true,
                //Password = "my_super_secret_password",
                ServerEnumerationStrategy = new ServerEnumerationStrategy()
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                }
            };

            return redisConfiguration;
        }

        public StackExchangeRedisCacheClient GetConnection()
        {
            return _redisCacheClient;
        }
    }
}
