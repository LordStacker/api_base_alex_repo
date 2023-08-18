using System.ComponentModel.DataAnnotations;

namespace dotnetwebapi;

public class getDtoTyped
{
    [Required(ErrorMessage = "QueryParam is required.")]
    public string firstQuery { get; set; }

    [StringLength(10, ErrorMessage = "OtherQueryParam length must not exceed 10 characters.")]
    public string secondQueryParam { get; set; }
}