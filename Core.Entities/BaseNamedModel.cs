using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BaseNamedModel
    {
        [Required, DataType(DataType.Text)]
        public virtual string Name { get; set; }
    }
}