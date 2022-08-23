using System;
using System.Collections.Generic;
using System.Linq;

namespace EdabitChalanges
{
    // DRAFT KINGS Interview
    // in out store we want to make sure every cusotmer will get fair discount they can find
    // please check that all of the statements are valid and return true if they are if not return false
    // calculate time and space complexity at the end

    // 1) No Clients get more than 3 discount assigned to them
    // 2) All discounts are assigned at least one time
    // 3) No clients get discount more that 20% total of their spendings
    // 4) The clients that spent more should get bigger discount

    class Interview
    {

        public static bool ValidateDiscounts(ICollection<AssignedDiscount>discountAssigned, ICollection<Customer> customers, ICollection<Discount> discounts)
        {
            
            // FIRST
            Dictionary<int, int> discountsPerClient = new Dictionary<int, int>();
            Dictionary<int, decimal> totalDiscoutnAmount = new Dictionary<int, decimal>();
            foreach(AssignedDiscount discount in discountAssigned)
            {
                decimal discountAmount =  discounts.FirstOrDefault(x => x.DiscountId == discount.DiscountId).Ammount;
                var clientId = discount.ClientId;
                // build the hash with clientId/totalDiscoutnAmount
                if (totalDiscoutnAmount.ContainsKey(clientId))
                {
                    decimal tempAmount = totalDiscoutnAmount[clientId];
                    totalDiscoutnAmount.Remove(clientId);
                    totalDiscoutnAmount.Add(clientId, tempAmount + discountAmount);
                }
                else totalDiscoutnAmount.Add(clientId, discountAmount);

                // build the hash with clientId/numberOfDiscountsAssighned
                if (discountsPerClient.ContainsKey(clientId))
                    discountsPerClient[clientId]++;
                else
                    discountsPerClient.Add(clientId, 1);
            }
            if (discountsPerClient.Values.Where(x => x > 3).Count() > 0 ) return false;

            // SECOND
            Dictionary<int, int> assignedDiscounts = new Dictionary<int, int>();
            foreach(var discount in discountAssigned)
            {
                // build hash with discountId/numberOf discounts and compare tottal of keys with all available discounts
                if (assignedDiscounts.ContainsKey(discount.DiscountId))
                    assignedDiscounts[discount.DiscountId]++;
                else assignedDiscounts.Add(discount.DiscountId, 1);
            }
            if (assignedDiscounts.Keys.Count() < discounts.Count())
                return false;

            // THIRD 
            foreach(var discount in totalDiscoutnAmount)
                if ((discount.Value / customers.FirstOrDefault(x => x.ClientId == discount.Key).AnualAmountSpent) * 100 > 20)
                    return false;

            // FORTH
            // create a lits of moneySpent/moneyDiscount, sort it by the total discount
            // check if discount is bigger or equal than next one and do the same for $ spent
            List<Tuple<decimal, decimal>> spentVsDiscount = new List<Tuple<decimal, decimal>>();
            foreach (var customer in customers)
                spentVsDiscount.Add(Tuple.Create(customer.AnualAmountSpent, totalDiscoutnAmount[customer.ClientId]));

            List<Tuple<decimal, decimal>> sortedList = spentVsDiscount.OrderBy(c => c.Item2).ToList();

            for (int i = 0; i < sortedList.Count() - 1; i++)
                if (sortedList[i].Item1 >= sortedList[i + 1].Item1 && sortedList[i].Item2 >= sortedList[i + 1].Item2)
                    return false;

            return true;
        }

    }

    public class Customer
    {
        public int ClientId { get; set; }
        public decimal AnualAmountSpent { get; set; }
    }
    public class Discount
    {
        public decimal Ammount { get; set; }
        public int Product { get; set; }
        public int DiscountId { get; set; }

    }

    public class AssignedDiscount
    {
        public int ClientId { get; set; }
        public int DiscountId { get; set; }
    }
}
