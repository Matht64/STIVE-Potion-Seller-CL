namespace STIVE.CL.DTOs;

public class CartDto
{
    public string UserId { get; set; }
    public int GameDataId { get; set; }
    public List<CartItemDto> CartItems { get; set; }
}