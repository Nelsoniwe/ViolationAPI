﻿namespace API.Models;

public class VideoModel
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public byte[] data { get; set; }
}