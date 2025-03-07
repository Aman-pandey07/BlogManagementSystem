﻿using BlogManagementSystem.Dtos.UserDtos;
using BlogManagementSystem.Models;
using System.Runtime.CompilerServices;

namespace BlogManagementSystem.Mappers
{
    public static class UserDtosMapper
    {
        public static GetAllUserDto ToGetAllUserDto(this UserModel userModel)
        {
            return new GetAllUserDto
            {
                UserName = userModel.UserName,
                UserEmail = userModel.UserEmail,
                UserPhoneNumber = userModel.UserPhoneNumber,
                UserDp = userModel.UserDp,
                UserIsAuthor = userModel.UserIsAuthor,
            };
        }

        public static UserModel ToCreateUserDto(this CreateUserDto createUser)
        {
            return new UserModel
            {
                UserName = createUser.UserName,
                UserEmail = createUser.UserEmail,
                UserPhoneNumber = createUser.UserPhoneNumber,
                UserDp = createUser.UserDp,
                UserIsAuthor = createUser.UserIsAuthor,
                UserCreatedAt = DateTime.Now,
            };
        }

        public static void UpdateUserModel(this UserModel userModel, UpdateUserDto updateUser)
        {
            userModel.UserName = updateUser.UserName;
            userModel.UserEmail = updateUser.UserEmail;
            userModel.UserPhoneNumber = updateUser.UserPhoneNumber;
            userModel.UserDp = updateUser.UserDp;
            userModel.UserIsAuthor = updateUser.UserIsAuthor;
        }


    }
}
