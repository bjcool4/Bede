namespace Bede.Logic.Interface
{
    public interface ISlotMachineService
    {
        double DepositAmount { get; }
        double Balance { get; }
        void Play();
    }
}
