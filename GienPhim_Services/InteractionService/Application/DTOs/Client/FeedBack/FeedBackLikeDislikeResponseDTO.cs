using GienPhim_Services.InteractionService.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack
{
    public class FeedBackLikeDislikeResponseDTO
    {
        public string FeedbackId { get; set; }
        public int PhimId { get; set; }
        public string UserId { get; set; }
        public bool LuotThichP { get; set; }
        public bool LuotDislikeP { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
