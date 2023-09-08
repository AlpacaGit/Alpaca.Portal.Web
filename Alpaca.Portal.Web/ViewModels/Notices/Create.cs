using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Alpaca.Portal.Web.Models;

namespace Alpaca.Portal.Web.ViewModels.Notices
{
    public class Create
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
        [DisplayName("お知らせタイトル")]
        public string NoticeTitle
        {
            get => _noticeTitle ?? string.Empty;
            set => _noticeTitle = value;
        }

        private DateTime? _registDate;
        [DisplayName("登録日時")]
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime RegistDate
        {
            get => _registDate ?? DateTime.MinValue;
            set => _registDate = value;
        }

        private string? _noticeBody;
        /// <summary>
        /// お知らせ本文
        /// </summary>
        [DisplayName("お知らせ本文")]
        public string NoticeBody
        {
            get => _noticeBody ?? string.Empty; 
            set => _noticeBody = value;
        }
    }
}
