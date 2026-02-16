using System;
using System.ComponentModel.DataAnnotations;

namespace PeminjamRuangAPI.DTOs
{
    // DTO untuk CREATE (tambah data)
    public class CreatePeminjamanDto
    {
        [Required(ErrorMessage = "Nama peminjam wajib diisi")]
        public string NamaPeminjam { get; set; } = "";

        [Required(ErrorMessage = "Ruangan wajib diisi")]
        public string Ruangan { get; set; } = "";

        [Required(ErrorMessage = "Tanggal pinjam wajib diisi")]
        public DateTime TanggalPinjam { get; set; }

        public string Tujuan { get; set; } = "";
    }

    // DTO untuk UPDATE (mengubah data)
    public class UpdatePeminjamanDto
    {
        [Required(ErrorMessage = "Nama peminjam wajib diisi")]
        public string NamaPeminjam { get; set; } = "";

        [Required(ErrorMessage = "Ruangan wajib diisi")]
        public string Ruangan { get; set; } = "";

        [Required(ErrorMessage = "Tanggal pinjam wajib diisi")]
        public DateTime TanggalPinjam { get; set; }

        public string Tujuan { get; set; } = "";
    }

    // DTO untuk UPDATE STATUS (mengubah status)
    public class UpdateStatusDto
    {
        [Required(ErrorMessage = "Status wajib diisi")]
        [RegularExpression("^(Menunggu|Disetujui|Ditolak|Selesai)$",
            ErrorMessage = "Status harus: Menunggu, Disetujui, Ditolak, atau Selesai")]
        public string Status { get; set; } = "";
    }

    // DTO untuk RESPONSE (data yang dikirim ke client)
    public class PeminjamanResponseDto
    {
        public int Id { get; set; }
        public string NamaPeminjam { get; set; } = "";
        public string Ruangan { get; set; } = "";
        public DateTime TanggalPinjam { get; set; }
        public string Tujuan { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}