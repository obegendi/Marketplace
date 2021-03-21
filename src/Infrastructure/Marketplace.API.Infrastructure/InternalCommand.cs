using System;

namespace Marketplace.API.Infrastructure
{
    public class InternalCommand
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
        public string Data { get; set; }
        public DateTime? ProcessedData { get; set; }
    }
}
