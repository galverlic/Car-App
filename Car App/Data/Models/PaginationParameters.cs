namespace Car_App.Data.Models
{
    public class PaginationParameters
    {
        private int _page = 1;
        public int Page
        {
            get => _page;
            set => _page = Math.Max(1, value);
        }

        public int PageSize { get; set; } = 10;
    }

}
