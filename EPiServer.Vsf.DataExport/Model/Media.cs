﻿using Nest;

namespace EPiServer.Vsf.DataExport.Model
{
    public class Media
    {
        [PropertyName("image")]
        public string Image { get; set; }

        [PropertyName("pos")]
        public int Position { get; set; }

        [PropertyName("typ")]
        public string Type { get; set; }

        [PropertyName("lab")]
        public string Label { get; set; }
    }
}