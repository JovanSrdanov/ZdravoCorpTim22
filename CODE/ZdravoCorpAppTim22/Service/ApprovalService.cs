using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class ApprovalService : GenericService<ApprovalRepository, Approval>
    {
        private ApprovalService() : base(ApprovalRepository.Instance) { }
        public static ApprovalService Instance
        {
            get
            {
                return new ApprovalService();
            }
        }
        public bool isApproved(Approval approval)
        {
            return approval.IsApproved;
        }
        public bool isRejected(Medicine medicine)
        {
            return (medicine.MedicineData.Approval.Doctor != null && medicine.MedicineData.Approval.IsApproved == false);
        }
    }
}
