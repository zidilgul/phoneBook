//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace phoneBook.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class peopleInDirectory
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Bu alan gerekli!")]
        [Display(Name = "Ad Soyad")]
        public string fullName { get; set; }

        [Required(ErrorMessage = "Bu alan gerekli!")]
        [Display(Name = "Telefon Numaras?")]
        public string phone { get; set; }
        public int userId { get; set; }
    
        public virtual users users { get; set; }
    }
}
