using GienPhim_Services.FilmService.Domain.Exceptions.Duplicates;
using GienPhim_Services.FilmService.Domain.Exceptions.Failed;
using GienPhim_Services.FilmService.Domain.Exceptions.Invalid;
using GienPhim_Services.FilmService.Domain.Exceptions.NotFounds;
using GienPhim_Services.FilmService.Shared.Exceptions;
using System.Net;

namespace GienPhim_Services.FilmService.Api.Middlewares
{
    // Ngoại lệ trung gian
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Dictionary mapping exception type → HTTP status
        private static readonly Dictionary<Type, int> ExceptionStatusMap = new()
        {

            // { typeof(CreateUserFailedException), StatusCodes.Status500InternalServerError }
            // Đạo diễn
            { typeof(DuplicateDaoDienException), StatusCodes.Status500InternalServerError },
            { typeof(DaoDienFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidDaoDienException), StatusCodes.Status500InternalServerError },
            { typeof(DaoDienNotFoundException), StatusCodes.Status500InternalServerError },

            // Diễn viên
            { typeof(DuplicateDienVienException), StatusCodes.Status500InternalServerError },
            { typeof(DienVienFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidDienVienException), StatusCodes.Status500InternalServerError },
            { typeof(DienVienNotFoundException), StatusCodes.Status500InternalServerError },

            // Thể loại
            { typeof(DuplicateTheLoaiException), StatusCodes.Status500InternalServerError },
            { typeof(TheLoaiFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidTheLoaiException), StatusCodes.Status500InternalServerError },
            { typeof(TheLoaiNotFoundException), StatusCodes.Status500InternalServerError },

            // Thẻ Tag
            { typeof(DuplicateTheTagException), StatusCodes.Status500InternalServerError },
            { typeof(TheTagFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidTheTagException), StatusCodes.Status500InternalServerError },
            { typeof(TheTagNotFoundException), StatusCodes.Status500InternalServerError },

            // Ngôn ngữ
            { typeof(DuplicateNgonNguException), StatusCodes.Status500InternalServerError },
            { typeof(NgonNguFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidNgonNguException), StatusCodes.Status500InternalServerError },
            { typeof(NgonNguNotFoundException), StatusCodes.Status500InternalServerError },

            // Quốc gia
            { typeof(DuplicateQuocGiaException), StatusCodes.Status500InternalServerError },
            { typeof(QuocGiaFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidQuocGiaException), StatusCodes.Status500InternalServerError },
            { typeof(QuocGiaNotFoundException), StatusCodes.Status500InternalServerError },

            // Phim
            { typeof(DuplicatePhimException), StatusCodes.Status500InternalServerError },
            { typeof(PhimFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidPhimException), StatusCodes.Status500InternalServerError },
            { typeof(PhimNotFoundException), StatusCodes.Status500InternalServerError },

            // Đạo diễn của phim
            { typeof(DuplicateDaoDienCuaPhimException), StatusCodes.Status500InternalServerError },
            { typeof(DaoDienCuaPhimFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidDaoDienCuaPhimException), StatusCodes.Status500InternalServerError },
            { typeof(DaoDienCuaPhimNotFoundException), StatusCodes.Status500InternalServerError },

            // Diễn viên của phim
            { typeof(DuplicateDienVienCuaPhimException), StatusCodes.Status500InternalServerError },
            { typeof(DienVienCuaPhimFailedException), StatusCodes.Status500InternalServerError },
            { typeof(InvalidDienVienCuaPhimException), StatusCodes.Status500InternalServerError },
            { typeof(DienVienCuaPhimNotFoundException), StatusCodes.Status500InternalServerError },

            // Phân loại thể loại
            { typeof(DuplicatePhanLoaiTheLoaiException), StatusCodes.Status409Conflict },
            { typeof(PhanLoaiTheLoaiFailedException), StatusCodes.Status400BadRequest },
            { typeof(InvalidPhanLoaiTheLoaiException), StatusCodes.Status400BadRequest },
            { typeof(PhanLoaiTheLoaiNotFoundException), StatusCodes.Status404NotFound },

            // Phân loại thẻ tag
            { typeof(DuplicatePhanLoaiTheTagException), StatusCodes.Status409Conflict },
            { typeof(PhanLoaiTheTagFailedException), StatusCodes.Status400BadRequest },
            { typeof(InvalidPhanLoaiTheTagException), StatusCodes.Status400BadRequest },
            { typeof(PhanLoaiTheTagNotFoundException), StatusCodes.Status404NotFound },

            // Tập phim
            { typeof(DuplicateTapPhimException), StatusCodes.Status409Conflict },
            { typeof(TapPhimFailedException), StatusCodes.Status400BadRequest },
            { typeof(InvalidTapPhimException), StatusCodes.Status400BadRequest },
            { typeof(TapPhimNotFoundException), StatusCodes.Status404NotFound },

            // Lượt xem phim
            { typeof(FilmViewNotFoundException), StatusCodes.Status404NotFound },

            // Lịc sử xem phim
            { typeof(InvalidLichXuXemException), StatusCodes.Status400BadRequest },
        };


        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/DaoDien") && 
                !context.Request.Path.StartsWithSegments("/api/DienVien") &&
                !context.Request.Path.StartsWithSegments("/api/TheLoai") &&
                !context.Request.Path.StartsWithSegments("/api/TheTag") &&
                !context.Request.Path.StartsWithSegments("/api/NgonNgu") &&
                !context.Request.Path.StartsWithSegments("/api/QuocGia") &&
                !context.Request.Path.StartsWithSegments("/api/Phim") &&
                !context.Request.Path.StartsWithSegments("/api/DaoDienCuaPhim") &&
                !context.Request.Path.StartsWithSegments("/api/DienVienCuaPhim") &&
                !context.Request.Path.StartsWithSegments("/api/PhanLoaiTheLoai") &&
                !context.Request.Path.StartsWithSegments("/api/PhanLoaiTheTag") &&
                !context.Request.Path.StartsWithSegments("/api/TapPhim") &&
                !context.Request.Path.StartsWithSegments("/api/FilmView") &&
                !context.Request.Path.StartsWithSegments("/api/LichSuXem"))
            {
                await _next(context);
                return;
            }
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Domain error occurred");

                int statusCode = ExceptionStatusMap.TryGetValue(ex.GetType(), out var code)
                    ? code
                    : StatusCodes.Status400BadRequest;

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    success = false,
                    message = ex.Message,
                });
            }
            catch (BusinessException ex) // lỗi nghiệp vụ
            {
                _logger.LogWarning(ex, "Business error occurred.");

                context.Response.StatusCode = ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    Status = false,
                    Msg = ex.Message
                });
            }
            catch (Exception ex) // lỗi khác
            {
                _logger.LogError(ex, "Unexpected error occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    Status = false,
                    Msg = $"Đã xảy ra lỗi hệ thống, vui lòng thử lại sau."
                });
            }
        }
    }
}
