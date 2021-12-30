using Pan.Infrastructure.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Pan.Infrastructure.Entity
{
    public partial class FileStorage: BaseEntity
    {
        public string FileExt { get; set; }
        public double FileSize { get; set; }
        public int FileType { get; set; }
        public string HashCode { get; set; }
        public string PartDir { get; set; }
        public string PathLocal { get; set; }
        public string Title { get; set; }
        public string OSSName { get; set; }
        public string OSSBucketName { get; set; }
        public string OSSETag { get; set; }
        public string OSSRequestId { get; set; }
    }
}
