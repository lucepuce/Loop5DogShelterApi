using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DogShelter.Api;

    public class DogContext : DbContext
    {
        public DogContext (DbContextOptions<DogContext> options)
            : base(options)
        {
        }

        public DbSet<DogShelter.Api.Dog> Dog { get; set; } = default!;
    }
