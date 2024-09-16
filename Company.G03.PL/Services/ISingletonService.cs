namespace Company.G03.PL.Services
{
    public interface ISingletonService
    {
        public Guid ID { get; set; }

        public string GetGuid();
    }
}
