using System;
using System.Collections.Generic;

namespace EfCore.Data
{
    public partial class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SamuraiId { get; set; }

        public virtual Samurai Samurai { get; set; }
    }
}
