﻿using CloudinaryDotNet.Actions;

namespace WeldingJobTrackerWebApp.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletPhotoAsync(string publicId);
    }
}
