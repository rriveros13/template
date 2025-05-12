using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SYSVETE.Autorizacion;
using SYSVETE.Models;
using SYSVETE.Models.DTOs;
using SYSVETE.Services;


namespace SYSVETE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : Controller
    {
        private readonly IPersona _service;

        public PersonasController(IPersona service)
        {
            _service = service;
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var res = await _service.ObtenerPersonas();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Consultar)]
        [HttpGet("obtenerPorId")]
        public async Task<IActionResult> ObtenerPersonaPorId(int idPersona)
        {
            try
            {
                var res = await _service.ObtenerPersonaPorId(idPersona);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [Autorizar(AccionesDefinidas.Agregar)]
        [HttpPost]
        public async Task<IActionResult> AgregarPersona(PersonaDto persona)
        {
            try
            {
                Usuario? usuarioSYSVETE = HttpContext.Items["UsuarioSYSVETE"] as Usuario;
                await _service.AgregarPersona(persona, usuarioSYSVETE.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }



        //// GET: Personas/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Personas == null)
        //    {
        //        return NotFound();
        //    }

        //    var persona = await _context.Personas.FindAsync(id);
        //    if (persona == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(persona);
        //}

        //// POST: Personas/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdPersona,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Cedula,FechaNacimiento")] Persona persona)
        //{
        //    if (id != persona.IdPersona)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(persona);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PersonaExists(persona.IdPersona))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(persona);
        //}

        //// GET: Personas/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Personas == null)
        //    {
        //        return NotFound();
        //    }

        //    var persona = await _context.Personas
        //        .FirstOrDefaultAsync(m => m.IdPersona == id);
        //    if (persona == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(persona);
        //}

        //// POST: Personas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Personas == null)
        //    {
        //        return Problem("Entity set 'SYSVETEContext.Personas'  is null.");
        //    }
        //    var persona = await _context.Personas.FindAsync(id);
        //    if (persona != null)
        //    {
        //        _context.Personas.Remove(persona);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PersonaExists(int id)
        //{
        //  return _context.Personas.Any(e => e.IdPersona == id);
        //}
    }
}
