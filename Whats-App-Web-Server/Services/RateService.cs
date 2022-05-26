using Whats_App_Web_Server.Models;

namespace Whats_App_Web_Server.Services
{
    public class RateService : IRateService
    {
        private static List<Rate> rates = new List<Rate>();

        public List<Rate> GetAll()
        {
            return rates;
        }

        public List<Rate> getSearch(int query)
        {
            List<Rate> returnList = new List<Rate>();
            if (query >= 0 && query <= 5)
            {
                Rate rate = rates.Find(x => x.givenRate == query);
                if (rate != null)
                {
                    returnList.Add(rate);
                }
                return returnList;
                
            }
            else
            {
                return rates;
            }
        }

        public Rate Get(int id)
        {
            return rates.Find(x => x.Id == id);
        }

        public void Edit(int id, int rate, string description)
        {
            Rate rate1 = Get(id);
            rate1.Description = description;
            rate1.givenRate = rate;
        }

        public void Create(int rate, string description)
        {
            int nextId = 0;
            if (rates.Count > 0)
            {
                nextId = rates.Max(x => x.Id) + 1;

            }
            rates.Add(new Rate() { Id = nextId, Description = description, givenRate = rate });
        }

        public void Delete(int id)
        {
            rates.Remove(Get(id));
        }

    }
}
