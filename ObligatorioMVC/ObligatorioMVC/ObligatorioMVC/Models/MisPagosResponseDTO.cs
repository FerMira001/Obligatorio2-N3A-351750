namespace ObligatorioMVC.Models
{
    public class MisPagosResponseDTO
    {
        public List<PagoUnicoDTO> PagosUnicos { get; set; } = new List<PagoUnicoDTO>();
        public List<PagoRecurrenteDTO> PagosRecurrentes { get; set; } = new List<PagoRecurrenteDTO>();
    }
}