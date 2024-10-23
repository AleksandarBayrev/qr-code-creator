namespace QRCodeCreator
{
    public class ConfigInvalidException : Exception
    {
        public ConfigInvalidException(string filePath) : base($"JSON configuration is invalid for file: {filePath}") {}
    }
}