namespace the_alkanz.Website.DTOs;

public class CreateCategoryRequestDto
{
    /// <summary>
    /// The name of the category.
    /// </summary>
    /// <example>Candles</example>
    public string Name { get; set; } = string.Empty;
}

public class CategoryResponseDto
{
    /// <summary>
    /// Unique identifier of the category.
    /// </summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the category.
    /// </summary>
    /// <example>Candles</example>
    public string Name { get; set; } = string.Empty;
}
