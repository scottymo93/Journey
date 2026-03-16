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
    
    
    [HttpPost]
    public async Task<IActionResult> UpdatePinToDatabase(Pin pin)
    {
        // Get the existing pin from the database
        var existingPin = _pinRepository.GetPinById(pin.PinId);

        if (existingPin == null)
            return RedirectToAction("Index");

        if (pin.ImageFile != null && pin.ImageFile.Length > 0)
        {
            string uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images/pins"
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // 🗑 DELETE OLD IMAGE
            if (!string.IsNullOrEmpty(existingPin.ImagePath))
            {
                string oldImagePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    existingPin.ImagePath.TrimStart('/')
                );

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            // 📷 SAVE NEW IMAGE
            string fileName = Guid.NewGuid().ToString()
                              + Path.GetExtension(pin.ImageFile.FileName);

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pin.ImageFile.CopyToAsync(stream);
            }

            pin.ImagePath = "/images/pins/" + fileName;
        }
        else
        {
            // Keep existing image if no new one uploaded
            pin.ImagePath = existingPin.ImagePath;
        }

        _pinRepository.UpdatePin(pin);

        return RedirectToAction("ViewPin", new { id = pin.PinId });
    }
    
    public IActionResult InsertPinToDatabase(Pin newPin)
    {
        _pinRepository.InsertPin(newPin);
        return RedirectToAction("Index", new { id = newPin.PinId });
    }
    

    public IActionResult InsertPin(Pin newPin)
    {
        _pinRepository.InsertPin(newPin);
        return RedirectToAction("ViewPin", new { id = newPin.PinId });
    }

    public IActionResult CreatePin()
    {
        // var newPin = new Pin();
        // _pinRepository.CreatePin(newPin);
        return View("CreatePin");
    }

    // public IActionResult ViewNewPin(Pin newPin)
    // {
    //     return View(newPin);
    // }

    public IActionResult DeletePin(Pin pin)
    {
        _pinRepository.DeletePin(pin);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePin(Pin pin)
    {
        if (pin.ImageFile != null && pin.ImageFile.Length > 0)
        {
            string uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images/pins"
            );

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileName = Guid.NewGuid().ToString()
                              + Path.GetExtension(pin.ImageFile.FileName);

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pin.ImageFile.CopyToAsync(stream);
            }

            pin.ImagePath = "/images/pins/" + fileName;
        }

        _pinRepository.InsertPin(pin);

        return RedirectToAction("Index");
    }

}