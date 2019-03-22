namespace CentralTelefonica.Entidades
{
    public class LlamadaLocal : Llamada
    {
        private double precio;
        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public LlamadaLocal() {}
        //el tipo de dato que recibe,  se lo envia al =, lo mete en el base
        public LlamadaLocal(string numOrigen, string numeroDestino, double duracion)
        => (base.NumeroOrigen, base.NumeroDestino, base.Duracion) = (NumeroOrigen, numeroDestino, duracion);
        public override double CalcularPrecio()
        {
            return this.Precio * this.Duracion;
        }

    }
}