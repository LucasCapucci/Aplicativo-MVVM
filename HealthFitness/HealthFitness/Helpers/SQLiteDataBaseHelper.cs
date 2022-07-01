using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HealthFitness.Models;
using SQLite;

namespace HealthFitness.Helpers
{
    public class SQLiteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _db;

        public SQLiteDataBaseHelper(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);

            _db.CreateTableAsync<Exercicio>().Wait();

        }

      
        public Task<List<Exercicio>> GetAllRows()
        {
            return _db.Table<Exercicio>().OrderByDescending(i => i.ID).ToListAsync();
        }        
        
        public Task<Exercicio> GetById(int id)
        {
            return _db.Table<Exercicio>().FirstAsync(i => i.ID == id);
        }
        
        public Task<int> Insert(Exercicio model)
        {
            return _db.InsertAsync(model);
        }

        public Task<List<Exercicio>> Update(Exercicio model)
        {
            string sql = "UPDATE Exercicio SET Descricao=?, Data=?, Peso=?, Observacoes=? WHERE ID=?";

            return _db.QueryAsync<Exercicio>(
                sql,
                model.Descricao,
                model.Data,
                model.Peso,
                model.Observacoes,
                model.ID
                );
        }

        public Task<int> Delete(int id)
        {
            return _db.Table<Exercicio>().DeleteAsync(i => i.ID == id);
        }

        public Task<List<Exercicio>> Search(string q)
        {
            string sql = "SELECT * FROM Exercicio WHERE Descricao LIKE '%" + q + "%'";

            return _db.QueryAsync<Exercicio>(sql);
        }
    }
}
