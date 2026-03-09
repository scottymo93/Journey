namespace Journey.Models;

public class Pin
{
    public int PinId { get; set; }
    public double PinLat { get; set; }
    public double PinLong { get; set; }
    public string PinName { get; set; }
    public string PinJournal { get; set; }
    public string PinImage { get; set; }
    
    public string ImagePath { get; set; }
    
    public IFormFile ImageFile { get; set; }
    
    // public IFormFile ImageFile { get; set; }
}