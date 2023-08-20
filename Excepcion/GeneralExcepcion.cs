
namespace Excepcion
{ 
    public class GeneralExcepcion : Exception
    {
        public GeneralExcepcion() { }

        public GeneralExcepcion(string mensaje) : base(mensaje) { }

        public GeneralExcepcion(string mensaje, Exception innerException) : base(mensaje, innerException) { }
    }
}