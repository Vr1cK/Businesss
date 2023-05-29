using System;
using System.Collections.Generic;

namespace Business;

public partial class Worker
{
    public int Id { get; set; }

    public int? Age { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public int? PostId { get; set; }

    public virtual Post? Post { get; set; }

    public virtual ICollection<WorkingHour> WorkingHours { get; set; } = new List<WorkingHour>();

    public override string ToString()
    {
        return $"Полное имя: {FullName}. Телефон: {Phone}. Должность: {Post?.PostName}";
    }
}
