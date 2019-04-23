using System;

namespace Catalog.BLL.Pagination
{
    public class ErrorView
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}