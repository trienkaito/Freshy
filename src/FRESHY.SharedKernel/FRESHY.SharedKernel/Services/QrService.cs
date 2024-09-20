using System.Drawing;
using System.Drawing.Imaging;
using FRESHY.SharedKernel.Interfaces;
using QRCoder;

namespace FRESHY.SharedKernel.Services;

public class QrService : IQrService
{
    public byte[] GenerateQrCode(Guid orderId)
    {
        QRCodeGenerator qrGenerator = new();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(orderId.ToString(), QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new(qrCodeData);
        Bitmap qrCodeImage = qrCode.GetGraphic(20);

        using MemoryStream stream = new();
        // Specify the PNG image encoder
#pragma warning disable CA1416 // Validate platform compatibility
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        ImageCodecInfo pngEncoder = GetEncoder(ImageFormat.Png);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
        EncoderParameters encoderParameters = new(1);
#pragma warning restore CA1416 // Validate platform compatibility
#pragma warning disable CA1416 // Validate platform compatibility
        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
#pragma warning restore CA1416 // Validate platform compatibility

        // Save the bitmap to the stream using the PNG encoder
#pragma warning disable CA1416 // Validate platform compatibility
#pragma warning disable CS8604 // Possible null reference argument.
        qrCodeImage.Save(stream, pngEncoder, encoderParameters);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CA1416 // Validate platform compatibility

        return stream.ToArray();
    }

    private static ImageCodecInfo? GetEncoder(ImageFormat format)
    {
#pragma warning disable CA1416 // Validate platform compatibility
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
#pragma warning restore CA1416 // Validate platform compatibility
        foreach (ImageCodecInfo codec in codecs)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
#pragma warning restore CA1416 // Validate platform compatibility
        }
        return null;
    }
}