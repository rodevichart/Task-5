﻿using System;

namespace VideoRentBL.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GenreDto Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte NumberInStock { get; set; }
        public byte NumberAvailable { get; set; }

    }
}