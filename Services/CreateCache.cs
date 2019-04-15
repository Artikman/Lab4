using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_4.Models;
using Microsoft.Extensions.Caching.Memory;
using Lab_4.Data;
using Lab_4.ViewModels;

namespace Lab_4.Services
{
    public class CreateCache
    {
        private IMemoryCache cache;

        public CreateCache(CinemaContext context, IMemoryCache memoryCache)
        {
            cache = memoryCache;
        }

        public HomeViewModel GetProduct(string key)
        {
            HomeViewModel homeViewModel = null;

            if (!cache.TryGetValue(key, out homeViewModel))
            {
                homeViewModel = TakeLast.GetHomeViewModel();
                if (homeViewModel != null)
                {
                    cache.Set(key, homeViewModel,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds((2 * 8) + 240)));
                }
            }
            return homeViewModel;
        }
    }
}