using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        private decimal storeProfit = 0.00M;

        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData(); //setup for initial testing/data

            //link listboxes to bindingsource
            //store.Items
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListbox.DataSource = itemsBinding;
            itemsListbox.DisplayMember = "Display";
            itemsListbox.ValueMember = "Display";

            //cartItems
            cartBinding.DataSource = shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;
            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";

            //vendors
            vendorsBinding.DataSource = store.Vendors;
            vendorListbox.DataSource = vendorsBinding;
            vendorListbox.DisplayMember = "Display";
            vendorListbox.ValueMember = "Display";
        }

        private void SetupData()
        {
            //add vendors
            store.Vendors.Add(new Vendor
            {
                FirstName = "Bill",
                LastName = "Smith",
            });

            store.Vendors.Add(new Vendor
            {
                FirstName = "Sue",
                LastName = "Jones",
            });

            //add items
            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "A book about whale",
                Price = 4.50M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "A Tale of Two Cities",
                Description = "A book about a revolution",
                Price = 3.80M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Harry Potter 1",
                Description = "A book about wizards",
                Price = 5.20M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "Jane Eyre",
                Description = "A book about a girl",
                Price = 1.50M,
                Owner = store.Vendors[0]
            });

            //set store
            store.Name = "Seconds are Better";
        }

        private void AddToCart_Click(object sender, EventArgs e)
        {
            //figure out how to get selected items
            //copy that item to shopping cart
            //remove item ? - maybe not.

            Item selectedItem = (Item)itemsListbox.SelectedItem;
            shoppingCartData.Add(selectedItem);
            cartBinding.ResetBindings(false);
        }

        private void MakePurchase_Click(object sender, EventArgs e)
        {
            //mark item as sold
            //clear the cart

            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += item.Price * (decimal)item.Owner.Commission;
                storeProfit += item.Price * (decimal)(1 - item.Owner.Commission);
            }

            storeProfitValue.Text = string.Format("${0}", storeProfit);

            shoppingCartData.Clear();
            cartBinding.ResetBindings(false);

            //reset the items
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsBinding.ResetBindings(false);

            //reset the vendors
            vendorsBinding.ResetBindings(false);
        }

        private void RemoveCartItem_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)shoppingCartListbox.SelectedItem;
            shoppingCartData.Remove(selectedItem);
            cartBinding.ResetBindings(false);
        }
    }
}
