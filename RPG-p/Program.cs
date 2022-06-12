
internal class Program
{
    public static int N = 4;
    private static void Main(string[] args)
    {
        /* CREACIÓN DE PERSONAJES RANDOM*/
        var personajesENJuego = new List<Personaje>();
        Personaje nuevo;
        for (int i = 0; i < N; i++) {
            nuevo = new Personaje();
            nuevo.Nombre = "Persona-" + i;
            nuevo.Apodo = "Apodo-" + i;
            nuevo.generarDatos();
            nuevo.generarCaracteristicas();
            personajesENJuego.Add(nuevo);
        }
        

        /* CARGADO DE DATOS DESDE ARCHIVO */

        /* PANTALLA DE INICIO */
        string opcion;
        do {
            Console.Clear();
            Tipeo("Bienvenido al mundo pintado de Ariamis");
            Tipeo("\nElije tu destino...");
            Console.WriteLine("\n(T)orneo   Escaramuz(A)   (E)legidos   (V)encedores   (S)alir"); ///(L)ore   Vencedores --> (R)evivir
            opcion = Console.ReadLine().ToLower();
            if (opcion == "t")
                Torneo(personajesENJuego);
            if (opcion == "a")
                Batalla(personajesENJuego[0],personajesENJuego[1]);
            if (opcion == "e")
                mostrarPersonajes(personajesENJuego);
        } while (opcion != "s");


        /* PANTALLAS SECUNDARIAS */

        static void Torneo(List<Personaje> personajes)
        {
            var p1 = new Personaje();
            var p2 = new Personaje();
            
            while (personajes.Count > 1) {
                p1 = personajes.First();
                personajes.RemoveAt(0);
                p2 = personajes.First();
                personajes.RemoveAt(0);

                /* FUNCION COMBATE SIMPLE */
                personajes.Add(Batalla(p1,p2));
            }
            Console.Clear();
            Tipeo(personajes.First().Nombre+" resultó victorios@ en este torneo mortal");
            Console.WriteLine();
            Tipeo("Su nombre quedará inmortalizado en una saponita blanca.");
            Console.ReadKey();
        }

        static Personaje Batalla(Personaje p1, Personaje p2)
        {
            int max = 5000;
            int ataque;
            int danio;
            var rnd = new Random();
            Console.Clear();
            for (int i = 1; i < 3; i++) {
                if (p1.Salud > 0 && p2.Salud > 0) {
                    Console.WriteLine("\n"+p1.Nombre + "(" + p1.Salud + ")   vs.   (" + p2.Salud + ")" + p2.Nombre);
                    Tipeo("Round " + i);
                    Console.ReadKey();
                    if(i%2!=0) { 
                        ataque = p1.poderDisparo() * rnd.Next(0, 100);
                        danio = 100 * (ataque - p2.poderDefensa()) / max;
                        // Console.WriteLine(p2.poderDefensa());
                        if (danio < 0)
                            danio = 0;
                        Tipeo(p1.Apodo + " ataca con efectividad +" + ataque);
                        Tipeo(p2.Apodo + " recibió " + danio + " puntos de daño");
                        p2.Salud -= danio;
                    } else {                                            /// otra forma sin repetir código??
                        ataque = p2.poderDisparo() * rnd.Next(0, 100);
                        danio = 100 * (ataque - p1.poderDefensa()) / max;
                        // Console.WriteLine(p2.poderDefensa());
                        if (danio < 0)
                            danio = 0;
                        Tipeo(p2.Apodo + " contraataca consiguiendo +" + ataque + " puntos de ataque");
                        Tipeo(p1.Apodo + " recibió " + danio + " puntos de daño");
                        p1.Salud -= danio;
                    }
                    Console.ReadKey();
                }
            }
            if (p1.Salud<p2.Salud) {// (a implementar: en caso de empate...)
                Tipeo("\n"+p1.Nombre+" quedó eliminad@ de esta contienda");
                Console.ReadKey();
                return p2;
            } else {
                Tipeo("\n"+p2.Nombre+" está fuera de combate");
                Console.ReadKey();
                return p1;
            }
        }

        static void mostrarPersonajes(List<Personaje> personajes)
        {
            string opcion = "";
            int i = 0;
            do {
                Console.Clear();
                Console.WriteLine((i+1)+"/"+personajes.Count);
                personajes[i].mostrarCaracteristicas();
                personajes[i].mostrarDatos();
                Console.WriteLine("\n(G)enerar   (S)alir   (enter)-->");
                opcion = Console.ReadLine().ToLower();
                if(opcion == "g") {
                    personajes[i].generarCaracteristicas();
                } else {
                    i++;
                }
                if(i==personajes.Count)
                    i=0;
            } while (opcion != "s");
        }


        static void Tipeo(string texto, int delay = 20)
        {
            foreach (var c in texto)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}