using Microsoft.AspNetCore.Mvc;

namespace dotnetwebapi.Controllers;


[ApiController]
[Route(template: "[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;
    
    private static List<testCrud> _items = new List<testCrud>();
    private static int _nextId = 1;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [Route("/GetSecondTest")]
    [HttpGet]
    public object Get([FromQuery] string someValue)
    {
        return $"Hello {someValue}";
    }

    [Route("/GetTest")]
    [HttpGet]
    public object GetFromHeader([FromHeader] string test)
    {
        return Ok(test);
    }

    [Route("/GetTest/{id}")]
    [HttpGet]
    public object Get([FromRoute] int id)
    {
        return id;
    }

    [Route("/getJson")]
    [HttpGet()]
    public object GetJson()
    {
        var responseObject = new
        {
            FirstName = "Nico",
            LastName = "Clemente",
            Age = 29,
            Occupation = "Software Developer"
        };

        return Ok(responseObject);
    }
    
    [Route("/geterror")]
    [HttpGet()]
    public object error()
    {
        HttpContext.Response.StatusCode = 418;

        return Content("I'm a sheeesh(teapot)!");
    }
    
    
    [Route("/post/test")]
    [HttpPost()]
    public object Post([FromBody] CustomDto customDto)
    {
        if (customDto == null)
        {
            return BadRequest("Invalid JSON payload");
        }
        return Ok(customDto);
    }
    
    [Route("/get/dto")]
    [HttpGet()]
    public object GetWithDTO([FromQuery] getDto myObject)
    {
        return Ok(myObject); 
    }
    
    [Route("/get/typed")]
    [HttpGet()]
    public object GetWithTypedDTO([FromQuery] getDtoTyped myObject)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(myObject);
    }
    [Route("/error/controlled")]
    [HttpGet()]
    public object ExampleAction()
    {
        throw new BadRequestException("Bad request controlled.");
    }
    
    [Route("/crud/post")]
    [HttpPost]
    public object CreateItem(testCrudDTO crudDto)
    {
        if (crudDto == null)
        {
            return BadRequest("Test data is null.");
        }

        var item = new testCrud
        {
            Id = _nextId++,
            Name = crudDto.Name,
        };

        _items.Add(item);

        return CreatedAtRoute( new { id = item.Id }, item);
    }
    [Route("/crud/get/{id}")]
    [HttpGet()]
    public object GetItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
    [Route("/crud/update/{id}")]
    [HttpPut()]
    public object UpdateItem(int id, testCrudDTO crudDto)
    {
        if (crudDto == null || id != crudDto.Id)
        {
            return BadRequest();
        }

        var existingItem = _items.FirstOrDefault(i => i.Id == id);
        if (existingItem == null)
        {
            return NotFound();
        }

        existingItem.Name = crudDto.Name;

        return NoContent();
    }


}