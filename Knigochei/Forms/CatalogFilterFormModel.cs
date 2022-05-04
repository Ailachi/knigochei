namespace Knigochei.Forms
{
    public class CatalogFilterFormModel
    {
        public int PriceMin { get; set; } = 1000;
        public int PriceMax { get; set; } = 10000;
        public int GenreId { get; set; } = 0;
        public bool OrderByPriceDesc { get; set; } = false;
    }
}
