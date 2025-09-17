using GienPhim_Services.FilmService.Application.DTOs.Common;

namespace GienPhim_Services.FilmService.Application.DTOs.Client.DaoDien
{
    public class DaoDienResponseDTO
    {
        public int DaoDienId { get; set; } // id dao dien
        public string TenDD { get; set; } // ten dao dien
        public string MoTaDD { get; set; } // mo ta dao dien
    }
}
