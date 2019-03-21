using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace IdentityDeepDive
{
    public class PluralSightUserStore : IUserStore<PluralSightUser>, IUserPasswordStore<PluralSightUser>
    {
        public async Task<IdentityResult> CreateAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "update PluralsightUsers " +
                    "set [Id] = @id," +
                    "[UserName] = @userName," +
                    "[NormalizedUserName] = @normalizedUserName," +
                    "[PasswordHash] = @passwordHash " +
                    "where [Id] = @id",
                    new PluralSightUser
                    {
                        //id = user.Id,
                        //userName = user.UserName,
                        //normalizedUserName = user.NormalizedUserName,
                        //passwordHash = user.PasswordHash
                        Id = user.Id,
                        UserName = user.UserName,
                        NormalizedUserName=user.NormalizedUserName,
                        PasswordHash=user.PasswordHash
                        
                    }
                );
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public async Task<PluralSightUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<PluralSightUser>(
                    "select * From PluralsightUsers where Id = @id",
                    new { id = userId });
            }
        }

        public async Task<PluralSightUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<PluralSightUser>(
                    "select * From PluralsightUsers where NormalizedUserName = @name",
                    new { name = normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(PluralSightUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;

           return  Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(PluralSightUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(PluralSightUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(PluralSightUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Data Source = DESKTOP-JK3L343\\MSSQLSERVER01;"+"database=aa;" + " trusted_connection=yes;");

            connection.Open();

            return connection;
        }
    }
}
