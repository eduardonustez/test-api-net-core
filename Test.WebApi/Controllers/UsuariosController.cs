using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.WebApi.Data;
using Test.WebApi.Model;

namespace Test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ContextoBase _contexto;
        public UsuariosController(ContextoBase contexto)
        {
            _contexto = contexto;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _contexto.usuarios.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var _usuario = await _contexto.usuarios.FindAsync(id);
            if (_usuario == null)
            {
                return NotFound();
            }
            else
                return _usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _contexto.usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuarioById), new { Id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }
            _contexto.Entry(usuario).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _contexto.usuarios.FindAsync(id);
           if(usuario==null)
            {
                return NotFound();
            }
            _contexto.usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}