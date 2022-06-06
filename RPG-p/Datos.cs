public class Datos
{
    Tipos tipo;
    string nombre;
    string apodo;
    DateTime fechaNacimiento;
    //int Edad; //entre 0 y 300
    int Salud; //100%

    public Tipos Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public int Salud1 { get => Salud; set => Salud = value; }

        public void generarDatos() //No aleatorio?
    {
        var rnd = new Random();
        Tipo = (Tipos)rnd.Next(0,8);
        // FechaNacimiento
        Salud = 100;

    }
}