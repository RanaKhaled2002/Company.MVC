namespace Company.G03.PL.Services
{
    public interface ITransientService
    {
        public Guid ID { get; set; }

        public string GetGuid();
    }
}
