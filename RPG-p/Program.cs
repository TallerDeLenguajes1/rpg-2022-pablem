
var personajesENJuego = new List<Personaje>();
Personaje nuevo; 
for (int i = 0; i < 4; i++) {
    nuevo = new Personaje();
    nuevo.Nombre = "Persona-"+i;
    nuevo.Apodo = "Apodo-"+i;
    nuevo.generarDatos();
    nuevo.generarCaracteristicas();
    personajesENJuego.Add(nuevo);
}
// foreach (var persona in personajesENJuego)
// {
//     persona.mostrarDatos();
//     persona.mostrarCaracteristicas();
// }

string opcion;
do {
    Console.Clear();
    Tipeo("Bienvenido al mundo pintado de Ariamis");
    Tipeo("\nElije tu destino...");
    Console.WriteLine("\n(T)orneo   (E)scaramuza   (H)éroes   (S)alir");
    opcion = Console.ReadLine().ToLower();
    if (opcion == "e")
        Batalla(personajesENJuego);
} while (opcion != "s");



static void Batalla(List<Personaje> p)
{
    int t1, t2;
    int max = 50000;
    int ataque;
    int danio;
    var rnd = new Random();
    Console.Clear();
    for (int i = 1; i < 4; i++) {
        Console.WriteLine();
        Tipeo(p[0].Apodo+"("+p[0].Salud+")   vs.   ("+p[1].Salud+")"+p[1].Apodo);
        Tipeo("Round "+i);
        Console.ReadKey();
        ataque = p[0].poderDisparo()*rnd.Next(0,100);
        danio = 100*(ataque - p[1].poderDefensa())/max;
        if(danio < 0)
            danio = 0;
        Tipeo(p[0].Apodo+" ataca con efectividad +"+ataque);
        Tipeo(p[1].Apodo+" recibió "+danio+" de daño");
        p[1].Salud -= danio; 
        Console.ReadKey();
    }
}
static void Tipeo(string texto, int delay=30)
{
    foreach (var c in texto)
    {
        Console.Write(c);
        System.Threading.Thread.Sleep(delay);
    }
    Console.WriteLine();
}
