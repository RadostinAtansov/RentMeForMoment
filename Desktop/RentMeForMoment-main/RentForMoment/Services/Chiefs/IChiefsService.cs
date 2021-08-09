namespace RentForMoment.Services.Chiefs
{
    public interface IChiefsService
    {
        public bool IsChief(string userId);

        public int GetIdByUser(string userId);
    }
}
