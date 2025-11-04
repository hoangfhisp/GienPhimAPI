namespace GienPhim_Services.FilmService.Application.DTOs.Client.Phim
{
    public class ApiPhimDTO
    {
        public bool Status { get; set; }
        public string? Msg { get; set; }
        public MovieData? Movie { get; set; }
        public List<ServerData>? Episodes { get; set; }
    }

    public class MovieData
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Origin_Name { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? Poster_Url { get; set; }
        public string? Thumb_Url { get; set; }
        public string? Trailer_Url { get; set; }
        public string? Time { get; set; }
        public string? Lang { get; set; }
        public int Year { get; set; }
        public long View { get; set; }
        public List<string>? Actor { get; set; }
        public List<string>? Director { get; set; }
        public List<CategoryData>? Category { get; set; }
        public List<CountryData>? Country { get; set; }
    }

    public class CategoryData
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }

    public class CountryData
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }

    public class ServerData
    {
        public string? Server_Name { get; set; }
        public List<EpisodeData>? Server_Data { get; set; }
    }

    public class EpisodeData
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Filename { get; set; }
        public string? Link_Embed { get; set; }
        public string? Link_M3u8 { get; set; }
    }

    public class ApiPhimListResponse
    {
        public bool Status { get; set; }
        public string Msg { get; set; } = string.Empty;
        public List<ApiPhimItemDTO> Items { get; set; } = new();
    }

    public class ApiPhimItemDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Origin_Name { get; set; }
        public string? Type { get; set; }
        public string? Poster_Url { get; set; }
        public string? Thumb_Url { get; set; }
        public string? Episode_Current { get; set; }
        public string? Status { get; set; }
        public string? Quality { get; set; }
        public string? Lang { get; set; }
        public int Year { get; set; }
        public ModifiedTime? Modified { get; set; }
        public List<CategoryData>? Category { get; set; }
        public List<CountryData>? Country { get; set; }
    }

    public class ModifiedTime
    {
        public DateTime Time { get; set; }
    }

}
