using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ExtensaoUniversitaria.Domain
{
    public class Projeto
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public Coordenador Coordenador { get; private set; }
        public StatusProjeto Status { get; private set; }

        private readonly List<Estudante> _participantes = new();
        private readonly List<Atividade> _atividades = new();

        public IReadOnlyCollection<Estudante> Participantes => new ReadOnlyCollection<Estudante>(_participantes);
        public IReadOnlyCollection<Atividade> Atividades => new ReadOnlyCollection<Atividade>(_atividades);

        public Projeto(string titulo, string descricao, Coordenador coordenador)
        {
            Id = Guid.NewGuid();
            Titulo = titulo ?? string.Empty;
            Descricao = descricao ?? string.Empty;
            Coordenador = coordenador ?? throw new ArgumentNullException(nameof(coordenador));
            Status = StatusProjeto.Rascunho;
        }

        public void IncluirParticipante(Estudante estudante)
        {
            if (estudante == null) throw new ArgumentNullException(nameof(estudante));
            if (_participantes.Any(p => p.Id == estudante.Id)) return;
            _participantes.Add(estudante);
        }

        public void RegistrarAtividade(Atividade atividade)
        {
            if (atividade == null) throw new ArgumentNullException(nameof(atividade));
            if (_atividades.Any(a => a.Id == atividade.Id)) return;
            _atividades.Add(atividade);
        }

        public void AtualizarStatus(StatusProjeto status)
        {
            Status = status;
        }
    }
}
