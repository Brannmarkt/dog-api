namespace Domain.Entities;

public class DogEntity
{
    public string Name {  get; set; }
    public string Color { get; set; }
    public int TailLength { get; set; }
    public int Weight { get; set; }

    public DogEntity()
    {
        Name = string.Empty;
        Color = string.Empty;
        TailLength = 0;
        Weight = 0;
    }

    public DogEntity(string name, string color, int tailLength, int weight)
    {
        Name = name;
        Color = color;
        TailLength = tailLength;
        Weight = weight;
    }
}
