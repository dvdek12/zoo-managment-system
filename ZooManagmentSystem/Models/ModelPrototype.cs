using System.ComponentModel.DataAnnotations;

namespace ZooManagmentSystem.Models
{
    public abstract class ModelPrototype
    {
        [Key]
        public int id {  get; set; }
    }
}
