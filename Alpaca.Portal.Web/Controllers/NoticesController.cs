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
using Microsoft.AspNetCore.Authorization;

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

            var noticeDetail 
                = _context.NoticeDetail.Where(detail =>
                detail.NoticeId == id).Single();

            viewModel.NoticeBody = noticeDetail.NoticeBody;

            return View(viewModel);
        }

        // GET: Notices/Create
        [Authorize(Roles ="Administrators")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("NoticeTitle")] Notice notice, 
            [Bind("NoticeBody")] NoticeDetail noticeDetail)
        {
            if (ModelState.IsValid)
            {
                //GUIDを採番して登録を行う。
                const int RETRY_MAX_COUNT = 5;
                int retryCount = 0;
                while (retryCount < RETRY_MAX_COUNT)
                {
                    //GUID生成
                    var guid = Guid.NewGuid().ToString();
                    //お知らせ追加予定レコードの調整
                    notice.NoticeId = guid;
                    notice.RegistDate = DateTime.Now;
                    //お知らせ詳細追加予定レコードの調整
                    noticeDetail.NoticeId = guid;
                    //お知らせテーブルへのレコード追加
                    try
                    {
                        _context.Notice.Add(notice);
                    }
                    catch
                    {
                        retryCount++;
                        _logger.LogWarning("お知らせテーブルへのレコード登録に失敗 リトライします。：" + retryCount + "回目");
                        continue;
                    }

                    //お知らせ詳細テーブルへのレコード追加
                    try 
                    {
                        _context.NoticeDetail.Add(noticeDetail);
                    }
                    catch
                    {
                        //お知らせで追加予定にしていたものを無効にする。
                        _context.Entry(notice).State = EntityState.Detached;
                        retryCount++;
                        _logger.LogWarning("お知らせ詳細テーブルレコードの登録に失敗 リトライします。：" + retryCount + "回目");
                        continue;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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

            var viewModel = new ViewModels.Notices.Edit();
            var notice = await _context.Notice.FindAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            
            viewModel.NoticeId = notice.NoticeId;
            viewModel.NoticeTitle = notice.NoticeTitle;

            var noticeDetail = await _context.NoticeDetail.FindAsync(id);
            if (noticeDetail == null)
            {
                return NotFound();
            }

            viewModel.NoticeBody = noticeDetail.NoticeBody;

            return View(viewModel);
        }

        // POST: Notices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NoticeId,NoticeTitle")] Notice notice,

            [Bind("NoticeId,NoticeBody")] NoticeDetail noticeDetail)
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

                    var updateDetailParam
                        = _context.NoticeDetail.Where(x => 
                        x.NoticeId == noticeDetail.NoticeId).Single();

                    updateDetailParam.NoticeBody = noticeDetail.NoticeBody;

                    _context.NoticeDetail.Update(updateDetailParam);

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

            var noticeDetail = await _context.NoticeDetail.FindAsync(id);
            if (noticeDetail != null)
            {
                _context.NoticeDetail.Remove(noticeDetail);
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
