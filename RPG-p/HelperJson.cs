public static class HelperJson
{
    public static string? LeerJson(string nombreArchivo)
    {
        string documento = "";
        if (File.Exists(nombreArchivo)) {
            using (var archivoOpen = new FileStream(nombreArchivo, FileMode.Open))
            {
                using (var strReader = new StreamReader(archivoOpen))
                {
                    documento = strReader.ReadToEnd();
                    archivoOpen.Close();
                }
            }
        }
        else {
            Console.WriteLine("Archivo no encontrado: {0}",nombreArchivo);
            return null;
        }
        return documento;
    }
    public static void GuardarJson(string nombreArchivo, string datos)
    {
        using(var archivo = new FileStream(nombreArchivo, FileMode.Create))
        {
            using (var strWriter = new StreamWriter(archivo))
            {
                strWriter.WriteLine("{0}", datos);
                strWriter.Close();
            }
        }
    } 
    
}
