using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Buffers;
using System.IO;

namespace AssetStudio
{
    public static class Texture2DExtensions
    {
        private static Configuration _configuration;

        static Texture2DExtensions()
        {
            _configuration = Configuration.Default.Clone();
            _configuration.PreferContiguousImageBuffers = true;
        }
        public static Image<Bgra32> ConvertToImage(this Texture2D m_Texture2D, bool flip, bool convertBC7Normal = false)
        {
            var converter = new Texture2DConverter(m_Texture2D);
            var buff = ArrayPool<byte>.Shared.Rent(m_Texture2D.m_Width * m_Texture2D.m_Height * 4);
            try
            {
                if (converter.DecodeTexture2D(buff))
                {
                    var image = Image.LoadPixelData<Bgra32>(_configuration, buff, m_Texture2D.m_Width, m_Texture2D.m_Height);
                    if (flip)
                    {
                        image.Mutate(x => x.Flip(FlipMode.Vertical));
                    }
                    if (convertBC7Normal)
                    {
                        image.Mutate(x => x.ProcessPixelRowsAsVector4((row) =>
                        {
                            foreach(ref var pixel in row)
                            {
                                float x = pixel.W * 2f - 1f;
                                float y = 1f - pixel.Y;
                                float z = MathF.Max(MathF.Sqrt(1f - MathF.Min(x * x + y * y, 1f)), 1.0e-16f);
                                var tmp = pixel.W;
                                pixel.W = pixel.X;
                                pixel.Y = 1f - pixel.Y;
                                pixel.Z = (z + 1f) / 2f;
                                pixel.X = tmp;
                            }
                        }));
                    }
                    return image;
                }
                return null;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buff, true);
            }
        }

        public static MemoryStream ConvertToStream(this Texture2D m_Texture2D, ImageFormat imageFormat, bool flip, bool isNormal)
        {
            bool needConvertNormal = isNormal && m_Texture2D.m_TextureFormat == TextureFormat.BC7;
            var image = ConvertToImage(m_Texture2D, flip, needConvertNormal);
            if (image != null)
            {
                using (image)
                {
                    return image.ConvertToStream(imageFormat);
                }
            }
            return null;
        }
    }
}
