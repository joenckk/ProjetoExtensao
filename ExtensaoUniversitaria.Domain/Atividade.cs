using System;

namespace ExtensaoUniversitaria.Domain
{
    public class Atividade
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public SituacaoAtividade Situacao { get; private set; }

        public Atividade(string titulo, string descricao)
        {
            Id = Guid.NewGuid();
            Titulo = titulo ?? string.Empty;
            Descricao = descricao ?? string.Empty;
            DataCriacao = DateTime.UtcNow;
            Situacao = SituacaoAtividade.Pendente;
        }

        public void AtualizarSituacao(SituacaoAtividade situacao)
        {
            Situacao = situacao;
        }
    }
}
