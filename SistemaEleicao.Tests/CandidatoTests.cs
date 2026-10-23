using SistemaEleicao.Console.Services;

namespace SistemaEleicao.Tests;

public class CandidatoTests
{
    [Fact]
    public void CadastrarCandidato_ComSucesso_DeveAdicionarNaLista()
    {
        // Arrange
        var manager = new EleicaoManager();

        // Act
        var resultado = manager.CadastrarCandidato(13, "Candidato A");

        // Assert
        Assert.True(resultado);
        Assert.Single(manager.Candidatos);
        Assert.Equal("Candidato A", manager.Candidatos[0].Nome);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveRetornarFalso()
    {
        // Arrange
        var manager = new EleicaoManager();
        manager.CadastrarCandidato(13, "Candidato A");

        // Act
        var resultado = manager.CadastrarCandidato(13, "Candidato B");

        // Assert
        Assert.False(resultado);
        Assert.Single(manager.Candidatos); // Continua contendo apenas 1 candidato
    }
}