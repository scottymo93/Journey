using Journey.Data;
using Journey.Models;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Controllers;

public class PinController : Controller
{
    private readonly IPinRepository _pinRepository;

    public PinController(IPinRepository pinRepository)
    {
        this._pinRepository = pinRepository;
    }
    
    // GET
    public IActionResult Index()
    {
        var pins = _pinRepository.GetAllPins();
        return View(pins);
    }

    public IActionResult ViewPin(int id)
    {
        var pin = _pinRepository.GetPinById(id);
        return View(pin);
    }

    public IActionResult UpdatePin(int id)
    {
        Pin pin = _pinRepository.GetPinById(id);
        if (pin == null)
        {
            return View("ProductNotFound");
        }
        return View(pin);
    }
    
    public IActionResult UpdatePinToDatabase(Pin pin)
    {
        _pinRepository.UpdatePin(pin);

        return RedirectToAction("ViewPin", new { id = pin.PinId });
    }
    
    public IActionResult InsertPinToDatabase(Pin newPin)
    {
        _pinRepository.InsertPin(newPin);
        return RedirectToAction("Index");
    }

    public IActionResult InsertPin(Pin newPin)
    {
        _pinRepository.InsertPin(newPin);
        return RedirectToAction("ViewPin", new { id = newPin.PinId });
    }

    public IActionResult CreatePin()
    {
        var newPin = _pinRepository.CreatePin();
        return ViewNewPin(newPin);
    }

    public IActionResult ViewNewPin(Pin newPin)
    {
        return RedirectToAction("ViewPin");
    }

    public IActionResult DeletePin(Pin pin)
    {
        _pinRepository.DeletePin(pin);
        return RedirectToAction("Index");
    }
}