using System.Data;
using System.Runtime.CompilerServices;
using Dapper;
using Journey.Models;

namespace Journey.Data;

public class PinRepository : IPinRepository
{
    private readonly IDbConnection _connection;

    public PinRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    
    public IEnumerable<Pin> GetAllPins()
    {
        return _connection.Query<Pin>("SELECT * FROM pins");
    }

    public Pin GetPinById(int pinId)
    {
        // return _connection.QuerySingle<Pin>("SELECT * FROM pins WHERE pinID = @id", new { id = pinId });
        
        return _connection.QuerySingleOrDefault<Pin>(
            "SELECT * FROM pins WHERE pinID = @id",
            new { id = pinId }
        );
    }

    public void UpdatePin(Pin pinToUpdate)
    {
        _connection.Execute(
            "UPDATE pins SET pinName=@pinName, pinJournal=@pinJournal, pinLong=@pinLong, pinLat=@pinLat, imagePath=@imagePath WHERE pinID = @pinID",
            new
            {
                pinName = pinToUpdate.PinName, pinJournal = pinToUpdate.PinJournal,
                pinLong = pinToUpdate.PinLong, pinLat = pinToUpdate.PinLat, imagePath = pinToUpdate.ImagePath, pinID = pinToUpdate.PinId
            });
    }

    public void InsertPin(Pin pinToInsert)
    {
        _connection.Execute(
            "INSERT INTO pins (pinName, pinJournal, pinLong, pinLat, imagePath) VALUES (@pinName, @pinJournal, @pinLong, @pinLat, @imagePath);",
            new
            {
                pinName = pinToInsert.PinName, pinJournal = pinToInsert.PinJournal, 
                pinLong = pinToInsert.PinLong, pinLat = pinToInsert.PinLat, imagePath = pinToInsert.ImagePath, pinID = pinToInsert.PinId
            });
        
        
    }
    
    public Pin CreatePin(Pin newPin)
    {
        _connection.Execute(
            "INSERT INTO pins (pinName, pinJournal, pinLong, pinLat, pinID, imagePath) VALUES (@pinName, @pinJournal, @pinLong, @pinLat, @pinID, @imagePath);",
            new
            {
                pinName = newPin.PinName, pinJournal = newPin.PinJournal, 
                pinLong = newPin.PinLong, pinLat = newPin.PinLat, pinID = newPin.PinId, imagePath = newPin.ImagePath
            });
        return newPin;
    }

    public void DeletePin(Pin pin)
    {
        _connection.Execute("DELETE FROM Pins WHERE pinID = @pinID", new { pinId = pin.PinId });
    }
}