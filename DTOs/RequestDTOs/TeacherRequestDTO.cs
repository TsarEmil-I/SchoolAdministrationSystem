﻿using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrationSystem.DTOs.RequestDTOs
{
    public class TeacherRequestDTO : BaseRequestDTO
    {
        [Required]
        [Display(Name = "Име")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Името трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Презимето трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"^[А-Яа-яЁё-]+$", ErrorMessage = "Фамилията трябва да съдържа само букви на кирилица, без специални знаци и цифри.")]
        public string LastName { get; set; }
    }
}
