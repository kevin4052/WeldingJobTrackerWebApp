﻿using System.ComponentModel.DataAnnotations;

namespace WeldingJobTrackerWebApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street1 { get; set; }
        public string? Street2 { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; } = "United States";
    }
}
