using System;
using System.Text.Json.Serialization;

namespace PowerBranchBack.Model
{
    public class EventUser
    {
        public int Id { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public Event Event { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
