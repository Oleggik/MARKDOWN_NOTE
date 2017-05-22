using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownNotes.DataAccess
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsHidden { get; set; }
        public bool SharedToEveryone { get; set; }
        public int OwnerId { get; set; }

    }
}
