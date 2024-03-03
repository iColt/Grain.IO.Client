namespace Grain.IO.TGClient.Models;

public class WhiskyModel(string name, string distillery, string age, string aBV)
{
    public string Name { get; set; } = name;
    public string Distillery { get; set; } = distillery;
    public string Age { get; set; } = age;
    public string ABV { get; set; } = aBV;
}
