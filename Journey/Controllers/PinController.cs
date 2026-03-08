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
        // if (!ModelState.IsValid)
        //     return View(pin);
        //
        // if (imageFile != null && imageFile.Length > 0)
        // {
        //     // 1️⃣ Where images will live
        //     string uploadsFolder = Path.Combine(
        //         Directory.GetCurrentDirectory(),
        //         "wwwroot/images/"
        //     );
        //
        //     // 2️⃣ Ensure folder exists
        //     if (!Directory.Exists(uploadsFolder))
        //         Directory.CreateDirectory(uploadsFolder);
        //
        //     // 3️⃣ Generate unique file name
        //     string fileName = Guid.NewGuid().ToString()
        //                       + Path.GetExtension(imageFile.FileName);
        //
        //     // 4️⃣ Full physical path
        //     string filePath = Path.Combine(uploadsFolder, fileName);
        //
        //     // 5️⃣ Save file to disk
        //     using (var stream = new FileStream(filePath, FileMode.Create))
        //     {
        //         imageFile.CopyToAsync(stream);
        //     }
        //
        //     // 6️⃣ Save path to database
        //     pin.ImagePath = "/images/" + fileName;
        // }
        
        _pinRepository.UpdatePin(pin);

        return RedirectToAction("ViewPin", new { id = pin.PinId });
    }
    
    public IActionResult InsertPinToDatabase(Pin newPin)
    
    {
        
        // IFormFile imageFile
        // if (!ModelState.IsValid)
        //     return View(newPin);
        //
        // if (imageFile != null && imageFile.Length > 0)
        // {
        //     // 1️⃣ Where images will live
        //     string uploadsFolder = Path.Combine(
        //         Directory.GetCurrentDirectory(),
        //         "wwwroot/images/"
        //     );
        //
        //     // 2️⃣ Ensure folder exists
        //     if (!Directory.Exists(uploadsFolder))
        //         Directory.CreateDirectory(uploadsFolder);
        //
        //     // 3️⃣ Generate unique file name
        //     string fileName = Guid.NewGuid().ToString()
        //                       + Path.GetExtension(imageFile.FileName);
        //
        //     // 4️⃣ Full physical path
        //     string filePath = Path.Combine(uploadsFolder, fileName);
        //
        //     // 5️⃣ Save file to disk
        //     using (var stream = new FileStream(filePath, FileMode.Create))
        //     {
        //         imageFile.CopyToAsync(stream);
        //     }
        //
        //     // 6️⃣ Save path to database
        //     newPin.ImagePath = "/images/" + fileName;
        // }

        // _context.Pins.Add(pin);
        // await _context.SaveChangesAsync();
        
        _pinRepository.InsertPin(newPin);
        return RedirectToAction("Index", new { id = newPin.PinId });
    }
    
    // public IActionResult InsertPinToDatabase(Pin pin)
    // {
    //     int newPinId = _pinRepository.InsertPin(pin);
    //
    //     return RedirectToAction("ViewPin", new { id = newPinId });
    // }

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

    public IActionResult ViewNewPin(Pin newPin)
    {
        return View(newPin);
    }

    public IActionResult DeletePin(Pin pin)
    {
        _pinRepository.DeletePin(pin);
        return RedirectToAction("Index");
    }
}