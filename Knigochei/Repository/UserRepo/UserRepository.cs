﻿using Knigochei.Models;
using System.Data;
using Dapper;

namespace Knigochei.Repository.UserRepo
{
    internal class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public void Add(User user)
        {
            user.Id = Connection.ExecuteScalar<int>(
                sql: "INSERT INTO Users(Email, UserPassword, FirstName, LastName, GenderId, RoleId) " +
                "VALUES(@email, @password, @firstName, @lastName, @genderId, @roleId); " +
                "SELECT SCOPE_IDENTITY();",
                param: new
                {
                    @email = user.Email,
                    @password = user.UserPassword,
                    @firstName = user.FirstName,
                    @lastName = user.LastName,
                    @genderId = user.GenderId,
                    @roleId = user.RoleId
                },
                transaction: Transaction
            );
        }

        public IEnumerable<User> All()
        {
            return Connection.Query<User>(
                sql: "SELECT * FROM Users",
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public User Find(int id)
        {
            throw new NotImplementedException();
        }

        public User FindByEmail(string email)
        {
            return Connection.Query<User>(
                sql: "SELECT * FROM Users u WHERE u.Email = @email",
                param: new { @email = email },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public User FindByEmailAndPassword(string email, string password)
        {
            return Connection.Query<User>(
                sql:"SELECT * FROM Users u WHERE u.Email = @email AND u.UserPassword = @password",
                param:new { @email = email, @password = password },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public User FindByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}