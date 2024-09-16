
using System.Text;

namespace Company.G03.PL.Services
{
    public class ScopedService : IScopedService
    {
        public Guid ID { get; set; }

        public ScopedService()
        {
            ID = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return ID.ToString(); 
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
