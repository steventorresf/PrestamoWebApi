﻿namespace Domain.DTO
{
    public class ClienteRequestDTO
    {
        public string ClienteId { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
        public string TipoId { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string GeneroId { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string TelCel { get; set; } = string.Empty;
        public string EstadoId { get; set; } = string.Empty;
    }
}
