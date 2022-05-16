using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels
{
    public class MedicineDetailsViewModel
    {
        public Medicine Medicine { get; private set; }
        public MedicineDetailsViewModel(Medicine medicine)
        {
            Medicine = medicine;
        }
    }
}
