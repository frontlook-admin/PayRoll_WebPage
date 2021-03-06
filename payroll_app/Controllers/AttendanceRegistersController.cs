﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using payroll_app.Data;
using payroll_app.Models.repository;

namespace payroll_app.Controllers
{
    public class AttendanceRegistersController : Controller
    {
        private readonly payroll_app_context _context;

        public AttendanceRegistersController(payroll_app_context context)
        {
            _context = context;
        }

        // GET: AttendanceRegisters
        public async Task<IActionResult> Index()
        {
            var payroll_app_context = _context.AttendanceRegister.Include(a => a.Employees);
            return View(await payroll_app_context.ToListAsync());
        }

        // GET: AttendanceRegisters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRegister = await _context.AttendanceRegister
                .Include(a => a.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceRegister == null)
            {
                return NotFound();
            }

            return View(attendanceRegister);
        }

        // GET: AttendanceRegisters/Create
        public IActionResult Create()
        {
            
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName");
            return View();
        }

        // POST: AttendanceRegisters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Attendance,AttendanceTime")] AttendanceRegister attendanceRegister)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendanceRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName", attendanceRegister.EmployeeId);
            return View(attendanceRegister);
        }

        // GET: AttendanceRegisters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRegister = await _context.AttendanceRegister.FindAsync(id);
            if (attendanceRegister == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName", attendanceRegister.EmployeeId);
            return View(attendanceRegister);
        }

        // POST: AttendanceRegisters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Attendance,AttendanceTime")] AttendanceRegister attendanceRegister)
        {
            if (id != attendanceRegister.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceRegisterExists(attendanceRegister.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName", attendanceRegister.EmployeeId);
            return View(attendanceRegister);
        }

        // GET: AttendanceRegisters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRegister = await _context.AttendanceRegister
                .Include(a => a.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceRegister == null)
            {
                return NotFound();
            }

            return View(attendanceRegister);
        }

        // POST: AttendanceRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendanceRegister = await _context.AttendanceRegister.FindAsync(id);
            _context.AttendanceRegister.Remove(attendanceRegister);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceRegisterExists(int id)
        {
            return _context.AttendanceRegister.Any(e => e.Id == id);
        }
    }
}
