using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DogShelter.Api;

namespace DogShelter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly DogContext _context;

        BlobContainerClient containerClient;

        public DogController(DogContext context)
        {
            _context = context;
            
            var blobServiceClient = new BlobServiceClient(connectionString);
            string containerName = "marcusoftnet" + Guid.NewGuid().ToString();
            containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        }

        public async Task<ActionResult> UploadADog()
        {
 
            string localFilePath = "DogShelter.Api/Pictures/golden-retriever-royalty-free-image-506756303-1560962726.jpeg";
        }

        // GET: api/Dog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDog()
        {
          if (_context.Dog == null)
          {
              return NotFound();
          }
            return await _context.Dog.ToListAsync();
        }

        public async Task<ActionResult> UploadADog()
        {
            string localPath = "./";
            string localFilePath = Path.Combine(localPath, "aPicture.gif");
        }

        // GET: api/Dog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> GetDog(int id)
        {
          if (_context.Dog == null)
          {
              return NotFound();
          }
            var dog = await _context.Dog.FindAsync(id);

            if (dog == null)
            {
                return NotFound();
            }

            return dog;
        }

        // PUT: api/Dog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDog(int id, Dog dog)
        {
            if (id != dog.Id)
            {
                return BadRequest();
            }

            _context.Entry(dog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dog>> PostDog(Dog dog)
        {
          if (_context.Dog == null)
          {
              return Problem("Entity set 'DogContext.Dog'  is null.");
          }
            _context.Dog.Add(dog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDog", new { id = dog.Id }, dog);
        }

        // DELETE: api/Dog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDog(int id)
        {
            if (_context.Dog == null)
            {
                return NotFound();
            }
            var dog = await _context.Dog.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }

            _context.Dog.Remove(dog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DogExists(int id)
        {
            return (_context.Dog?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
