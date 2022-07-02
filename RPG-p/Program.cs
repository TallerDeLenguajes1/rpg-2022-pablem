using System.Text.Json;
using System.Text.Json.Serialization;
internal class Program
{
    public static int N = 4; //Número de personajes para el torneo
    private static void Main(string[] args)
    {
        /* INICIALIZACIÓN - Generación de personajes - Api web services */

        Console.Clear();
        int op;
        do {
            Tipeo("¿Número de personajes? (2-4-6-8-10)");
            op = Convert.ToInt16(Console.ReadLine());
        } while (op < 2 && op > 10);
        N = op;

        List<Personaje> personajesEnJuego = GeneradorPersonajes();

        /* PANTALLA DE INICIO */
        string opcion;
        do {
            Console.Clear();
            Tipeo("Bienvenido al Mundo Pintado de Ariamis");
            Tipeo("\nNuevos participantes llegaron a la contienda:\n");
            foreach(var persona in personajesEnJuego) {
                Console.WriteLine("\t"+persona.Nombre);
            }
            Tipeo("\n\nElije sus destinos...");
            Console.WriteLine("\n(T)orneo   Es(C)aramuza   (E)legidos   (V)encedores   (S)alir"); ///(L)ore (?)
            opcion = Console.ReadLine().ToLower();
            if (opcion == "t")
                Torneo(personajesEnJuego);
            if (opcion == "c")
                Batalla(personajesEnJuego[0],personajesEnJuego[1]);
            if (opcion == "e")
                MenuListarPersonajes(personajesEnJuego);
            if (opcion == "v")
                MostrarGanadoresCsv(HelperCsv.LeerCsv("ganadores.csv",','));
        } while (opcion != "s");


        /* INICIALIZACIÓN */

        List<Personaje> GeneradorPersonajes()
        {
            /* Lee combinaciones Nombre-Apodo-Tipo desde archivo: "nombres.csv" */
            List<string[]> ListaNombreApodoTipo = HelperCsv.LeerCsv("nombres.csv",',');
           
            /* CREACIÓN DE PERSONAJES RANDOM */
            var personajesENJuego = new List<Personaje>();
            Personaje nuevo;
            for (int i = 0; i < N; i++) {
                nuevo = new Personaje();
                nuevo.GenerarDatos(ref ListaNombreApodoTipo);
                nuevo.GenerarCaracteristicas();
                personajesENJuego.Add(nuevo);
            }
            return personajesENJuego;
        }

        /* PANTALLAS SECUNDARIAS: Torneo */

        void Torneo(List<Personaje> personajes)
        {
            MenuListarPersonajes(personajesEnJuego);

            Personaje p1;
            Personaje p2;
            int ronda = 1;

            while (personajes.Count > 1) 
            {
                /* CABECERA RONDA x */
                Console.Clear();
                Tipeo("VUELTA "+ronda+++":");
                Console.Write("\t");
                foreach (var persona in personajes) {
                    Console.Write("_ "+persona.Apodo+" _ ");
                }
                Console.WriteLine();
                p1 = personajes.First();
                personajes.Remove(p1);
                p2 = personajes.First();
                personajes.Remove(p2);

                /* FUNCION COMBATE SIMPLE */
                p1 = Batalla(p1,p2);

                /* UNA VEZ GANADO EL COMBATE */
                p1.MostrarCaracteristicas();
                p1.Levelear();
                personajes.Add(p1);
            }

            /* UNA VEZ GANADO EL TORNEO */
            GuardarGanadorCSV("ganadores.csv",personajes.First());
            GuardarGanadorJson("ganadores.json",personajes.First());
            
            Tipeo(personajes.First().Nombre+" resultó victorios@ en este torneo mortal\n");
            Tipeo("Su nombre quedará inmortalizado en la lista de ganadores.");
            personajesEnJuego = GeneradorPersonajes();
            Console.ReadKey();
        }

        /* PANTALLA SECUNDARIA: batalla Singular */

        Personaje Batalla(Personaje p1, Personaje p2)
        {
            Personaje auxAtq; //Personaje p1 o p2 en su turno de atacar
            Personaje auxDef; //Personaje p1 o p2 en su turno de defender
            int max = 5000;
            int ataque;
            int danio;
            var rnd = new Random();
            var ronda = 1;
            for (int i = 0; i < 6; i++) 
            {
                if (p1.Salud > 0 && p2.Salud > 0) 
                {
                    if(i%2==0) {
                        Tipeo("\nRound " + ronda+++":\n");
                        auxAtq = p1;
                        auxDef = p2;
                        Console.ReadKey();
                    } else {
                        auxAtq = p2;
                        auxDef = p1;
                    }
                    ataque = auxAtq.PoderDisparo() * rnd.Next(0, 100);
                    danio = 100 * (ataque - auxDef.PoderDefensa()) / max;
                    if (danio < 0)
                        danio = 0;
                    Console.WriteLine(p1.Nombre + " (" + p1.Salud + ")   Vs.   (" + p2.Salud + ") " + p2.Nombre);
                    Tipeo(auxAtq.Apodo + " ataca con efectividad +" + ataque); //FrasesAtq(ataque) FrasesDef(danio) 
                    Tipeo(auxDef.Apodo + " recibió " + danio + " puntos de daño\n");
                    auxDef.Salud -= danio;
                }
            }
            if (p1.Salud<p2.Salud) {
                Tipeo("\n"+p1.Nombre+" quedó eliminad@ de esta contienda");
                p1.Salud = 100;
                Console.ReadKey();
                return p2;
            } else {
                Tipeo("\n"+p2.Nombre+" está fuera de combate");
                p2.Salud = 100;
                Console.ReadKey();
                return p1;
            }
        }

        /* PANTALLA SECUNDARIA: Mostrar los personajes en juego */

        void MenuListarPersonajes(List<Personaje> personajes)
        {
            var opcion = "";
            var i = 0;
            do {
                Console.Clear();
                Console.WriteLine((i+1)+"/"+personajes.Count+" Personajes en Juego:");
                personajes.ElementAt(i).MostrarDatos(); //indexOf?
                personajes.ElementAt(i).MostrarCaracteristicas();
                Console.WriteLine("\n(G)enerar   (C)argar   (L)isto   (enter)-->");

                opcion = Console.ReadLine().ToLower();
                if(opcion == "g" || opcion == "c") 
                {
                    if(opcion == "g")
                        personajes.ElementAt(i).GenerarCaracteristicas();
                    if(opcion == "c") 
                    {
                        List<Personaje>? personajesGanadores;
                        string? documentoGanadores = HelperJson.LeerJson("ganadores.json");
                        if (documentoGanadores != null) {
                            personajesGanadores = JsonSerializer.Deserialize<List<Personaje>>(documentoGanadores);
                        } else {
                            personajesGanadores = new List<Personaje>();
                        }
                        Personaje? cargado = MenuListarGanadores(personajesGanadores);
                        if (cargado != null) 
                        {
                            if(!personajes.Contains(cargado)) 
                            {
                                var aux = personajes.ElementAt(i); //personajes[i] = cargado;
                                aux = cargado;                     ///Replace()?? mala práctica? Error?
                                personajesGanadores.Remove(cargado);
                            } else {
                                Console.WriteLine("(no se puede cargar, ya existe un personaje con ese nombre)");
                                Console.ReadKey();
                            }
                        }  
                    }
                } else {
                    i++;
                }
                if(i==personajes.Count)
                    i=0;
            } while (opcion != "l");
        }

        Personaje? MenuListarGanadores(List<Personaje>? personajes)
        {
            if(personajes!=null && personajes.Any()) 
            {
                int i;
                string opcion = "";
                do {
                    i = 1;
                    foreach (var persona in personajes) {
                        Console.Clear();
                        Console.WriteLine((i++)+"/"+personajes.Count+" Carga un Personaje Ancestral:");
                        persona.MostrarDatos();
                        persona.MostrarCaracteristicas();
                        Console.WriteLine("\n(E)legir   (C)ancelar   (enter)-->");
                        opcion = Console.ReadLine().ToLower();
                        if(opcion == "c") {
                            return null;
                        }
                        if(opcion == "e") {
                            return persona;
                        }
                    }
                } while (opcion != "c");
            } else {
                Console.WriteLine("(no hay personajes guardados)");
                Console.ReadKey();
                return null;
            }
            return null;
        }

        /* PANTALLA SECUNDARIA: Mostrar lista de ganadores */

        void MostrarGanadoresCsv(List<string[]>? personajes)
        {
            string nombre,fecha,stats;
            Console.Clear();
            Tipeo("\nLista de Vencedores Ancestrales:");
            if (personajes != null && personajes.Any()) {
                Console.WriteLine(String.Format("\n{0,12} {1,35} {2,25}\n", "Torneo", "Nombre","Stats[v/d/f/a]"));
                foreach (var persona in personajes) {
                    if(persona == null)
                        break;
                    fecha = persona[0];
                    nombre = persona[1];
                    stats = persona[2];
                    Console.WriteLine(String.Format("{0,12} {1,35} {2,25}",fecha,nombre,stats));
                }
            } else {
                Tipeo("\n(no hubo ningún ganador)");
            }
            Console.ReadKey();
        }

        /* GUARDAR GANADOR EN csv */

        void GuardarGanadorCSV(string ruta, Personaje p)
        {
            string torneo = DateTime.Today.ToString("dd-MM-yyyy");
            string nombre = p.Nombre;
            string stats = p.Velocidad+"/"+p.Destreza+"/"+p.Fuerza+"/"+p.Armadura;
           
            StreamWriter sw = new StreamWriter(ruta,true); // append = true
            sw.WriteLine(torneo+","+nombre+","+stats);
            sw.Close();
        }

        void GuardarGanadorJson(string nombreArchivo, Personaje p) 
        {
            List<Personaje>? personajesGanadores;
            string? documentoGanadores = HelperJson.LeerJson(nombreArchivo);
            if (documentoGanadores != null) 
            { ///try catch?
                personajesGanadores = JsonSerializer.Deserialize<List<Personaje>>(documentoGanadores);
                if (!personajesGanadores.Contains(p)) /// equals modificado: compara sólo Nombres 
                    personajesGanadores.Remove(p);
            } else {
                personajesGanadores = new List<Personaje>();
            }
            personajesGanadores.Add(p);
            documentoGanadores = JsonSerializer.Serialize(personajesGanadores);
            HelperJson.GuardarJson(nombreArchivo,documentoGanadores);
        }
        
        /* FUNCION AUXILIAR: efecto "tipeo" */

        void Tipeo(string texto)
        {
            foreach (var c in texto)
            {
                Console.Write(c);
                Thread.Sleep(20);///20ms
            }
            Console.WriteLine();
        }
    }
}