using System.Text.Json.Serialization;
using System.Globalization;

public class Emoji
{
    [JsonPropertyName("name")]
    public string? Nombre { get; set; }

    [JsonPropertyName("category")]
    public string? Categoria { get; set; }

    [JsonPropertyName("group")]
    public string? Grupo { get; set; }

    [JsonPropertyName("htmlCode")]
    public List<string>? HtmlCode { get; set; }

    [JsonPropertyName("unicode")]
    public List<string>? Unicode { get; set; }


    public static string Generar(List<Emoji> fuente)
    {
        string emoji = "";
        var rnd = new Random();
        if(fuente!=null && fuente.Any()) 
        {
            emoji = fuente.ElementAt(rnd.Next(fuente.Count)).Unicode.First().Remove(0,2); // de string "U+1F577" a string "1F577"
            int emojiInt = int.Parse(emoji,NumberStyles.AllowHexSpecifier); // de string(hexadecimal) a int 
            emoji = Convert.ToString(char.ConvertFromUtf32(emojiInt));      // de int UNICODE a char UTF32
        }
        return emoji;
    }   
}