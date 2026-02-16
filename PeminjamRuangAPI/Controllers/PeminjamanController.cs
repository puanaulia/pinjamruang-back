using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeminjamRuangAPI.Data;
using PeminjamRuangAPI.Entities;
using PeminjamRuangAPI.DTOs;

namespace PeminjamRuangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeminjamanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeminjamanController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/peminjaman?search=&status=&page=&pageSize=
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeminjamanResponseDto>>> GetPeminjaman(
            [FromQuery] string? search = null,
            [FromQuery] string? status = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Peminjaman
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => 
                    p.NamaPeminjam.Contains(search) || 
                    p.Ruangan.Contains(search) ||
                    p.Tujuan.Contains(search));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status == status);
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = items.Select(p => new PeminjamanResponseDto
            {
                Id = p.Id,
                NamaPeminjam = p.NamaPeminjam,
                Ruangan = p.Ruangan,
                TanggalPinjam = p.TanggalPinjam,
                Tujuan = p.Tujuan,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            });

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Page", page.ToString());
            Response.Headers.Append("X-Page-Size", pageSize.ToString());

            return Ok(response);
        }

        // GET: api/peminjaman/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeminjamanResponseDto>> GetPeminjaman(int id)
        {
            var peminjaman = await _context.Peminjaman
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (peminjaman == null)
                return NotFound(new { message = "Data tidak ditemukan" });

            var response = new PeminjamanResponseDto
            {
                Id = peminjaman.Id,
                NamaPeminjam = peminjaman.NamaPeminjam,
                Ruangan = peminjaman.Ruangan,
                TanggalPinjam = peminjaman.TanggalPinjam,
                Tujuan = peminjaman.Tujuan,
                Status = peminjaman.Status,
                CreatedAt = peminjaman.CreatedAt
            };

            return Ok(response);
        }

        // POST: api/peminjaman
        [HttpPost]
        public async Task<ActionResult> CreatePeminjaman(CreatePeminjamanDto createDto)
        {
            var peminjaman = new Peminjaman
            {
                NamaPeminjam = createDto.NamaPeminjam,
                Ruangan = createDto.Ruangan,
                TanggalPinjam = createDto.TanggalPinjam,
                Tujuan = createDto.Tujuan,
                Status = "Menunggu",
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };

            _context.Peminjaman.Add(peminjaman);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data berhasil ditambahkan", id = peminjaman.Id });
        }

        // PUT: api/peminjaman/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePeminjaman(int id, UpdatePeminjamanDto updateDto)
        {
            var peminjaman = await _context.Peminjaman
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (peminjaman == null)
                return NotFound(new { message = "Data tidak ditemukan" });

            peminjaman.NamaPeminjam = updateDto.NamaPeminjam;
            peminjaman.Ruangan = updateDto.Ruangan;
            peminjaman.TanggalPinjam = updateDto.TanggalPinjam;
            peminjaman.Tujuan = updateDto.Tujuan;
            peminjaman.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/peminjaman/5/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto statusDto)
        {
            var peminjaman = await _context.Peminjaman
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (peminjaman == null)
                return NotFound(new { message = "Data tidak ditemukan" });

            peminjaman.Status = statusDto.Status;
            peminjaman.UpdatedAt = DateTime.Now;

            if (statusDto.Status == "Selesai" && peminjaman.TanggalSelesai == null)
                peminjaman.TanggalSelesai = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { message = $"Status diubah ke {statusDto.Status}" });
        }

        // DELETE: api/peminjaman/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeminjaman(int id)
        {
            var peminjaman = await _context.Peminjaman
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (peminjaman == null)
                return NotFound(new { message = "Data tidak ditemukan" });

            peminjaman.IsDeleted = true;
            peminjaman.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/peminjaman/riwayat
        [HttpGet("riwayat")]
        public async Task<ActionResult<IEnumerable<PeminjamanResponseDto>>> GetRiwayat(
            [FromQuery] DateTime? dari,
            [FromQuery] DateTime? sampai)
        {
            var query = _context.Peminjaman
                .Where(p => p.IsDeleted || p.Status == "Selesai")
                .AsQueryable();

            if (dari.HasValue)
                query = query.Where(p => p.TanggalPinjam >= dari.Value);
            if (sampai.HasValue)
                query = query.Where(p => p.TanggalPinjam <= sampai.Value);

            var items = await query
                .OrderByDescending(p => p.TanggalPinjam)
                .Take(100)
                .ToListAsync();

            var response = items.Select(p => new PeminjamanResponseDto
            {
                Id = p.Id,
                NamaPeminjam = p.NamaPeminjam,
                Ruangan = p.Ruangan,
                TanggalPinjam = p.TanggalPinjam,
                Tujuan = p.Tujuan,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            });

            return Ok(response);
        }
    }
}