﻿using System;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using DomainCore.Repository;
using DomainCore.Logic.UserLogic;
using CrossCutting.Resources;
using ApplicationCore.Contracts.UserContracts;

namespace ApplicationServices.ManagementUser
{
    /// <summary>
    /// Servicio que orquesta las acciones para mantener un usuario.
    /// </summary>
    public class AddUserService : IAddUserService
    {

        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IAddUserLogic _userLogic;
        public AddUserService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            IAddUserLogic userLogic
            )
        {
            if (uow == null || userRepository == null || userLogic == null)
                throw new ArgumentNullException(Resource.ExceptionNullObject);
            _uow = uow;
            _userRepository = userRepository;
            _userLogic = userLogic;
        }
        /// <summary>
        /// Orquesta todos los trabajos necesarios para añadir un usuario en base de datos.
        /// </summary>
        /// <param name="userDto">Objeto usuario que se va a añadir</param>
        /// <returns></returns>
        public async Task AddUser(UserDto userDto)
        {
            var userAll =  _userRepository.GetAll();
            _userLogic.LogicToAdd(userAll, userDto);
            var user = MapperUser.MapFromDtoToEntity(userDto);
            _userRepository.Add(user);
            await _uow.CommitAsync();
        }
    }
}
