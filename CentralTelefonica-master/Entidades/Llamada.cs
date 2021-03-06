namespace CentralTelefonica.Entidades
{
    public abstract class Llamada
    {
        private string numeroOrigen;
        public string NumeroOrigen
        {
            get { return numeroOrigen; }
            set { numeroOrigen = value; }
        }

        private string numeroDestino;
        public string NumeroDestino
        {
            get { return numeroDestino; }
            set { numeroDestino = value; }
        }
        private double duracion;
        public double Duracion
        {
            get { return duracion; }
            set { duracion = value; }
        }
        public Llamada() { }
        public Llamada(string numeroOrigen, string numeroDestino, double duracion)
        => (NumeroOrigen, NumeroDestino, Duracion) = (numeroOrigen, numeroDestino, duracion);

        public abstract double CalcularPrecio(); //implementar los metodos de las clases hijas
        public override string ToString()
        {
            return $" #Origen: {this.numeroOrigen} - #Destino: {this.numeroDestino} - Duracion: {this.duracion}";
            //tostring se hereda de la clase object
        }
    }
}