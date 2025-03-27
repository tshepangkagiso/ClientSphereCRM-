using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace CRM_EMPLOYEE_APP.Services
{
    public class ImageServices
    {
        private const int resizeWidth = 300; //300px
        public static async Task<byte[]> ProcessImage(Stream content)
        {
            try
            {
                using var image = Image.Load(content);
                var width = image.Width;
                var height = image.Height;

                if (width >= resizeWidth)
                {
                    height = Convert.ToInt32(resizeWidth / width * height);
                    width = resizeWidth;
                }

                image.Metadata.ExifProfile = null;
                image.Mutate(i => i.Resize(width, height));

                await using var memoryStream = new MemoryStream();
                await image.SaveAsJpegAsync(memoryStream, new JpegEncoder { Quality = 70 });
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Array.Empty<byte>();
            }
        }
    }
}
