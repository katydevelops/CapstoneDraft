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

        public string AddPostErrorMessage { get; private set; }

        public UploadPhotoService()
        {
        }

        // Needed to create some kind of "random" filename for the photos becuase collisions could happen if the same filename and extension were updated. The use of new guid was a simple way to create a "random enough" id file name to prevent duplicates from being uploaded for the use of this project.
        public string CreateRandomFileName(string fileExtentsion) => $"{Guid.NewGuid()}{fileExtentsion}";

        public string CreateUploadedPhotoDirectory(string rootFile, string uploadedPhotoDirectory)
        {
            // Need to check if the upload folder exists, if not, one needs to be created so that all the files uploaded by the user can be saved there.
            var createUploadFolder = Path.Combine(rootFile, uploadedPhotoDirectory);
            if (!Directory.Exists(createUploadFolder))
            {
                Directory.CreateDirectory(createUploadFolder);
            }
            return createUploadFolder;
        }

        // Use SixLabors Compression Library to set the image encoder type to only allow jpg/jpeg and png images to prevent the user from uploading file formats that aren't relevant to the purpose of Safety Net.
        public IImageEncoder SetPhotoType(string photoType) => photoType.ToLower() switch
        {
            ".jpg" or ".jpeg" => new JpegEncoder { Quality = 50 },
            ".png" => new PngEncoder(),
        };

        public async Task<string> ProcessSelectedPhotoAsync(IBrowserFile photoFile)
        {
            // Create folder at the wwwroot directory for added photos to be upladed to
            var fileDirectory = CreateUploadedPhotoDirectory("wwwroot", "Uploaded_Photos");
            const long fileSizeLimit = 3 * 1024 * 1024; // Needed to make a long datatype for OpenReadSteam to accept
            var photoFileExtension = Path.GetExtension(photoFile.Name); // Set the extension of the uploaded file
            var randomFileName = CreateRandomFileName(photoFileExtension); // Generate the random guid-based file name to prevent collisions
            var photoFilePath = Path.Combine(fileDirectory, randomFileName); // Set the photo name to randomlyGeneratedGuid.fileExtension

            try
            {
                // Check to make sure the photo isn't larger than the allowed limit after compression
                if (photoFile.Size > fileSizeLimit)
                {
                    AddPostErrorMessage = "Your selected photo exceeds the maximum file size limit! Please try a different photo!";
                }
                // Use memory stream to compress the image in memory before storing the compressed image to the database
                await using (var uploadFileSteam = photoFile.OpenReadStream(fileSizeLimit))
                {
                    // Compress the image in the memory stream
                    await CompressImageSizeAsync(uploadFileSteam, photoFilePath, photoFileExtension);
                }
                // Then once the compression is completed, return the path of the compressed image using System.IO built-in GetRelativePath method
                return Path.GetRelativePath("wwwroot", photoFilePath);
            }
            catch (Exception error)
            {
                throw new ArgumentException("There was an error processing your image - please try a different photo!", error);
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
