﻿namespace MaxiCrush.Contracts.Authentication.Login;

public record LoginRequest(
    string Email,
    string Password);