using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.Domain;
using FastEventos.Persistence.Contratos;
using FastEventos.Persistence.Contextos;
using Microsoft.EntityFrameworkCore;

namespace FastEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly FastEventosContext _context;
        public GeralPersist(FastEventosContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}