using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public readonly ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext _context)
        {
            context=_context;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var model=await context.Comments.FindAsync(id);
            if (model==null)
                return null;
            context.Remove(model);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment=await context.Comments.FindAsync(id);
            if (comment==null)
                return null;
            return comment;
        }

        public async Task<bool> StockExist(int stockId)
        {
            return await context.Stocks.AnyAsync(x=>x.Id==stockId);
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var model=await context.Comments.FindAsync(id);
            if (model==null)
            {
                return null;
            }
            model.Title=updateCommentDto.Title;
            model.Content=updateCommentDto.Content;
            await context.SaveChangesAsync();
            return model;
        }
    }
}