﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorDespesas.Models;

namespace GerenciadorDespesas.Controllers
{
    public class SalariosController : Controller
    {
        private readonly Contexto _context;

        public SalariosController(Contexto context)
        {
            _context = context;
        }

        // GET: Salarios
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Salarios.Include(s => s.Meses);
            return View(await contexto.ToListAsync());
        }

        

        // GET: Salarios/Create
        public IActionResult Create()
        {
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId != s.Salarios.MesId), "MesId", "Nome");
            return View();
        }

        // POST: Salarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalarioId,MesId,Valor")] Salarios salarios)
        {
            if (ModelState.IsValid)
            {
                TempData["confirmacao"] = "Salário cadastrado com sucesso.";
                _context.Add(salarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId != s.Salarios.MesId), "MesId", "Nome", salarios.MesId);
            return View(salarios);
        }

        // GET: Salarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarios = await _context.Salarios.FindAsync(id);
            if (salarios == null)
            {
                return NotFound();
            }
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId == salarios.MesId), "MesId", "Nome", salarios.MesId);
            return View(salarios);
        }

        // POST: Salarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalarioId,MesId,Valor")] Salarios salarios)
        {
            if (id != salarios.SalarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salarios);
                    await _context.SaveChangesAsync();
                    TempData["confirmacao"] = "Salário atualizado com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalariosExists(salarios.SalarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId == salarios.MesId), "MesId", "Nome", salarios.MesId);
            return View(salarios);
        }

      

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var salarios = await _context.Salarios.FindAsync(id);
            _context.Salarios.Remove(salarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalariosExists(int id)
        {
            return _context.Salarios.Any(e => e.SalarioId == id);
        }
    }
}
