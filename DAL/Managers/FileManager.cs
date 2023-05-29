namespace DAL.Managers;

public class FileManager
{
    public async Task<byte[]> ReadFileAsync(string path)
    {
        return await File.ReadAllBytesAsync(path);
    }

    public async Task<string> WriteFileAsync(string name, byte[] fileData)
    {
        string path = Path.Combine(Path.GetTempPath(), name);
        await File.WriteAllBytesAsync(path, fileData);

        return path;
    }
}