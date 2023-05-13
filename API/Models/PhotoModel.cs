﻿namespace API.Models;

public class PhotoModel
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public byte[] Hash { get; set; }
}