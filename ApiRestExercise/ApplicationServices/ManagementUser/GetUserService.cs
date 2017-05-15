﻿using System.Threading.Tasks;
using ApplicationCore.DTOs;
using DomainCore.Repository;
using DomainCore.Logic.UserLogic;
using ApplicationCore.Contracts.UserContracts;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using CrossCutting.Resources;

namespace ApplicationServices.ManagementUser
{
    /// <summary>
    /// Servicio que orquesta las acciones para mantener un usuario.
    /// </summary>
    public class GetUserService : IGetUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IGetUserLogic _userLogic;
        public GetUserService(
            IUserRepository userRepository,
            IGetUserLogic userLogic
            )
        {
            if (userRepository == null || userLogic == null)
                throw new ArgumentNullException(Resource.ExceptionNullObject);
            _userRepository = userRepository;
            _userLogic = userLogic;
        }


        public async Task<IEnumerable<UserDto>> GetUserAll()
        {
            var userAll = await _userRepository.GetAll().ToListAsync();
            return MapperUser.MapFromEntityListToDtoList(userAll);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var userAll =  _userRepository.GetAllWithTracking();
            var userFinded = await _userLogic.QueryToGetUserById(userAll, id).FirstOrDefaultAsync();
            _userLogic.ValidationsToGetById(userFinded);
            return MapperUser.MapFromEntityToDto(userFinded);
        }
      
    }
}
