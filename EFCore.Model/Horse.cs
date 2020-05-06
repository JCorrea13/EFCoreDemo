using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Model
{
    public class Horse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public int SamuraiId { get; set; }
    }
}
