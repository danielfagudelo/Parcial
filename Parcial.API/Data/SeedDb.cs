using Parcial.API.Data;
using Parcial.Shared.Entities;

namespace Parcial.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckAccessControlerAsync();
        }

        private async Task CheckAccessControlerAsync()
        {
            if (!_context.Boletas.Any())
            {
                int numOfRowsToInsert = 50000;

                for (int i = 1; i <= numOfRowsToInsert; i++)
                {
                    _context.Boletas.Add(new Boleta
                    {
                        FechaUso = null,
                        FueUsada = false,
                        Porteria = null
                    });
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
