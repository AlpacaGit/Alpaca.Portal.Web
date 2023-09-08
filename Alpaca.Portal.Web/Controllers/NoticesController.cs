using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alpaca.Portal.Web.Data;
using Alpaca.Portal.Web.Models;
using Alpaca.Portal.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Alpaca.Portal.Web.Controllers
{
    public class NoticesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<NoticesController> _logger;
        public NoticesController(ApplicationDbContext context,
            ILogger<NoticesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Notices
        public async Task<IActionResult> Index()
        {
              return _context.Notice != null ? 
                          View(await _context.Notice.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Notice'  is null.");
        }

        // GET: Notices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var viewModel =  new ViewModels.Notices.Detail();
            if (id == null || _context.Notice == null)
            {
                return NotFound();
            }

            var notice = await _context.Notice
                .FirstOrDefaultAsync(m => m.NoticeId == id);
            if (notice == null)
            {
                return NotFound();
            }
            viewModel.Head = notice;

            //以下ダミー本文
            var body = new Models.NoticeDetail();
            body.NoticeId = notice.NoticeId;
            body.NoticeBody = "これはダミー本文です。";

            viewModel.Body = body;

            return View(viewModel);
        }

        // GET: Notices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoticeId,NoticeTitle")] Notice notice)
        {
            if (ModelState.IsValid)
            {
                notice.RegistDate = DateTime.Now;
                _context.Add(notice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notice);
        }

        // GET: Notices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Notice == null)
            {
                return NotFound();
            }

            var notice = await _context.Notice.FindAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            return View(notice);
        }

        // POST: Notices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoticeId,NoticeTitle")] Notice notice)
        {
            if (id != notice.NoticeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //更新用パラメタとして前データを取っておく
                    var updateParam = _context.Notice.Where(x =>
                        x.NoticeId == id).Single();
                    updateParam.NoticeTitle = notice.NoticeTitle;

                    _context.Notice.Update(updateParam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticeExists(notice.NoticeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogCritical("謎のエラー発生：" + ex.Message);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(notice);
        }

        // GET: Notices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Notice == null)
            {
                return NotFound();
            }

            var notice = await _context.Notice
                .FirstOrDefaultAsync(m => m.NoticeId == id);
            if (notice == null)
            {
                return NotFound();
            }

            return View(notice);
        }

        // POST: Notices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Notice == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Notice'  is null.");
            }
            var notice = await _context.Notice.FindAsync(id);
            if (notice != null)
            {
                _context.Notice.Remove(notice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticeExists(string id)
        {
          return (_context.Notice?.Any(e => e.NoticeId == id)).GetValueOrDefault();
        }

        private DateTime GetNoticeRegistDate(string id)
        {
            var datetime = DateTime.Now;
            try
            {
                var notice = _context.Notice.Where(x => 
                x.NoticeId == id).FirstOrDefault();
                if (notice == null)
                {
                    datetime = DateTime.Now;
                }
                else
                {
                    datetime = notice.RegistDate;
                }

            }
            catch
            {

            }
            return datetime;
        }
    }
}
