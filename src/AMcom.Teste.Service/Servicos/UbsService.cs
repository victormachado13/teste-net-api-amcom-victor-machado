using System;
using System.Collections.Generic;
using System.Linq;
using AMcom.Teste.DAL;
using AMcom.Teste.DAL.Interfaces;
using GeoCoordinatePortable;

namespace AMcom.Teste.Service
{
    public class UbsService : IServicesUbs
    {

        // Implemente um método que retorne as 5 UBSs mais próximas de um ponto (latitude e longitude) que devem 
        // ser passados como parâmetro para o método e retorne uma lista/coleção de objetos do tipo UbsDTO.
        // Esta lista deve estar ordenada pela avaliação (da melhor para a pior) de acordo com os dados que constam
        // no objeto retornado pela camada de acesso a dados (DAL).
        IRespositoryUbs _respository = null;

        public UbsService()
        {
            _respository = new UbsRepository();
        }

        public IEnumerable<UbsDTO> BuscarInformandoAQuantidade(double latitude, double longitude, int quantidadeRetorno = 5)
        {
            List<UbsDTO> Retorno = new List<UbsDTO>();

            IEnumerable<Ubs> TodasUbs = _respository.CarregarTodasAsUbs();
            TodasUbs = RetornarUbsEncontradas(latitude, longitude, TodasUbs);
            PreencherAsUbsEncontradas(Retorno, TodasUbs);

            return Retorno.OrderBy(x => x.Avaliacao);
        }

        public IEnumerable<UbsDTO> Buscar(double latitude, double longitude)
        {
            if (latitude == 0 || longitude == 0)
                return null;

            List<UbsDTO> Retorno = new List<UbsDTO>();
            IEnumerable<Ubs> TodasUbs = _respository.CarregarTodasAsUbs();
            TodasUbs = RetornarUbsEncontradas(latitude, longitude, TodasUbs);

            PreencherAsUbsEncontradas(Retorno, TodasUbs);

            return Retorno.OrderBy(x => x.Avaliacao);
        }

        private static void PreencherAsUbsEncontradas(List<UbsDTO> Retorno, IEnumerable<Ubs> TodasUbs)
        {
            foreach (Ubs item in TodasUbs)
            {
                Retorno.Add(new UbsDTO()
                {
                    Nome = item.Nome,
                    Endereco = item.ToString(),
                    Avaliacao = item.Avaliacao
                });
            }
        }

        private static IEnumerable<Ubs> RetornarUbsEncontradas(double latitude, double longitude, IEnumerable<Ubs> TodasUbs)
        {
            TodasUbs = TodasUbs.OrderBy(x => x.Distancia =
                                                             new GeoCoordinate(Convert.ToDouble(x.Latitude.Replace(".", ",")), Convert.ToDouble(x.Longitude.Replace(".", ",")))
                                                             .GetDistanceTo(new GeoCoordinate(latitude, longitude))
                                                        ).Take(5);
            return TodasUbs;
        }
    }
}
