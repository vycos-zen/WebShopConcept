using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MagicDay.BusinessLogic.ImageTasks
{
    
    public class ImageUploadItem
    {
        [DataObjectField(true)]
        public Guid ImageId { get; set; }       
        public string ImageName { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumbnail { get; set; }
    }
}
