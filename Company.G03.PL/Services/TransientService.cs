namespace Company.G03.PL.Services
{
    public class TransientService : ITransientService
    {
        public Guid ID { get; set; }

        public TransientService()
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
