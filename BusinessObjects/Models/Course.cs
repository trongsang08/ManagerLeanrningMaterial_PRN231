using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; }

    public string CourseCode { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
