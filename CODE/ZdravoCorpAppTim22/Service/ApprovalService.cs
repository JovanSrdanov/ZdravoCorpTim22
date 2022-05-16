using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class ApprovalService : GenericService<ApprovalRepository, Approval>
    {
        private static ApprovalService instance;
        private ApprovalService() : base(ApprovalRepository.Instance) { }
        public static ApprovalService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApprovalService();
                }

                return instance;
            }
        }
    }
}
