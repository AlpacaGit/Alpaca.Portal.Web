namespace Alpaca.Portal.Web.ViewModels.Notices
{
    using Alpaca.Portal.Web.Models;
    public class Detail
    {
        private Notice _head;
        /// <summary>
        /// お知らせヘッダ用
        /// </summary>
        public Notice Head
        {
            get => _head; set => _head = value;
        }

        private NoticeDetail _body;
        /// <summary>
        /// お知らせ本文
        /// </summary>
        public NoticeDetail Body
        {
            get => _body; set => _body = value;
        }
    }
}
