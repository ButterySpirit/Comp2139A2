namespace Assign1.Models;

public class ErrorViewModel
{
    public string RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    // Add the following properties
    public string Path { get; set; }
    public string ErrorMessage { get; set; }
    public string ExceptionMessage { get; set; }
}

