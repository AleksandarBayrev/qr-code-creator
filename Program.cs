using System.Text.Json;
using QRCodeCreator;
using QRCoder;

var config = JsonSerializer.Deserialize<AppConfig>(await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "config.json")));

using (QRCodeGenerator generator = new QRCodeGenerator())
using (QRCodeData data = generator.CreateQrCode(config.Link, QRCodeGenerator.ECCLevel.Q))
using (PngByteQRCode code = new PngByteQRCode(data))
{
    byte[] generatedQrCode = code.GetGraphic(20);
    await File.WriteAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), "mycode.png"), generatedQrCode);
}