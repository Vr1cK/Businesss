using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class WorkerList
    {
        public int Id { get; set; }

        public int? Age { get; set; }

        public string FullName { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int? PostId { get; set; }

        public virtual Post? Post { get; set; }

        public string? PostName { get; set; }
    }
}
