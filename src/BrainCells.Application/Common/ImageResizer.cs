using System;
using Microsoft.AspNetCore.Http;
using SkiaSharp;

namespace BrainCells.Application.Common;

public static class ImageResizer
{
    public static async Task<MemoryStream> ResizeAsync(IFormFile image)
    {
        MemoryStream stream = new();
        await image.CopyToAsync(stream);
        MemoryStream resultStream = new();
        using(var skBitmap = SKBitmap.Decode(stream.ToArray()))
        {
            var nskBitmap = skBitmap.Resize(new SKImageInfo(500, 500), SKFilterQuality.High);
            var skData = nskBitmap.Encode(SKEncodedImageFormat.Png, 100);
            skData.SaveTo(resultStream);
        }
        stream.Close();
        resultStream.Close();
        return resultStream;
    }
}
