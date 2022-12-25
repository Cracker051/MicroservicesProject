using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NLayerApp.DAL.Entities
{
    public class Departments
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public ICollection<Doctors> Doctors{ get; set; } = new List<Doctors>();
    }
}
