TbdMinimalApiProject Introduction

TbdMinimalApiProject is a .NET Core API project designed to manage users, artists, genres, and songs. The application utilizes Entity Framework Core for database interactions and includes functionalities to add, retrieve, and relate data. Notably, retrieves related artist information from an external API.

Collaborators

• Tensae Girma 

• Hugo Leijonhufvud 
• Edris Ahmed

Prerequisites

.NET Core SDK Microsoft SQL Server Management System (For Database) Setup

Clone the repository:

git clone https://github.com/ETW-BANK/TbdMinimalApiProject.git

Endpoints

GET Endpoints

/GetUser/{userId} - Get a specific user by ID. /GetUsers/ - Get all users. /GetArtists/{userId} - Get artists associated with a specific user. /GetRelated/{artistId} - GET method - Retrieve related information for a specific artist from an external API. /GetSongs/{userId} - Get songs associated with a specific user. /GetGenres/{userId} - Get genres associated with a specific user.

POST Endpoints

/AddUser/ - Add a new user. /AddArtists/{userId} - Add artists for a specific user. /AddGenres/{userId}/{artistId} - Add genres for a specific user and artist. /AddSongs/{userId}/{artistId}/{genreId} - Add songs for a specific user, artist, and genre.
