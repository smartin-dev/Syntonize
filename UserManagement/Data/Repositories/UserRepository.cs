using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Data.Models;

namespace UserManagement.Data.Repositories
{
    /// <summary>
    /// Repositorio de usuarios.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Obtiene todos los usuarios de la base de datos.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public List<User> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.ToList();
            }
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario encontrado o null si no existe.</returns>
        public User GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        /// <summary>
        /// Agrega un nuevo usuario a la base de datos.
        /// </summary>
        /// <param name="user">Usuario a agregar.</param>
        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            using (var context = new ApplicationDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Actualiza un usuario existente en la base de datos.
        /// </summary>
        /// <param name="user">Usuario con los datos actualizados.</param>
        public void Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            using (var context = new ApplicationDbContext())
            {
                var existingUser = context.Users.Find(user.Id);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    // Actualizar otras propiedades según sea necesario
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"User with ID {user.Id} not found.");
                }
            }
        }

        /// <summary>
        /// Elimina un usuario de la base de datos por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        public void Delete(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var userToRemove = context.Users.Find(id);
                if (userToRemove != null)
                {
                    context.Users.Remove(userToRemove);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException($"User with ID {id} not found.");
                }
            }
        }




        /// <summary>
        /// Obtiene un usuario por su email.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        /// <returns>El usuario encontrado o null si no existe.</returns>
        public User GetByEmail(string email)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }
        }
    }
}
