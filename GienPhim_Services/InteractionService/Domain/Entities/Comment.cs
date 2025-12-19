namespace GienPhim_Services.InteractionService.Domain.Entities
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public int PhimId { get; set; }
        public string? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
        public string? BinhLuan { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
