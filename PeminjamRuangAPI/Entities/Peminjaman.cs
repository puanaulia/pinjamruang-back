using System;
using System.ComponentModel.DataAnnotations;

namespace PeminjamRuangAPI.Entities;

public class Peminjaman
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string NamaPeminjam { get; set; } = "";

    [Required]
    [MaxLength(50)]
    public string Ruangan { get; set; } = "";

    [Required]
    public DateTime TanggalPinjam { get; set; }

    public DateTime? TanggalSelesai { get; set; }

    [MaxLength(200)]
    public string Tujuan { get; set; } = "";

    public string Status { get; set; } = "Menunggu";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}