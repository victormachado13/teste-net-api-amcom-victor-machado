using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace AMcom.Teste.Service.Tests
{    
    [TestFixture]
    public class UbsServiceTest
    {
        // Implemente os testes unitários para o método criado no UbsService. Faça quantos testes achar
        // pertinente para validar a sua lógica de aplicação.

       [Test]
        public void DeveBuscarAsUbsERetornarAs5MaisProximasDaLocalizacaoInformada()
        {
            IEnumerable<UbsDTO> listUbs = null;
            var service = new Mock<UbsService>();
            service.Setup(m => m.Buscar(-10.9112370014188, -37.0620775222768)).Returns(listUbs);

            var listaConvertidaUbs = (List<UbsDTO>)listUbs;

            Assert.AreEqual(5, listaConvertidaUbs.Count);
        }

        [Test]
        public void DeveBuscarALocalizacaoComUmValorInvalidoRetornarNuloECausarUmaExcecao()
        {
            IEnumerable<UbsDTO> listUbs = null;
            var service = new Mock<UbsService>();
            service.Setup(m => m.Buscar(0, 0)).Returns(listUbs);

            var listaConvertidaUbs = (List<UbsDTO>)listUbs;

            Assert.IsNotNull(listaConvertidaUbs);
        }
    }
}
