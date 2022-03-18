namespace ShortLink.Persistence
{
    public class UnitOfWork :
        IcFramework.Persistence.UnitOfWork<DatabaseContext>, IUnitOfWork
    {
        public UnitOfWork
            (IcFramework.Persistence.Options options) : base(options: options)
        {
        }

        // **************************************************
        private Logs.Repositories.ILogRepository _logs;

        public Logs.Repositories.ILogRepository Logs
        {
            get
            {
                if (_logs == null)
                {
                    _logs =
                        new Logs.Repositories.LogRepository(databaseContext: DatabaseContext);
                }

                return _logs;
            }
        }
        // **************************************************
    }
}
