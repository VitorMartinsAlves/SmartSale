using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase {
        BD_SmartSaleContext _context = new BD_SmartSaleContext ();

        /// <summary>
        /// Lista os Tipos de Usuario
        /// </summary>
        /// <returns>Lista contendo os Tipos de Usuario</returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get () {
            var tipo_usuario = await _context.TipoUsuario.ToListAsync ();
            if (tipo_usuario == null) {
                return NotFound ();
            }
            return tipo_usuario;
        }

        /// <summary>
        /// Exibe um Tipo de Usuario Especifica
        /// </summary>
        /// <param name="id">int Id do Tipo de Usuario desejado</param>
        /// <returns>Tipo de Usuario Requisitado</returns>
        [Authorize]
        [HttpGet ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get (int id) {
            var tipo_usuario = await _context.TipoUsuario.FindAsync (id);
            if (tipo_usuario == null) {
                return NotFound ();
            }
            return tipo_usuario;
        }

        /// <summary>
        /// Adiciona um Tipo de Usuario
        /// </summary>
        /// <param name="tipo_usuario">string tipo de usuario</param>
        /// <returns>Tipo de Usuario Cadastrado</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post (TipoUsuario tipo_usuario) {
            try {
                await _context.AddAsync (tipo_usuario);
                await _context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }

            return tipo_usuario;
        }

        /// <summary>
        /// Faz a modificação de derterminado Tipo de Usuario
        /// </summary>
        /// <param name="id"> int id do Tipo de Usuario</param>
        /// <param name="tipo_usuario">string nome tipo do usuario</param>
        /// <returns>Tipo de Usuario Modificado</returns>
        [Authorize]
        [HttpPut ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Put (int id, TipoUsuario tipo_usuario) {
            if (id != tipo_usuario.IdTipoUsuario) {
                return BadRequest ();
            }
            _context.Entry (tipo_usuario).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var tipo_de_usuario_valida = await _context.TipoUsuario.FindAsync (id);
                if (tipo_de_usuario_valida == null) {
                    return NotFound ();
                } else {
                    throw;
                }

            }
            return tipo_usuario;
        }

        /// <summary>
        /// Delete o Tipo de Usuario Especificado
        /// </summary>
        /// <param name="id">int id do Tipo de Usuario</param>
        /// <returns>Tipo de Usuario deletado</returns>
        [Authorize]
        [HttpDelete ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Delete (int id) {
            var tipo_usuario = await _context.TipoUsuario.FindAsync (id);
            if (tipo_usuario == null) {
                return NotFound ();
            }
            _context.TipoUsuario.Remove (tipo_usuario);
            await _context.SaveChangesAsync ();
            return tipo_usuario;
        }
    }
}