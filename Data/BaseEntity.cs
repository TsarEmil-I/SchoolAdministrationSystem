﻿using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
