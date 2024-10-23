using System.Text.Json;
using QRCodeCreator;
using QRCoder;

namespace QRCodeCreator
{
	internal class Program
	{
		internal static async Task Main(string[] args)
		{
			AppConfig config;
			try
			{
				config = await ParseConfig(configFilePath);
				var checks = new List<bool>
				{
					!string.IsNullOrEmpty(config.Link),
					Uri.IsWellFormedUriString(config.Link, UriKind.Absolute)
				};
				if (checks.Any(x => !x))
				{
					await Console.Out.WriteLineAsync("Please update config.json with a valid link!");
					return;
				}
				await Console.Out.WriteLineAsync($"Generating QR code for link: {config.Link}");
				using (QRCodeGenerator generator = new QRCodeGenerator())
				using (QRCodeData data = generator.CreateQrCode(config.Link, QRCodeGenerator.ECCLevel.Q))
				using (PngByteQRCode code = new PngByteQRCode(data))
				{
					byte[] generatedQrCode = code.GetGraphic(20);
					await File.WriteAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), $"qr-code-{DateTime.Now.ToString("yyyy_MM_dd_HH:mm:sss_fff")}.png"), generatedQrCode);
				}
			}
			catch (ConfigInvalidException ex)
			{
				await Console.Out.WriteLineAsync(ex.Message);

			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync($"A problem has occured, message: {ex.Message}");
			}
		}

		internal static async Task<AppConfig> ParseConfig(string path)
		{
			try
			{
				return JsonSerializer.Deserialize<AppConfig>(await File.ReadAllTextAsync(path));

			}
			catch (System.Text.Json.JsonException ex)
			{
				throw new ConfigInvalidException(path);
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static readonly string configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");
	}
}