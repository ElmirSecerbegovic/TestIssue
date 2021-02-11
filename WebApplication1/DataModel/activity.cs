using System;

namespace WebApplication1.DataModel
{
    public partial class activity
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Guid uid { get; set; }
    }
}
