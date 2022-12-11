using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using XYZEngineeringProject.Application.Interfaces;
using XYZEngineeringProject.Application.ViewModels.File;

namespace XYZEngineeringProject.Web.Controllers;

[Authorize]
public class FileController : Controller
{
    private readonly ILogger<FileController> _logger;
    private readonly IFileService _fileService;
    public FileController(ILogger<FileController> logger, IFileService fileService)
    {
        _logger = logger;
        _fileService = fileService;
    }

    [HttpPost, DisableRequestSizeLimit]
    public IActionResult Upload()
    {
        var file = Request.Form.Files[0];
        var folderName = Path.Combine("Resources", "Files");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (file.Length > 0)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok(new { dbPath });
        }
        else
        {
            return BadRequest();
        }
    }
    [HttpPost, DisableRequestSizeLimit]
    public IActionResult UploadFile([FromBody] FileVM file)
    {
        return Ok(_fileService.SaveFile(file));
    }

    [HttpGet, DisableRequestSizeLimit]
    public IActionResult GetTreeData()
    {
        return Ok(_fileService.GetStructure());
    }

    [HttpGet]
    public IActionResult CreateDefaultDirectory()
    {
        _fileService._AddDepartmentDirectory(Guid.Parse("85FB6738-7287-4F11-A9B1-C6867A14DDE2"));
        return Ok();
    }

    [HttpGet]
    [Route("/File/CreateDirectory/{parentId}/{name}")]
    public IActionResult CreateDirectory(string parentId, string name)
    {
        _fileService.CreateDirectory(Guid.Parse(parentId), name);
        return Ok();
    }
    [HttpGet]
    [Route("File/EditFileName/{id}/{name}")]
    public IActionResult EditFileName(string id, string name)
    {
        _fileService.ChangeFileName(Guid.Parse(id), name);
        return Ok();
    }
    [HttpGet]
    [Route("File/EditDirectoryName/{id}/{name}")]
    public IActionResult EditDirectoryName(string id, string name)
    {
        _fileService.ChangeDirectoryName(Guid.Parse(id), name);
        return Ok();

    }

    [HttpGet]
    [Route("File/DeleteFile/{id}")]
    public IActionResult DeleteFile(string id)
    {
        _fileService.DeleteFile(Guid.Parse(id));
        return Ok();
    }

    [HttpGet]
    [Route("File/DeleteDirectory/{id}")]
    public IActionResult DeleteDirectory(string id)
    {
        _fileService.DeleteDirectory(Guid.Parse(id));
        return Ok();
    }
}
