using Whats_App_Web_Server.Models;

namespace Whats_App_Web_Server.Services
{
    public interface IRateService
    {
        public List<Rate> GetAll();

        public Rate Get(int id);

        public void Create(int rate, string description);

        public void Edit(int id, int rate, string description);

        public void Delete(int id);


    }
}
