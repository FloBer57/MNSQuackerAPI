using QuackerMNS.Model;

namespace QuackerMNS.Infrastructure.Interface
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
    }
}
