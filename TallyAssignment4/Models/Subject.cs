using System;
using System.Collections.Generic;

namespace TallyAssignment4.Models;

public partial class Subject
{
    public int SubId { get; set; }

    public int? StudentId { get; set; }

    public string? SubjectName { get; set; }

    public int? MaxMarks { get; set; }

    public int? ObtMarks { get; set; }

    public virtual Student? Student { get; set; }
}
