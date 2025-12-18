namespace GienPhim_Services.InteractionService.Application.DTOs.Client.FeedBack
{
    public class ProcessingFeedBackLikeDislikeRequestDTO
    {
        public int PhimId { get; set; }
        public string UserId { get; set; }
        public bool LuotThichP { get; set; }
        public bool LuotDislikeP { get; set; }
    }
}
