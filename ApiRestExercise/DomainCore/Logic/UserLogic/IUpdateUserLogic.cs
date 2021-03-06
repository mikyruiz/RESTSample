﻿using ApplicationCore.DTOs;
using DomainEntities;
using System.Linq;

namespace DomainCore.Logic.UserLogic
{
    public interface IUpdateUserLogic
    {
        /// <summary>
        /// Lógica para actualizar un usuario en base de datos
        /// </summary>
        /// <param name="userAll">Query con tracking para obtener todos los usuarios</param>
        /// <param name="user">Objeto del usuario que se quiere actualizar en base de datos</param>
        IQueryable<User> LogicToUpdate(IQueryable<User> userAll, UserDto user);



    }
}
