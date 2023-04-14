using System.Text.Json;

internal class ErrorResponse
{
    public string Message { get; set; }
    public int Status { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}