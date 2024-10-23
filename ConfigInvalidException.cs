namespace QRCodeCreator
{
    public class ConfigInvalidException : Exception
    {
        public ConfigInvalidException(string filePath, string additionalMessage) : base($"JSON configuration is invalid for file: {filePath}")
        {
            AdditionalMessage = additionalMessage;
        }

		public string AdditionalMessage { get; }
	}
}