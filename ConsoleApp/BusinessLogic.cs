using EfCore.Data;
using EfCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class BusinessLogic
    {
        private SamuraiTestDataContext _context;
        public BusinessLogic()
        {
            _context = new SamuraiTestDataContext();
        }

        public BusinessLogic(SamuraiTestDataContext context)
        {
            _context = context;
        }

        public Task<int> AddMultipleSamurais(string[] samuraiNames)
        {
            var samuraiList = new List<Samurai>();
            foreach (var name in samuraiNames)
                samuraiList.Add(new Samurai { Name = name });

            _context.Samurais.AddRange(samuraiList);
            return _context.SaveChangesAsync();
        }

        public Task<int> InsertNewSamurai(Samurai samurai)
        {
            _context.Samurais.Add(samurai);
            return _context.SaveChangesAsync();
        }
    }
}
