using System;

namespace ExtensaoUniversitaria.Domain
{
    public class Estudante
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public Estudante(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome ?? string.Empty;
        }
    }
}
