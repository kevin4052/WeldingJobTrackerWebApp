﻿using System.ComponentModel.DataAnnotations;
using WeldingJobTrackerWebApp.Data.Enum;

namespace WeldingJobTrackerWebApp.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}