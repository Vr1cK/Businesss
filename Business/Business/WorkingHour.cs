using System;
using System.Collections.Generic;

namespace Business;

public partial class WorkingHour
{
    public int Id { get; set; }

    public int WorkerId { get; set; }

    public DateTime StartOfWork { get; set; }

    public DateTime? EndOfWork { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
