using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}