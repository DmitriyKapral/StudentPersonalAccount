using Newtonsoft.Json;

namespace StudentPersonalAccount.Views
{
    public class EvaliationView
    {
        public Guid Guid { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        [JsonIgnore]
        public Guid StudentId { get; set; }
    }
}
