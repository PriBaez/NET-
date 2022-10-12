using Microsoft.AspNetCore.Mvc;
using NET_Mongo.Models;
using NET_Mongo.Repositories;

namespace NET_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductCollection db = new ProductCollection();
        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            //al no recibir parametros se ejecuta un codigo 200/Ok y se invoca 
            //el metodo de la coleccion (GetAllProducts)
            return Ok(await db.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneProduct(string id)
        {
            //En este caso se le pasa el id al metodo de la coleccion para que
            //obtenga ese articulo en especifico
            return Ok(await db.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            //se hacen dos validaciones si el producto es nulo o el nombre esta
            //vacio
            if(product == null) return BadRequest();

            if (product.Name == string.Empty) ModelState.AddModelError("Name", "The product name shouldn't be empty");
        
            //si pasa todas las validaciones ejecuta el metodo de creacion
            // de la clase ProductCollection pasandole la informacion recibida

            await db.InsertProduct(product);

            //si todo sale bien devuelve un codigo 201
            return Created("Created", true);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product, string id)
        {
            if(product == null) return BadRequest();

            if (product.Name == string.Empty) ModelState.AddModelError("Name", "The product name shouldn't be empty");
        
            product.Id = new MongoDB.Bson.ObjectId(id);
            await db.UpdateProduct(product);

            return Created("Updated", true);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await db.DeleteProduct(id);

            return NoContent(); //NoContent porque se borro y no hay nada que mostrar
        }

    }
}