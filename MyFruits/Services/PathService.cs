using MyFruits.Options;

namespace MyFruits.Services;

public class PathService
{
    private readonly IConfiguration configuration;
    private readonly IWebHostEnvironment env;

    public PathService(IConfiguration configuration, IWebHostEnvironment env)
    {
        this.configuration = configuration;
        this.env = env;
    }

    public string GetUploadsPath(string? filename = null, bool withWebRootPath = true)
    {
        var pathOptions = new PathOptions();

        configuration.GetSection(PathOptions.Path).Bind(pathOptions);

        var uploadsPath = pathOptions.FruitsImages;

        if (null != filename)
            uploadsPath = Path.Combine(uploadsPath, filename);

        return withWebRootPath ? Path.Combine(env.WebRootPath, uploadsPath) : uploadsPath;
    }
}
