using System;
using Microsoft.AspNetCore.Identity;

public class NPTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public NPTokenProviderOptions()
    {
        Name = "NPTokenProvider";
        TokenLifespan = TimeSpan.FromMinutes(5);
    }
}
