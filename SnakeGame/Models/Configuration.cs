using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace SnakeGame.Models
{
    public class Configuration
    {
        public Configuration()
        {
            var fileName = @"configuration.json";
            var json = File.ReadAllText(fileName);
            dynamic config = JsonConvert.DeserializeObject(json);
            PixabyConfigKey = config.PixabyConfigKey;
        }

        public string PixabyConfigKey { get; }
    }
}
