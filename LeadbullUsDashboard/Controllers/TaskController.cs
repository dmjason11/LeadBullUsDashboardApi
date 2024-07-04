using Api.DTOS;
using Api.Errors;
using AutoMapper;

using Core.IRepos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.IO.Compression;
using System.Text;


namespace Api.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
      
        public TaskController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

       
        [HttpPost("addTaskDocument/{userId}")]
        public async Task<ActionResult> addTaskDocument(string userId,IFormFile file)
        {
            if (file.FileName != null)
            {
                var res = await writeFile(file);
                await _uow._userTaskService.AddUserTask(new Core.UserTask()
                {
                    UserId = userId,
                    DocumentUrl = res
                });
                await _uow.saveChanges();
                return Ok(res);
            }
            else
            {
                return BadRequest(new ApiResponse(400 , "File is not found"));
            }
        }

        private async Task<string> writeFile(IFormFile file)
        {
            string fileName = "";
            
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks.ToString() + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var exactPath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileName);
                using (var stream = new FileStream(exactPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            
            return fileName;
        }

        [HttpGet("getAllFiles/{userId}")]
        public async Task<ActionResult> getAllFiles(string userId)
        {
            var tasks = await _uow._userTaskService.getUserTasks(userId);
            var tasksDto = _mapper.Map<List<UserTaskDto>>(tasks);
            return Ok(tasksDto);
        }


        [HttpGet("DownloadCompressTasks/{userId}")]
        public async Task<ActionResult> DownloadCompressTasks(string userId)
        {
            var userTasks = await _uow._userTaskService.getUserTasks(userId);
            var zipName = $"archive-EvidenceFiles-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
            using (MemoryStream ms = new MemoryStream())
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var userTask in userTasks)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", userTask.DocumentUrl);
                        var provider = new FileExtensionContentTypeProvider();
                        if (!provider.TryGetContentType(path, out var contentType))
                        {
                            contentType = "application/octet-stream";
                        }

                        var file = await System.IO.File.ReadAllBytesAsync(path);
                        string fPath = Encoding.ASCII.GetString(file);
                        var entry = archive.CreateEntry(System.IO.Path.GetFileName(fPath), CompressionLevel.Fastest);
                        using (var zipStream = entry.Open())
                        {
                            var bytes = System.IO.File.ReadAllBytes(fPath);
                            zipStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
                return File(ms.ToArray(), "application/zip", zipName);
            }
        }

        [HttpGet("DownloadFile/{fileName}")]
        public async Task<ActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files",fileName);
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes,contentType , Path.GetFileName(filePath));    
        }
    }
}
