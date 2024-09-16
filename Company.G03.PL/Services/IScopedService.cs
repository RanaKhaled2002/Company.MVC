namespace Company.G03.PL.Services
{
    public interface IScopedService
    {
        public Guid ID { get; set; }

        string GetGuid();
    }
}
