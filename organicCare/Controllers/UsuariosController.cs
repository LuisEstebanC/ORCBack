using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using organicCare.Context;
using organicCare.Models;

namespace organicCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> Getusuarios()
        {
            return await _context.usuarios
                .Select(x => UsuarioToDTO(x))
                .ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarios(int id)
        {
            var usuarios = await _context.usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return UsuarioToDTO(usuarios);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.id)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CreateUsuarios([FromBody]UsuarioDTO usuarioDTO)
        {
            var usuarios = new Usuarios
            {
                apellido = usuarioDTO.apellido,
                nombre = usuarioDTO.nombre,
                correo = usuarioDTO.correo,
                username = usuarioDTO.username,
                password = usuarioDTO.password
            };

            _context.usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUsuarios),
                new { id = usuarios.id },
                UsuarioToDTO(usuarios));
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuarios>> DeleteUsuarios(int id)
        {
            var usuarios = await _context.usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return usuarios;
        }

        [HttpGet("{username}/{password}")]
        public ActionResult<List<Usuarios>> GetIniciarSesion(string username, string password)
        {
            var usuarios = _context.usuarios.Where(usuario => usuario.username.Equals(username) && usuario.password.Equals(password)).ToList();

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        private bool UsuariosExists(int id)
        {
            return _context.usuarios.Any(e => e.id == id);
        }

        private static UsuarioDTO UsuarioToDTO(Usuarios usuarios) =>
            new UsuarioDTO
            {
                apellido = usuarios.apellido,
                nombre = usuarios.nombre,
                correo = usuarios.correo,
                username = usuarios.username,
                password = usuarios.password
            };



    }
}
