using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace RssReaderVS2017.Models
{
    public class RSS
    {
        public RSS()
        {

        }
        public RSS(string Link, string Date, string Title, string Description)
        {
            this.Link = Link;
            this.Date = Date;
            this.Title = Title;
            this.Description = Description;
        }
        public string Date { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
    }
}