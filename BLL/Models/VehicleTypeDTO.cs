﻿namespace BLL.Models;

public class VehicleTypeDTO
{
    public int Id { get; set; }

    public string Type { get; set; }

    public virtual ICollection<int> ApplicationIds { get; set; }
}