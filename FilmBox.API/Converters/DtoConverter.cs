using FilmBox.Api.DTOs;
using FilmBox.Api.DTOs.GetDTOs;
using FilmBox.Api.DTOs.PostDTOs;
using FilmBox.Api.Models;
using FilmBox.API.DTOs.GetDTOs;
using FilmBox.Shared.DTOs.PostDTOs;
using System.Collections.Generic;

namespace WebApi.DTOs.Converters
{
    /// <summary>
    /// Tool class for storing extension methods for converting DTOs to Model objects and back
    /// </summary>
    public static class DtoConverter
    {
        #region User conversion methods
        public static UserDto ToDto(this User userToConvert)
        {
            var userDto = new UserDto();
            userToConvert.CopyPropertiesTo(userDto);
            return userDto;
        }

        public static User FromDto(this UserDto userDtoToConvert)
        {
            var user = new User();
            userDtoToConvert.CopyPropertiesTo(user);
            return user;
        }

        public static IEnumerable<UserDto> ToDtos(this IEnumerable<User> usersToConvert)
        {
            foreach (var user in usersToConvert)
            {
                yield return user.ToDto();
            }
        }

        public static IEnumerable<User> FromDtos(this IEnumerable<UserDto> userDtosToConvert)
        {
            foreach (var userDto in userDtosToConvert)
            {
                yield return userDto.FromDto();
            }
        }
        #endregion


        #region Review conversion methods
        public static ReviewDto ToDto(this Review reviewToConvert)
        {
            var reviewDto = new ReviewDto();
            reviewToConvert.CopyPropertiesTo(reviewDto);
            return reviewDto;
        }

        public static Review FromCreateDto(this ReviewCreateDto reviewDtoToConvert)
        {
            var review = new Review();
            reviewDtoToConvert.CopyPropertiesTo(review);
            return review;
        }

        public static IEnumerable<ReviewDto> ToDtos(this IEnumerable<Review> reviewsToConvert)
        {
            foreach (var review in reviewsToConvert)
            {
                yield return review.ToDto();
            }
        }


        #endregion

        #region Media conversion methods
        public static MediaDto ToDto(this Media mediaToConvert)
        {
            var mediaDto = new MediaDto();
            mediaToConvert.CopyPropertiesTo(mediaDto);
            return mediaDto;
        }

        public static Media FromCreateDto(this MediaCreateDto mediaDtoToConvert)
        {
            var media = new Media();
            mediaDtoToConvert.CopyPropertiesTo(media);
            return media;
        }

        public static IEnumerable<MediaDto> ToDtos(this IEnumerable<Media> mediaToConvert)
        {
            foreach (var media in mediaToConvert)
            {
                yield return media.ToDto();
            }
        }


        #endregion
    }
}