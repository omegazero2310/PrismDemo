using PrismDemo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PrismDemo.Services
{
    public class UserInfoServices : IDBServices<UserInfo>
    {
        private SQLiteAsyncConnection _conn;
        ~UserInfoServices()
        {
            _conn.CloseAsync();
        }
        public async Task InitDB()
        {
            if (_conn != null)
                return;

            _conn = new SQLiteAsyncConnection(App.DbPath);
            await _conn.CreateTableAsync<UserInfo>();
        }
        public async Task<bool> Create(UserInfo item)
        {
            try
            {
                await this.InitDB();
                await _conn.InsertAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> Delete(UserInfo item)
        {
            try
            {
                await this.InitDB();
                await _conn.DeleteAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<UserInfo> GetData(object key)
        {
            try
            {
                await this.InitDB();
                return await _conn.GetAsync<UserInfo>(key);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<UserInfo>> GetData(int skip = 0, int take = 0)
        {
            try
            {
                await this.InitDB();
                if(skip > 0 && take > 0)
                    return await _conn.Table<UserInfo>().Skip(skip).Take(take).ToListAsync();
                else if (skip > 0)
                    return await _conn.Table<UserInfo>().Skip(skip).ToListAsync();
                else if(take > 0)
                    return await _conn.Table<UserInfo>().Take(take).ToListAsync();
                else
                    return await _conn.Table<UserInfo>().ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> Update(UserInfo item)
        {
            try
            {
                await this.InitDB();
                await _conn.UpdateAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> Create(IEnumerable<UserInfo> item)
        {
            try
            {
                await this.InitDB();
                await _conn.InsertAllAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> Update(IEnumerable<UserInfo> item)
        {
            try
            {
                await this.InitDB();
                await _conn.UpdateAllAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
