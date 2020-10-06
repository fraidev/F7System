using System;

namespace F7System.Api.Domain.Enums
{
    [Flags]
    public enum Perfil
    {
        Administrator = 0,
        Estudante = 1,
        Professor = 2,
        Secretario = 4,
    }
}