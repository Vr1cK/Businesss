using System;
using System.Collections.Generic;

namespace Business;

public partial class Post
{
    public int Id { get; set; }

    public string PostName { get; set; } = null!;

    public decimal RatePerHour { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
