using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment model){
            return new CommentDto{
                Id=model.Id,
                Content=model.Content,
                CreatedOn=model.CreatedOn,
                Title=model.Title,
                StockId=model.StockId
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto model,int stockId){
            return new Comment{
                Content=model.Content,
                Title=model.Title,
                StockId=stockId
            };
        }
    }
}