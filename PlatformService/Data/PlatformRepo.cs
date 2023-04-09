using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private AppDbContext _context { get; }
        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform plat)
        {
            if(plat is null)
                throw new ArgumentNullException(nameof(plat));

            _context.Platforms.Add(plat);
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}