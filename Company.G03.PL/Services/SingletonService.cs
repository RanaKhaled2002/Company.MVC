namespace Company.G03.PL.Services
{
    public class SingletonService : ISingletonService
    {
        public Guid ID { get; set; }

        public SingletonService()
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
