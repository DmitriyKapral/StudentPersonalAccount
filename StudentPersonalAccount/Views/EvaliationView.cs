using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using StudentPersonalAccount.Models;

namespace StudentPersonalAccount.Views
{
    public class EvaliationView
    {
        public Guid Guid { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        [JsonIgnore]
        public Guid StudentId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            EvaliationView other = (EvaliationView)obj;

            return Guid == other.Guid &&
                   Quantity == other.Quantity &&
                   DateTime == other.DateTime &&
                   StudentId == other.StudentId;
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode() ^ Quantity.GetHashCode() ^ DateTime.GetHashCode() ^ StudentId.GetHashCode();
        }
    }
}
