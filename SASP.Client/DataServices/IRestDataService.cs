namespace SASP.Client.DataServices
{
    public interface IRestDataService<Entitie,Dto>
    {
        Task<List<Dto>> GetAllAsync();
        Task<Dto> GetAsync(int idItem);
        Task AddAsync(Entitie item);
        Task UpdateAsync(Entitie item);
        Task DeleteAsync(int id);
    }
}
