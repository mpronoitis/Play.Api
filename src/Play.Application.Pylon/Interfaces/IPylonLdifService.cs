using Microsoft.AspNetCore.Mvc;

namespace Play.Application.Pylon.Interfaces;

public interface IPylonLdifService
{
    Task<FileContentResult> ExportContactsLdif();
}