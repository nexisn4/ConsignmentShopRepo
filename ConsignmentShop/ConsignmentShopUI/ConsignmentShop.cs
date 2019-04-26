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
            //default sort is by Title
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).OrderBy(x => x.Title).ToList();
            itemsListbox.DataSource = itemsBinding;
            itemsListbox.DisplayMember = "Display";
            itemsListbox.ValueMember = "Display";

            //cartItems
            cartBinding.DataSource = shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;
            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";

            //vendors
            //default sort is by FirstName
            vendorsBinding.DataSource = store.Vendors.OrderBy(x => x.FirstName);
            vendorListbox.DataSource = vendorsBinding;
            vendorListbox.DisplayMember = "Display";
            vendorListbox.ValueMember = "Display";
        }

        private void SetupData()
        {
            //add vendors
            store.Vendors.Add(new Vendor
            {
                FirstName = "Rachel",
                LastName = "Davison",
            });

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

            store.Vendors.Add(new Vendor
            {
                FirstName = "Amy",
                LastName = "Davison",
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
            //copy that item to cart list
            //refresh the cart 

            Item selectedItem = (Item)itemsListbox.SelectedItem;
            //check to not add duplicate items to cart list
            if (!shoppingCartData.Contains(selectedItem))
            {
                shoppingCartData.Add(selectedItem);
                cartBinding.ResetBindings(false);
            }
        }

        private void MakePurchase_Click(object sender, EventArgs e)
        {
            //mark items in the cart as sold
            //set the money gains for owner, store
            //clear the cart list
            //update the items list
            //update the vendors

            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += item.Price * (decimal)item.Owner.Commission;
                storeProfit += item.Price * (decimal)(1 - item.Owner.Commission);
            }
            storeProfitValue.Text = string.Format("${0}", storeProfit);


            shoppingCartData.Clear();
            cartBinding.ResetBindings(false);

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsBinding.ResetBindings(false);

            vendorsBinding.ResetBindings(false);
        }

        private void RemoveCartItem_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)shoppingCartListbox.SelectedItem;
            shoppingCartData.Remove(selectedItem);
            cartBinding.ResetBindings(false);
        }

        private void EmptyCart_Click(object sender, EventArgs e)
        {
            shoppingCartData.Clear();
            cartBinding.ResetBindings(false);
        }
    }
}
