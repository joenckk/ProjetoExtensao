using System;
using System.Linq;
using ExtensaoUniversitaria.Data.Repositories;
using ExtensaoUniversitaria.Domain;
using System.Collections.Generic;

namespace ExtensaoUniversitaria.ConsoleApp
{
	public class Program
	{
		private static readonly ProjetoRepository _repo = new ProjetoRepository();
		public static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("1 - Cadastrar projeto");
				Console.WriteLine("2 - Incluir estudante em projeto");
				Console.WriteLine("3 - Registrar atividade em projeto");
				Console.WriteLine("4 - Mudar situação de atividade");
				Console.WriteLine("5 - Listar todos os projetos");
				Console.WriteLine("6 - Buscar por área");
				Console.WriteLine("7 - Buscar por status");
				Console.WriteLine("0 - Sair");
				Console.Write("Escolha: ");
				var opc = Console.ReadLine();
				Console.WriteLine();
				switch (opc)
				{
					case "1":
						CadastrarProjeto();
						break;
					case "2":
						IncluirEstudante();
						break;
					case "3":
						RegistrarAtividade();
						break;
					case "4":
						MudarSituacaoAtividade();
						break;
					case "5":
						ListarProjetos();
						break;
					case "6":
						BuscarPorArea();
						break;
					case "7":
						BuscarPorStatus();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Opção inválida");
						break;
				}
				Console.WriteLine();
			}
		}

		private static void CadastrarProjeto()
		{
			Console.Write("Título: ");
			var titulo = Console.ReadLine() ?? string.Empty;
			Console.Write("Descrição: ");
			var descricao = Console.ReadLine() ?? string.Empty;
			Console.Write("Nome do coordenador: ");
			var nomeCoord = Console.ReadLine() ?? string.Empty;
			var coordenador = new Coordenador(nomeCoord);
				var projeto = new Projeto(titulo, descricao, coordenador);
				_repo.Adicionar(projeto);
			Console.WriteLine($"Projeto cadastrado: {projeto.Titulo} (Id: {projeto.Id})");
		}

		private static Projeto? SelecionarProjeto()
		{
			var todos = _repo.ListarTodos().ToList();
			if (!todos.Any())
			{
				Console.WriteLine("Nenhum projeto encontrado");
				return null;
			}
			for (int i = 0; i < todos.Count; i++)
			{
				Console.WriteLine($"{i} - {todos[i].Titulo} (Id: {todos[i].Id})");
			}
			Console.Write("Selecione o índice do projeto: ");
			var entrada = Console.ReadLine();
			if (!int.TryParse(entrada, out var idx) || idx < 0 || idx >= todos.Count)
			{
				Console.WriteLine("Índice inválido");
				return null;
			}
			return todos[idx];
		}

		private static void IncluirEstudante()
		{
			var projeto = SelecionarProjeto();
			if (projeto == null) return;
			Console.Write("Nome do estudante: ");
			var nome = Console.ReadLine() ?? string.Empty;
			var estudante = new Estudante(nome);
			projeto.IncluirParticipante(estudante);
			Console.WriteLine($"Estudante incluído: {estudante.Nome} (Id: {estudante.Id})");
		}

		private static void RegistrarAtividade()
		{
			var projeto = SelecionarProjeto();
			if (projeto == null) return;
			Console.Write("Título da atividade: ");
			var titulo = Console.ReadLine() ?? string.Empty;
			Console.Write("Descrição da atividade: ");
			var descricao = Console.ReadLine() ?? string.Empty;
			var atividade = new Atividade(titulo, descricao);
			projeto.RegistrarAtividade(atividade);
			Console.WriteLine($"Atividade registrada: {atividade.Titulo} (Id: {atividade.Id})");
		}

		private static void MudarSituacaoAtividade()
		{
			var projeto = SelecionarProjeto();
			if (projeto == null) return;
			var atividades = projeto.Atividades.ToList();
			if (!atividades.Any())
			{
				Console.WriteLine("Nenhuma atividade encontrada");
				return;
			}
			for (int i = 0; i < atividades.Count; i++)
			{
				Console.WriteLine($"{i} - {atividades[i].Titulo} (Situação: {atividades[i].Situacao})");
			}
			Console.Write("Selecione o índice da atividade: ");
			var entrada = Console.ReadLine();
			if (!int.TryParse(entrada, out var idx) || idx < 0 || idx >= atividades.Count)
			{
				Console.WriteLine("Índice inválido");
				return;
			}
			var atividade = atividades[idx];
			var valores = Enum.GetValues(typeof(SituacaoAtividade)).Cast<SituacaoAtividade>().ToList();
			for (int i = 0; i < valores.Count; i++)
			{
				Console.WriteLine($"{i} - {valores[i]}");
			}
			Console.Write("Selecione a nova situação pelo índice: ");
			var entrada2 = Console.ReadLine();
			if (!int.TryParse(entrada2, out var idx2) || idx2 < 0 || idx2 >= valores.Count)
			{
				Console.WriteLine("Índice inválido");
				return;
			}
			var nova = valores[idx2];
			atividade.AtualizarSituacao(nova);
			Console.WriteLine($"Situação atualizada para: {atividade.Situacao}");
		}

		private static void ListarProjetos()
		{
			var todos = _repo.ListarTodos();
			foreach (var p in todos)
			{
				Console.WriteLine($"Id: {p.Id}");
				Console.WriteLine($"Título: {p.Titulo}");
				Console.WriteLine($"Descrição: {p.Descricao}");
				Console.WriteLine($"Coordenador: {p.Coordenador.Nome}");
				Console.WriteLine($"Status: {p.Status}");
				Console.WriteLine($"Participantes: {p.Participantes.Count}");
				Console.WriteLine($"Atividades: {p.Atividades.Count}");
				Console.WriteLine("----------------------------");
			}
		}

		private static void BuscarPorArea()
		{
			Console.Write("Área (texto para buscar em título/descrição): ");
			var area = Console.ReadLine() ?? string.Empty;
			var resultados = _repo.BuscarPorArea(area);
			foreach (var p in resultados)
			{
				Console.WriteLine($"{p.Titulo} - {p.Descricao} (Id: {p.Id})");
			}
		}

		private static void BuscarPorStatus()
		{
			var valores = Enum.GetValues(typeof(StatusProjeto)).Cast<StatusProjeto>().ToList();
			for (int i = 0; i < valores.Count; i++)
			{
				Console.WriteLine($"{i} - {valores[i]}");
			}
			Console.Write("Selecione o status pelo índice: ");
			var entrada = Console.ReadLine();
			if (!int.TryParse(entrada, out var idx) || idx < 0 || idx >= valores.Count)
			{
				Console.WriteLine("Índice inválido");
				return;
			}
			var status = valores[idx];
			var resultados = _repo.BuscarPorStatus(status);
			foreach (var p in resultados)
			{
				Console.WriteLine($"{p.Titulo} - {p.Status} (Id: {p.Id})");
			}
		}
	}
}
