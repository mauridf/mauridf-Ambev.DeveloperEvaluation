﻿using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string ClientName { get; private set; }
        public string BranchName { get; private set; }
        public SaleStatus Status { get; private set; }
        public List<SaleItem> Items { get; private set; } = new();
        public decimal TotalValue => Items.Sum(i => i.Total);

        public Sale(string saleNumber, DateTime saleDate, string clientName, string branchName, List<SaleItem> items)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            ClientName = clientName;
            BranchName = branchName;

            if (items == null || !items.Any())
                throw new ArgumentException("A venda deve conter pelo menos um item.");

            foreach (var item in items)
            {
                if (item.Quantity > 20)
                    throw new ArgumentException("Não é permitido mais de 20 unidades do mesmo item.");
            }

            Items = items;
            Status = SaleStatus.NotCancelled;
        }

        private Sale() { }

        public void CancelSale()
        {
            Status = SaleStatus.Cancelled;
        }

        public void CancelItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
                Items.Remove(item);
        }

        public void Update(string clientName, string branchName, List<SaleItem> newItems, ILogger logger)
        {
            ClientName = clientName;
            BranchName = branchName;

            // Identificar itens removidos
            var removedItems = Items.Where(oldItem =>
                !newItems.Any(newItem =>
                    newItem.ProductName == oldItem.ProductName &&
                    newItem.Quantity == oldItem.Quantity &&
                    newItem.UnitPrice == oldItem.UnitPrice)).ToList();

            foreach (var removedItem in removedItems)
            {
                logger.LogInformation("Evento: ItemCancelled | Item removido: {ProductName}", removedItem.ProductName);
            }

            Items.Clear();
            Items.AddRange(newItems);
        }


        private void RecalculateTotals()
        {
            foreach (var item in Items)
            {
                item.CalculateTotals();
            }
        }
    }
}
