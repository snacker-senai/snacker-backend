using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class ThemeService : BaseService<Theme>, IThemeService
    {
        private readonly IThemeRepository _themeRepository;
        public ThemeService(IThemeRepository themeRepository) : base(themeRepository)
        {
            _themeRepository = themeRepository;
        }

        public ICollection<object> GetFromRestaurant(long restaurantId)
        {
            var itens = _themeRepository.SelectFromRestaurant(restaurantId);
            var result = new List<object>();
            foreach (var item in itens)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
