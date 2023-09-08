using System.ComponentModel.DataAnnotations;

namespace Alpaca.Portal.Web.Models
{
    /// <summary>
    /// お知らせ詳細
    /// </summary>
    public class NoticeDetail
    {
        private string? _noticeId;
        /// <summary>
        /// お知らせID
        /// </summary>
        [Key]
        public string NoticeId
        {
            get => _noticeId ?? string.Empty;
            set => _noticeId = value;
        }

        private string? _noticeBody;
        /// <summary>
        /// お知らせ本文
        /// </summary>
        public string NoticeBody
        {
            get => _noticeBody ?? string.Empty;
            set => _noticeBody = value;
        }
    }
}
