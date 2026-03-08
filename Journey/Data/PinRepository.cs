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
            "UPDATE pins SET pinName=@pinName, pinJournal=@pinJournal, pinImage=@pinImage, pinLong=@pinLong, pinLat=@pinLat, imagePath=@imagePath WHERE pinID = @pinID",
            new
            {
                pinName = pinToUpdate.PinName, pinJournal = pinToUpdate.PinJournal, pinImage = pinToUpdate.PinImage,
                pinLong = pinToUpdate.PinLong, pinLat = pinToUpdate.PinLat, imagePath = pinToUpdate.ImagePath, pinID = pinToUpdate.PinId
            });
    }

    public void InsertPin(Pin pinToInsert)
    {
        _connection.Execute(
            "INSERT INTO pins (pinName, pinJournal, pinImage, pinLong, pinLat, imagePath) VALUES (@pinName, @pinJournal, @pinImage, @pinLong, @pinLat, @imagePath);",
            new
            {
                pinName = pinToInsert.PinName, pinJournal = pinToInsert.PinJournal, pinImage = pinToInsert.PinImage,
                pinLong = pinToInsert.PinLong, pinLat = pinToInsert.PinLat, imagePath = pinToInsert.ImagePath, pinID = pinToInsert.PinId
            });
        
        
    }
    

    // public int InsertPin(Pin pin)
    // {
    //     var sql = @"
    //     INSERT INTO pins (PinName, PinJournal, PinImage, PinLat, PinLong)
    //     OUTPUT INSERTED.PinId
    //     VALUES (@PinName, @PinJournal, @PinImage, @PinLat, @PinLong);
    // ";
    //
    //     return _connection.QuerySingle<int>(sql, pin);
    // }

    public Pin CreatePin(Pin newPin)
    {
        _connection.Execute(
            "INSERT INTO pins (pinName, pinJournal, pinImage, pinLong, pinLat, pinID, imagePath) VALUES (@pinName, @pinJournal, @pinImage, @pinLong, @pinLat, @pinID, @imagePath);",
            new
            {
                pinName = newPin.PinName, pinJournal = newPin.PinJournal, pinImage = newPin.PinImage,
                pinLong = newPin.PinLong, pinLat = newPin.PinLat, pinID = newPin.PinId, imagePath = newPin.ImagePath
            });
        return newPin;
    }

    public void DeletePin(Pin pin)
    {
        _connection.Execute("DELETE FROM Pins WHERE pinID = @pinID", new { pinId = pin.PinId });
    }
}