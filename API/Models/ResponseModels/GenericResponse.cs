namespace API.Models.Response
{
    public class GenericResponse<TObjet>
    {
        public bool Status { get; set; } = false;
        public string? Message { get; set; }
        public TObjet? Object { get; set; }
        public List<TObjet>? ListObject { get; set; }


    }
}
