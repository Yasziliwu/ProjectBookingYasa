using BookingAPI.Contracts;
using BookingAPI.DTOs;
using BookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingAPI.Services
{
    public class PelangganService : IPelangganRepository
    {
        private readonly IPelangganRepository _repository;

        public PelangganService(IPelangganRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pelanggan>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Pelanggan?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Pelanggan?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<Pelanggan>> FindAsync(Expression<Func<Pelanggan, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task AddAsync(Pelanggan entity)
        {
            await _repository.AddAsync(entity);
        }

        public void Update(Pelanggan entity)
        {
            _repository.Update(entity);
        }

        public void Delete(Pelanggan entity)
        {
            _repository.Delete(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        public async Task<Pelanggan?> LoginAsync(string email)
        {
            return await _repository.LoginAsync(email);
        }

        public IQueryable<Pelanggan> Query()
        {
            return _repository.Query();
        }
    }
}
