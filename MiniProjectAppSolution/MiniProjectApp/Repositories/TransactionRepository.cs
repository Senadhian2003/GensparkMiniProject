﻿using Microsoft.EntityFrameworkCore.Storage;
using MiniProjectApp.Context;
using MiniProjectApp.Repositories.Interface;

namespace MiniProjectApp.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LibraryManagementContext _context;
        private IDbContextTransaction _transaction;

        public TransactionRepository(LibraryManagementContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

    }
}
