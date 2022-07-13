using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DogShelter.Api;
using Azure.Storage.Blobs;

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

      var blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=loop5storageaccount;AccountKey=cdJ3laNHQnFCRwkSPOfs5g7/xhwVV5PAFJACdAf3hgd9A7KR2x4z8oNW1HAqIbNU7QvIEo96uhac+AStPwQP6g==;EndpointSuffix=core.windows.net");
      string containerName = "marcusoftnet" + Guid.NewGuid().ToString();
      containerClient = blobServiceClient.CreateBlobContainer(containerName);
    }

    [HttpPost ("UploadDog")]
    public async Task<ActionResult> UploadADog()
    {
      string localFilePath = "./Pictures/golden-retriever-royalty-free-image-506756303-1560962726.jpeg";
      // Set filename for blob
      string fileName = Guid.NewGuid().ToString() + ".gif";
      var blobClient = containerClient.GetBlobClient(fileName);
      Console.WriteLine("Uploading to Blob storage:\n\t {0}\n", blobClient.Uri);
      // Upload data from the local file
      await blobClient.UploadAsync(localFilePath, true);
      return Ok();
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
