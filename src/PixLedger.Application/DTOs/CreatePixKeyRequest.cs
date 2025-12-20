namespace PixLedger.Application.DTOs;

// later, the userid will then be retrieved by jwt
public record CreatePixKeyRequest(string Key, string Kind, string UserId);