using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Liron_api.Data;
using Liron_api.Models;

namespace Liron_api.Controllers
{
    public class ConversationsController : Controller
    {
        private readonly Liron_apiContext _context;

        public ConversationsController(Liron_apiContext context)
        {
            _context = context;
        }

        // GET: Conversations
        public async Task<IActionResult> Index()
        {
              return _context.Conversation != null ? 
                          View(await _context.Conversation.ToListAsync()) :
                          Problem("Entity set 'Liron_apiContext.Conversation'  is null.");
        }

        // GET: Conversations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Conversation == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversation
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // GET: Conversations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conversations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,Last")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Conversation == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,Last")] Conversation conversation)
        {
            if (id != conversation.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.MessageId))
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
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Conversation == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversation
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conversation == null)
            {
                return Problem("Entity set 'Liron_apiContext.Conversation'  is null.");
            }
            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation != null)
            {
                _context.Conversation.Remove(conversation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
          return (_context.Conversation?.Any(e => e.MessageId == id)).GetValueOrDefault();
        }
    }
}
