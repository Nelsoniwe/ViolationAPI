﻿namespace BLL.Models;

public class VideoDTO
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public byte[] Hash { get; set; }
}