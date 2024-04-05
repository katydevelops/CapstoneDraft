using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats;

namespace CapstoneDraft.Services
{
    public class UploadPhotoService
    {

        private readonly ILogger<UploadPhotoService> _logger;

        public string AddPostErrorMessage { get; private set; }

        public UploadPhotoService(ILogger<UploadPhotoService> logger)
        {
            _logger = logger;
        }

        // Needed to create some kind of "random" filename for the photos becuase collisions could happen if the same filename and extension were updated. The use of new guid was a simple way to create a "random enough" id file name to prevent duplicates from being uploaded for the use of this project.
        public string CreateRandomFileName(string fileExtentsion) => $"{Guid.NewGuid()}{fileExtentsion}";

        public string CreateUploadedPhotoDirectory(string rootFile, string uploadedPhotoDirectory)
        {
            var createUploadFolder = Path.Combine(rootFile, uploadedPhotoDirectory);
            if (!Directory.Exists(createUploadFolder))
            {
                Directory.CreateDirectory(createUploadFolder);
            }
            return createUploadFolder;
        }


        public IImageEncoder SetPhotoType(string photoType) => photoType.ToLower() switch
        {
            ".jpg" or ".jpeg" => new JpegEncoder { Quality = 50 },
            ".png" => new PngEncoder(),
        };

        public async Task<string> ProcessSelectedPhotoAsync(IBrowserFile photoFile)
        {
            // Create folder for added photos to be upladed to in wwwroot
            var fileDirectory = CreateUploadedPhotoDirectory("wwwroot", "Uploaded_Photos");
            const long fileSizeLimit = 3 * 1024 * 1024; // Needed to make a long for OpenReadSteam to accept
            var photoFileExtension = Path.GetExtension(photoFile.Name);
            var randomFileName = CreateRandomFileName(photoFileExtension);
            var photoFilePath = Path.Combine(fileDirectory, randomFileName);

            try
            {
                // If the Uploaded Photos folder doesn't yet exist, create ie
                if (photoFile.Size > fileSizeLimit)
                {
                    AddPostErrorMessage = "Your selected photo exceeds the maximum file size limit! Please try a different photo!";
                }
                // Use memory stream to compress the image in memory before storing the compressed image to the database
                await using (var uploadFileSteam = photoFile.OpenReadStream(fileSizeLimit))
                {
                    await CompressImageSizeAsync(uploadFileSteam, photoFilePath, photoFileExtension);
                    _logger.LogInformation($"Processing file: {photoFile.Name}, Size: {photoFile.Size}, Directory: {fileDirectory}");

                }
                _logger.LogInformation($"Photo processed and saved at: {photoFilePath}");
                return Path.GetRelativePath("wwwroot", photoFilePath);
            }
            catch (Exception error)
            {
                _logger.LogInformation(error, "You are receiving an error with the iamge you are trying to upload - please try agian");
                throw;
            }
        }

        public async Task CompressImageSizeAsync(Stream uploadFileStream, string photoFilePath, string photoFileExtension)
        {
            using var compressPhotoStream = new MemoryStream();
            await uploadFileStream.CopyToAsync(compressPhotoStream);
            compressPhotoStream.Position = 0;
            using (var photo = Image.Load(compressPhotoStream))
            {
                photo.Mutate(image => image.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new Size(500),
                }));
                IImageEncoder photoType = SetPhotoType(photoFileExtension);
                await photo.SaveAsync(photoFilePath, photoType);
            }
        }

    }
}
