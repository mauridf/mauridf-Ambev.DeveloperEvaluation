using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit
{
    public class SaleTests
    {
        [Fact]
        public void CriarVenda_ItensValidos_DeveCriarVendaComStatusNotCancelled()
        {
            var items = new List<SaleItem>
            {
                new SaleItem("Produto A", 5, 10m),
                new SaleItem("Produto B", 2, 20m)
            };

            var sale = new Sale("S001", DateTime.Now, "Cliente X", "Filial Y", items);

            Assert.Equal("S001", sale.SaleNumber);
            Assert.Equal("Cliente X", sale.ClientName);
            Assert.Equal("Filial Y", sale.BranchName);
            Assert.Equal(SaleStatus.NotCancelled, sale.Status);
            Assert.Equal(2, sale.Items.Count);
            Assert.True(sale.TotalValue > 0);
        }

        [Fact]
        public void CriarVenda_SemItens_DeveLancarArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                new Sale("S002", DateTime.Now, "Cliente Y", "Filial Z", new List<SaleItem>()));

            Assert.Contains("pelo menos um item", exception.Message);
        }

        [Fact]
        public void CriarVenda_ItemComQuantidadeMaiorQue20_DeveLancarArgumentException()
        {
            var items = new List<SaleItem>
            {
                new SaleItem("Produto A", 21, 10m)
            };

            Assert.Throws<ArgumentException>(() =>
                new Sale("S003", DateTime.Now, "Cliente Z", "Filial W", items));
        }

        [Fact]
        public void CancelarVenda_AlteraStatusParaCancelled()
        {
            var items = new List<SaleItem> { new SaleItem("Produto", 1, 10m) };
            var sale = new Sale("S004", DateTime.Now, "Cliente", "Filial", items);

            sale.CancelSale();

            Assert.Equal(SaleStatus.Cancelled, sale.Status);
        }

        [Fact]
        public void CancelarItem_RemoveItemDaLista()
        {
            var item1 = new SaleItem("Produto A", 1, 10m);
            var item2 = new SaleItem("Produto B", 1, 20m);
            var sale = new Sale("S005", DateTime.Now, "Cliente", "Filial", new List<SaleItem> { item1, item2 });

            sale.CancelItem(item1.Id);

            Assert.Single(sale.Items);
            Assert.DoesNotContain(sale.Items, i => i.Id == item1.Id);
        }
    }

    public class SaleItemTests
    {
        [Fact]
        public void CriarSaleItem_QuantidadeValida_DeveCalcularDescontoETotalCorretamente()
        {
            var item = new SaleItem("Produto A", 5, 10m);

            Assert.Equal(5, item.Quantity);
            Assert.Equal(10m, item.UnitPrice);

            // Desconto: 10% porque qtd >= 4 e < 10
            decimal expectedDiscount = 0.1m * 10m * 5;
            Assert.Equal(expectedDiscount, item.Discount);

            decimal expectedTotal = (item.UnitPrice * item.Quantity) - item.Discount;
            Assert.Equal(expectedTotal, item.Total);
        }

        [Fact]
        public void CriarSaleItem_QuantidadeInvalida_DeveLancarArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new SaleItem("Produto", 0, 10m));
            Assert.Throws<ArgumentException>(() => new SaleItem("Produto", 21, 10m));
        }
    }
}
