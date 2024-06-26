﻿using System;
using SQLite;

namespace TravelRecordApp.Model
{
    public class Post
    {
        public string Id { get; set; }

        public string Experience { get; set; }

        public string VenueName { get; set; }

        public string PlaceId { get; set; }

        public string TypePlace { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }

        public string UserId { get; set; }
    }
}

