using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MagicDay.BusinessLogic.ImageTasks
{
    [Serializable]
    public class ImageUploadItem
    {
        [DataObjectField(true)]
        public Guid ImageID { get; set; }       
        public Guid ProductID { get; set; }
        public string ImageDescription { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumbnail { get; set; }
    }
}
