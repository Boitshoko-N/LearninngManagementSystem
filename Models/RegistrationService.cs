using System;

public static class RegistrationService
{
    public static string GeneratePermanentNumber()
    {
        string year = DateTime.Now.Year.ToString().Substring(2, 2);
        string random = new Random().Next(100000, 999999).ToString();
        return year + random;
    }

    public static string GenerateTempPassword()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }
}
