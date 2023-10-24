using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Fullname { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual Role Role { get; set; }

    public virtual ICollection<SubmitAssignment> SubmitAssignments { get; set; } = new List<SubmitAssignment>();

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
