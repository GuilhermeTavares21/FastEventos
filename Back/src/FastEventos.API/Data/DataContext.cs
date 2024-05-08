using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEventos.Domain;
using Microsoft.EntityFrameworkCore;

namespace FastEventos.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options){}
        public DbSet<Evento> Eventos { get; set; }


    }
}