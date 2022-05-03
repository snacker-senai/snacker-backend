using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
