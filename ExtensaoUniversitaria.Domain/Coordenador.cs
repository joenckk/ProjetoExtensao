using System;

namespace ExtensaoUniversitaria.Domain
{
    public class Coordenador
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public Coordenador(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome ?? string.Empty;
        }
    }
}
