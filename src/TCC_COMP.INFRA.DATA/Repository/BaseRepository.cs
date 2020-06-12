namespace TCC_COMP.INFRA.DATA.Repository
{

    using Microsoft.Extensions.Configuration;

    public abstract class BaseRepository<T>
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected string ConnectionString => _connectionString;
    }
}
