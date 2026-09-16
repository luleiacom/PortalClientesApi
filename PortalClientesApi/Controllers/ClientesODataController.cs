using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using PortalClientesApi.Data;
using PortalClientesApi.Models;

namespace PortalClientesApi.Controllers
{
    public class ClientesODataController : ODataController
    {
        private readonly AppDbContext _context;

        public ClientesODataController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        [HttpGet("odata/ClientesOData")]
        public IQueryable<Cliente> Get()
        {
            return _context.Clientes;
        }
    }
}
