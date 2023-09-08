namespace Alpaca.Portal.Web.ViewModels.Notices
{
    using Alpaca.Portal.Web.Models;
    using System.ComponentModel;

    public class Detail
    {
        private Notice? _head;
        /// <summary>
        /// お知らせヘッダ用
        /// </summary>
        public Notice Head
        {
            get => _head ?? new Notice(); 
            set => _head = value;
        }

        private string? _noticeBody;
        /// <summary>
        /// お知らせ本文
        /// </summary>
        [DisplayName("お知らせ本文")]
        public string NoticeBody
        {
            get => _noticeBody?? string.Empty; 
            set => _noticeBody = value;
        }
    }
}
