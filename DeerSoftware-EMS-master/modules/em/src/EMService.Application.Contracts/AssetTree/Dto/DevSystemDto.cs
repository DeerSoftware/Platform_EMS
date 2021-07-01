using System;
using System.Collections.Generic;
using System.Text;

namespace EMService.AssetTree.Dto
{
    public class DevSystemDto : FoundationDto
    {
        public string SystemGroup { get; set; }
        public string SystemClass { get; set; }
        public string Description { get; set; }
    }
}
