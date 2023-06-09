﻿namespace BLL.Models;

public class ApplicationStatusDTO
{
    public int Id { get; set; }

    public string Status { get; set; }

    public virtual ICollection<int> ApplicationIds { get; set; }
}