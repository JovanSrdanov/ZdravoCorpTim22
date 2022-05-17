using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class ApprovalRepository : GenericRepository<Approval>
    {
        public static string FileName = "ApprovalData.json";
        private static ApprovalRepository instance;
        private ApprovalRepository() : base(FileName) { }
        public static ApprovalRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApprovalRepository();
                }

                return instance;
            }
        }
    }
}
