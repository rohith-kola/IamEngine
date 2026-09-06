namespace IamEngine.Application.DTOs;

public record SignupRequest(string Email, string Password, string DisplayName);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Token, string Email, string DisplayName);