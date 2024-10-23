using System.Text.Json;
using QRCodeCreator;
using QRCoder;

var config = JsonSerializer.Deserialize<AppConfig>(await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "config.json")));
var checks = new List<bool>
{
	!String.IsNullOrEmpty(config.Link),
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
	await File.WriteAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), "mycode.png"), generatedQrCode);
}