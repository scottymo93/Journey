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
        return _connection.QuerySingle<Pin>("SELECT * FROM pins WHERE pinID = @id", new { id = pinId });
    }

    public void UpdatePin(Pin pinToUpdate)
    {
        _connection.Execute(
            "UPDATE pins SET pinName=@pinName, pinJournal=@pinJournal, pinImage=@pinImage, pinLong=@pinLong, pinLat=@pinLat WHERE pinID = @pinID",
            new
            {
                pinName = pinToUpdate.PinName, pinJournal = pinToUpdate.PinJournal, pinImage = pinToUpdate.PinImage,
                pinLong = pinToUpdate.PinLong, pinLat = pinToUpdate.PinLat, pinID = pinToUpdate.PinId
            });
    }

    public void InsertPin(Pin pinToInsert)
    {
        _connection.Execute(
            "INSERT INTO pins (pinName, pinJournal, pinImage, pinLong, pinLat) VALUES (@pinName, @pinJournal, @pinImage, @pinLong, @pinLat);",
            new
            {
                pinName = pinToInsert.PinName, pinJournal = pinToInsert.PinJournal, pinImage = pinToInsert.PinImage,
                pinLong = pinToInsert.PinLong, pinLat = pinToInsert.PinLat
            });
    }

    public Pin CreatePin()
    {
        var newPin = new Pin();
        _connection.Execute(
            "INSERT INTO pins (pinName, pinJournal, pinImage, pinLong, pinLat, pinID) VALUES (@pinName, @pinJournal, @pinImage, @pinLong, @pinLat, @pinID);",
            new
            {
                pinName = newPin.PinName, pinJournal = newPin.PinJournal, pinImage = newPin.PinImage,
                pinLong = newPin.PinLong, pinLat = newPin.PinLat, pinID = newPin.PinId
            });
        return newPin;
    }

    public void DeletePin(Pin pin)
    {
        _connection.Execute("DELETE FROM Pins WHERE pinID = @pinID", new { pinId = pin.PinId });
    }
}