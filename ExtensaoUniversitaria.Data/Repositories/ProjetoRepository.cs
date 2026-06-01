using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExtensaoUniversitaria.Domain;

namespace ExtensaoUniversitaria.Data.Repositories
{
    public class ProjetoRepository
    {
        private readonly List<Projeto> _projetos = new();

        public void Adicionar(Projeto projeto)
        {
            if (projeto == null) throw new ArgumentNullException(nameof(projeto));
            if (_projetos.Any(p => p.Id == projeto.Id)) return;
            _projetos.Add(projeto);
        }

        public Projeto? ObterPorId(Guid id)
        {
            return _projetos.FirstOrDefault(p => p.Id == id);
        }

        public IReadOnlyCollection<Projeto> ListarTodos()
        {
            return new ReadOnlyCollection<Projeto>(_projetos);
        }

        public IReadOnlyCollection<Projeto> BuscarPorArea(string area)
        {
            if (string.IsNullOrWhiteSpace(area)) return new ReadOnlyCollection<Projeto>(_projetos);
            var lista = _projetos.Where(p => (p.Titulo != null && p.Titulo.Contains(area, StringComparison.OrdinalIgnoreCase)) || (p.Descricao != null && p.Descricao.Contains(area, StringComparison.OrdinalIgnoreCase))).ToList();
            return new ReadOnlyCollection<Projeto>(lista);
        }

        public IReadOnlyCollection<Projeto> BuscarPorStatus(StatusProjeto status)
        {
            var lista = _projetos.Where(p => p.Status == status).ToList();
            return new ReadOnlyCollection<Projeto>(lista);
        }
    }
}
