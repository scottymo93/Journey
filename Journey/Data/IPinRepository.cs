using Journey.Models;

namespace Journey.Data;

public interface IPinRepository
{
    public IEnumerable<Pin> GetAllPins();
    public Pin GetPinById(int pinId);
    
    public void UpdatePin(Pin pinToUpdate);
    public void InsertPin(Pin pinToInsert);
    
    public Pin CreatePin();
    
    public void DeletePin(Pin pin);
    
    
    
}