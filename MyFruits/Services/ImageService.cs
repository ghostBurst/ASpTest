using MyFruits.Models;

namespace MyFruits.Services;

public class ImageService
{
    private readonly PathService pathService;

    public ImageService(PathService pathService)
    {
        this.pathService = pathService;
    }

    public async Task<Image> UploadAsync(Image image)
    {
        var uploadsPath = pathService.GetUploadsPath();

        var imageFile = image.File;
        var imageFileName = GetRandomFileName(imageFile.FileName);
        var imageUploadPath = Path.Combine(uploadsPath, imageFileName);

        using (var fs = new FileStream(imageUploadPath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fs);
        }

        image.Name = imageFile.FileName;
        image.Path = pathService.GetUploadsPath(imageFileName, withWebRootPath: false);

        return image;
    }

    public void DeleteUploadedFile(Image? image)
    {
        if (image == null)
            return;

        var imagePath = pathService.GetUploadsPath(Path.GetFileName(image.Path));

        if (File.Exists(imagePath))
            File.Delete(imagePath);
    }

    private string GetRandomFileName(string filename)
    {
        return Guid.NewGuid() + Path.GetExtension(filename);
    }
}