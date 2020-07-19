using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace JourneyToTheWest.Models.ApiDTO
{
    public class NewEquipment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public MultipartFileData File { get; set; }
    }
}