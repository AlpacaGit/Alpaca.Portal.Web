using System.ComponentModel.DataAnnotations;

namespace Alpaca.Portal.Web.Models
{
    /// <summary>
    /// お知らせ
    /// </summary>
    public class Notice
    {
        private string? _noticeId;
        /// <summary>
        /// お知らせId
        /// </summary>
        [Key]
        public string NoticeId
        {
            get => _noticeId ?? string.Empty; 
            set => _noticeId = value; 
        }

        private string? _noticeTitle;
        /// <summary>
        /// お知らせタイトル
        /// </summary>
        public string NoticeTitle
        {
            get => _noticeTitle ?? string.Empty;
            set => _noticeTitle = value;
        }

        private DateTime? _registDate;
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDate
        {
            get => _registDate ?? DateTime.MinValue;
            set => _registDate = value;
        }
    }
}
