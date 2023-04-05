using Car_App.Data.Models;

namespace Car_App.Service
{
    public class CarSorting
    {
        public IEnumerable<Car> SortByYearAscending(IEnumerable<Car> cars)
        {
            return cars.OrderBy(c => c.Year);
        }

        public IEnumerable<Car> SortByYearDescending(IEnumerable<Car> cars)
        {
            return cars.OrderByDescending(c => c.Year);
        }
    }
}
