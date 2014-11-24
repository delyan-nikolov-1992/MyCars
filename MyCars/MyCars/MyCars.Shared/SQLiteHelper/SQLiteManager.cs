namespace MyCars.SQLiteHelper
{
    ﻿using MyCars.Models.SQLiteModels;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Windows.Storage;

    public class SQLiteManager
    {
        private const string dbName = "MyCars.db";

        public SQLiteManager()
        {
            this.InitializeDatabase();
        }

        public string DbName
        {
            get
            {
                return dbName;
            }
        }

        private async void InitializeDatabase()
        {
            bool dbExists = await CheckDbAsync();

            if (!dbExists)
            {
                await CreateDatabaseAsync();
            }
       }

        private async Task<bool> CheckDbAsync()
        {
            bool dbExist = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }

        private async Task CreateDatabaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.CreateTableAsync<CarSQLiteModel>();
        }

        public async Task AddCarAsync(CarSQLiteModel car)
        {
            // Add row to the Cars Table
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.InsertAsync(car);
        }

        public async Task<IEnumerable<CarSQLiteModel>> SearchCarsAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);

            AsyncTableQuery<CarSQLiteModel> query = conn.Table<CarSQLiteModel>();
            List<CarSQLiteModel> result = await query.ToListAsync();

            return result;
        }

        public async Task DeleteCarAsync(string objectId)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);

            // Retrieve Car
            var car = await conn.Table<CarSQLiteModel>().Where(c => c.ParseObjectId == objectId).FirstOrDefaultAsync();

            if (car != null)
            {
                // Delete record
                await conn.DeleteAsync(car);
            }
        }

        public async Task<bool> ExistCarAsync(string objectId)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);

            // Retrieve Car
            var car = await conn.Table<CarSQLiteModel>().Where(c => c.ParseObjectId == objectId).FirstOrDefaultAsync();

            if (car != null)
            {
                return true;
            }

            return false;
        }
    }
}