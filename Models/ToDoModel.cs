using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ROITest.Models
{
    /// <summary>
    /// Poco model
    /// </summary>
    public class ToDoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Done { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Due date: {DueDate}, Done: {Done}";
        }
    }
}