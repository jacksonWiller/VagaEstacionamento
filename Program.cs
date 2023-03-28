using System;
using System.Collections.Generic;
using System.Linq;

// To execute C#, please define "static void Main" on a class
// named Solution.


public enum TipoVaga
{
    VagaMoto,
    VagasCarro,
    VagasGrande
}

public enum TipoVeiculo
{
    Moto,
    Carro,
    Van
}

public class Veiculo
{
    TipoVeiculo Tipo { get; set; }

    public Veiculo(TipoVeiculo tipo)
    {
        Tipo = tipo;
    }
}

public class Vaga
{
    public Veiculo? VeiculoOcupado { get; set; }
    public TipoVaga Tipo { get; set; }
    public bool Ocupada { get; set; }
}

public class Estacionamento
{
    public List<Vaga> Vagas { get; set; } = new List<Vaga>();
    public int VagasOcupadasVan { get; set; }

    public Estacionamento(int numVagasMoto, int numVagasCarro, int numVagasGrande)
    {
        for (int i = 1; i <= numVagasMoto; i++)
        {
            Vagas.Add(new Vaga { Tipo = TipoVaga.VagaMoto, Ocupada = false });
        }
        for (int i = 1; i <= numVagasCarro; i++)
        {
            Vagas.Add(new Vaga { Tipo = TipoVaga.VagasCarro, Ocupada = false });
        }
        for (int i = 1; i <= numVagasGrande; i++)
        {
            Vagas.Add(new Vaga { Tipo = TipoVaga.VagasGrande, Ocupada = false });
        }
    }

    public int VagasRestantes()
    {
        int count = 0;
        foreach (Vaga vaga in Vagas)
        {
            if (vaga.Ocupada == false)
            {
                count++;
            }
        }
        return count;
    }

    public int VagsTotais()
    {
        return Vagas.Count;
    }

    public bool EstacionamentoCheio()
    {
        foreach (Vaga vaga in Vagas)
        {
            if (vaga.Ocupada == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool EstacionamentoVazio()
    {
        foreach (Vaga vaga in Vagas)
        {
            if (vaga.Ocupada == true)
            {
                return false;
            }
        }
        return true;
    }

    public bool VgasTipoCheia(TipoVaga tipo)
    {
        foreach (Vaga vaga in Vagas)
        {
            if (vaga.Tipo == tipo && vaga.Ocupada == false)
            {
                return false;
            }
        }
        return true;
    }

    public void EstacionarMoto()
    {
        var vagaSelecionada = Vagas.Where(x => x.Ocupada == false).FirstOrDefault();
        if (vagaSelecionada is not null)
        {
            vagaSelecionada.Ocupada = true;
            vagaSelecionada.VeiculoOcupado = new Veiculo(TipoVeiculo.Moto);
        }
        else Console.WriteLine("Vaga indisponivel");
    }

    public void EstacionarCarro()
    {
        var vagaSelecionada = Vagas.Where(x => x.Ocupada == false
        && (x.Tipo == TipoVaga.VagasCarro || x.Tipo == TipoVaga.VagasGrande)
        ).FirstOrDefault();
        if (vagaSelecionada is not null)
        {
            vagaSelecionada.Ocupada = true;
            vagaSelecionada.VeiculoOcupado = new Veiculo(TipoVeiculo.Moto);
        }
        else Console.WriteLine("Vaga indisponivel");
    }

    public void EstacionarVan()
    {
        var vagasSelecionada = Vagas.Where(x => x.Ocupada == false
        && x.Tipo == TipoVaga.VagasCarro
        ).Take(3).ToList();

        if (vagasSelecionada.Count >= 3)
        {
            foreach (var vaga in vagasSelecionada)
            {
                vaga.Ocupada = true;
                vaga.VeiculoOcupado = new Veiculo(TipoVeiculo.Carro);

            }
            VagasOcupadasVan = VagasOcupadasVan + 3;
        }
        else if (vagasSelecionada.Count < 3)
        {

            var vagaGrandeSelecionada = Vagas.Where(x => x.Ocupada == false
            && x.Tipo == TipoVaga.VagasGrande
            ).FirstOrDefault();

            if (vagaGrandeSelecionada is not null)
            {
                vagaGrandeSelecionada.Ocupada = true;
                vagaGrandeSelecionada.VeiculoOcupado = new Veiculo(TipoVeiculo.Van);
                VagasOcupadasVan++;
            }
            else Console.WriteLine("Vaga indisponivel");
        }
        else Console.WriteLine("Vaga indisponivel");
    }

}

class Solution
{
    static void Main(string[] args)
    {
        Estacionamento estacionamento = new Estacionamento(1, 4, 3);
        Console.WriteLine("Vagas restantes: " + estacionamento.VagasRestantes());
        Console.WriteLine("Vagas totais: " + estacionamento.VagsTotais());

        estacionamento.EstacionarMoto();
        estacionamento.EstacionarMoto();
        estacionamento.EstacionarMoto();
        estacionamento.EstacionarCarro();
        estacionamento.EstacionarVan();
        estacionamento.EstacionarVan();
        estacionamento.EstacionarVan();

        Console.WriteLine("Vagas restantes: " + estacionamento.VagasRestantes());
        Console.WriteLine("Vagas totais: " + estacionamento.VagsTotais());

        Console.WriteLine("Estacionamento está cheio?: " + estacionamento.EstacionamentoCheio());
        Console.WriteLine("Estacionamento está vazio?: " + estacionamento.EstacionamentoVazio());

        Console.WriteLine("Vagas de Moto estão cheias?: " + estacionamento.VgasTipoCheia(TipoVaga.VagaMoto));

        Console.WriteLine("Vagas de Carro estão cheias?: " + estacionamento.VgasTipoCheia(TipoVaga.VagasCarro));

        Console.WriteLine("Vagas grande estão cheias?: " + estacionamento.VgasTipoCheia(TipoVaga.VagasGrande));

        Console.WriteLine("Vagas ocupadas por vans: " + estacionamento.VagasOcupadasVan);

    }
}