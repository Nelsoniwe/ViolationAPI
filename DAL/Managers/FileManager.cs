using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace DAL.Managers;

public class FileManager
{
    private static string connectionString =
        "{\r\n  \"type\": \"service_account\",\r\n  \"project_id\": \"black-anagram-388320\",\r\n  \"private_key_id\": \"02c0dd02ded5071d196193993a4a58238d4abc98\",\r\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC9fEZk3jtXOW7m\\nACzIQDZuUakbZ0iA3W/AzxByod2SoMWRqhKqdpe0WBesrmNPjYMhfiIwFrFnItzm\\nTwa6SuGweHN5doRHzf0XrQcZ7/vb/fa51KZ/1ct4Ln9wyQb5pDOxaC+Jhy5iU+7V\\nOAheME7+oik5TSJlBE1ASwOS62dWjxVma5L+AhAXFA9Y8sWMorkfJ7/A+kn3OH10\\nUORS2cEnB2u1CYs1EmKDqh8Ua7C7K9s5XhOp6/7mZujEmByVe7h60Zhx4uShypB1\\nXrfbdru1GS89Wa44P//vnh3RPwM01hx8g76YGo0EohzAnh7Vv9/b9tW1Ya2zzi6H\\nPooavImFAgMBAAECggEAAdu96f/W96ZHzOgsCEO+9OqGnM7wwljJn4u4VtN5E5bR\\nkTnoiHT0zG8/vjEQpsdVxaaRycRzNXZ9UaHtSDbzSzKHOWX86qJdzNVk2C+HGhN9\\nz7lC78aGQhW8qLvD/n9KhX8m1jO18/9sPHmZ9WiGYc9v1ZDI2VJHOq3xqUd3Qj1N\\nL7KlOZYEdqM1vYGb6hZGLAf3iAaefd/qtV+1yO0dqX+BC8KLzoW/D7e40pn80j1u\\nuS0MGAVTE6a9STcWjtdUc5rC0zMdlKSdKFVkXCrawzFnXeKEhV5dCZrsI3N4yqbp\\nlkethNAXTln1/cRmLQrZaA4cqm/NBnJFu7PijWd4CQKBgQDdjAROcRyFagJQIUm8\\nx9hsNJqF1qForkE+YA+JiMier4segU51UAqeNCilcyk+6Ppaf3kQyZw+kUd9oqXm\\n6cPm3qD7E1+v3C+ttv7wnf9QsWg010DwVYWlitZEW/4V/Z3DXifRBx8fnIpiuCBT\\nTQU+Jl+2ZjTBNGNcZ2/t61wCvQKBgQDa89x+tTn1jgiJEEosb+Eyzmq/18RPtogX\\nO+rc4DYj6jnekmMjAsgB3EmZXTpPekUImVOHR7MvXXXfQHchFmeL74vzUMuVQP87\\n+zFmbqtSsyu6D1DWn/YIwcwItKMbkSbtZ4npMFp4rmJE8e6iCSrFMfLXACDkCQHM\\n21qbRfiyaQKBgQCDZIsZSpQzu89rXpKw14VPh1MtlUFdKBuhtq8ATpNNKadEjEPP\\n54ykjyKcHVSU7u+rxHY0sp1rrhXMOS5TaMi6n8axoafaUKgmSqWgZtQJM24uCIuG\\n/fGpYFH7cOOcfzGVsL3Ehoi64shgC/t/6+n0FiYlszAOddeH8gC8Bh4nsQKBgQCI\\nc8/nm3PKh8DkAqXnObZkHmZCtupeL0hzCmFdU9IJ4fA8uIhWCmaxWiL4FDTB0VZy\\nfHOk7FdMXneWMavMkBTqDdjvQ1wM4VvveqWXy8KzoHvQ0bi0eS4w2O4aQtEf3f8f\\nlxZIoFzRtwQBKbFphEMGcBGCUk8tLjvT+qRl/l52eQKBgEYejpt9WCfcucpcAWCF\\nvszzuBMTxs+M1zeXZlFg6vk/foWKJV6Q8lydvcK5xLW0W8gGXION3pGgKX3ehTnV\\nKHcembkdxdIZT33qE8h/COs6WNganOC9uoEKIP1PEmu5JSsUSRZ85XHqF6hFfCRk\\ncHFKrilQjgHPFg0+Z4tQnDro\\n-----END PRIVATE KEY-----\\n\",\r\n  \"client_email\": \"violationapi@black-anagram-388320.iam.gserviceaccount.com\",\r\n  \"client_id\": \"101786721339724714649\",\r\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\r\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\r\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\r\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/violationapi%40black-anagram-388320.iam.gserviceaccount.com\",\r\n  \"universe_domain\": \"googleapis.com\"\r\n}\r\n";
    
    public async Task<byte[]> ReadFileAsync(string path)
    {
        var credential = GoogleCredential.FromJson(connectionString);
        StorageClient storageClient = await StorageClient.CreateAsync(credential);
        using (MemoryStream stream = new MemoryStream())
        {
            await storageClient.DownloadObjectAsync("violationapplicationbucket", path, stream);
            return stream.ToArray();
        }
    }

    public async Task<string> WriteFileAsync(string name, byte[] fileData)
    {
        GoogleCredential credential = GoogleCredential.FromJson(connectionString);

        StorageClient storageClient = await StorageClient.CreateAsync(credential);
        using (MemoryStream stream = new MemoryStream(fileData))
        {
            await storageClient.UploadObjectAsync("violationapplicationbucket", name, null, stream);
        }

        return name;
    }
}