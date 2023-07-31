namespace API.Models.Response
{
    public class GenericResponse<TObjet>
    {
        public bool Estado { get; set; } = false;
        public string? Mensaje { get; set; }
        public TObjet? Objeto { get; set; }
        public List<TObjet>? ListaObjeto { get; set; }


    }
}
