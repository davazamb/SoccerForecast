﻿using SoccerForecast.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PicturePath { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
    ? "https://Soccerforecastweb.azurewebsites.net//images/noimage.png"
    : $"https://soccerforecaststorageapp.blob.core.windows.net/users/{PicturePath}";


        public UserType UserType { get; set; }

        public TeamResponse Team { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

    }

}
