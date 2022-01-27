using System;
using System.Collections.Generic;
using System.Text;

namespace TouristGuide.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string UserId { get; set; }
        public string Adress { get; set; }
    }
}
